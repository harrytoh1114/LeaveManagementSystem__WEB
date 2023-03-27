using AutoMapper;
using LeaveManagementSystem.Contracts;
using LeaveManagementSystem.Data;
using LeaveManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<Employee> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;

        public LeaveRequestRepository(ApplicationDbContext context, IMapper mapper, UserManager<Employee> userManager, IHttpContextAccessor httpContextAccessor, ILeaveAllocationRepository leaveAllocationRepository) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
            this.leaveAllocationRepository = leaveAllocationRepository;
        }

        public async Task CancelLeaveRequest(int leaveRequestId)
        {
            await DeleteAsync(leaveRequestId);
        }

        public async Task ChangeApprovalStatus(int leaveRequestId, bool approved)
        {
            var leaveRequest = await GetAsync(leaveRequestId);
            leaveRequest.Approved = approved;

            if (approved)
            {
                var allocation = await leaveAllocationRepository.GetEmployeeAllocation(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
                int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
                allocation.NumberOfDays -= daysRequested;

                await leaveAllocationRepository.UpdateAsync(allocation);
            }

            await UpdateAsync(leaveRequest);
        }

        public async Task<bool> CreateLeaveRequest(LeaveRequestCreateVM model)
        {
            var leaveRequest = mapper.Map<LeaveRequest>(model);
            var employeeId = (await userManager.GetUserAsync(httpContextAccessor?.HttpContext?.User)).Id;
            var leaveAllocation = await leaveAllocationRepository.GetEmployeeAllocation(employeeId, model.LeaveTypeId);
            var requestedDays = (model.EndDate - model.StartDate).TotalDays;

            if (leaveAllocation == null)
            {
                return false;
            }

            if(requestedDays > leaveAllocation.NumberOfDays)
			{
                return false;
			}

            leaveRequest.DateRequested = DateTime.Now;
            leaveRequest.RequestingEmployeeId = (await userManager.GetUserAsync(httpContextAccessor?.HttpContext?.User)).Id;
            leaveRequest.Approved = null;
            leaveRequest.Cancelled = false;

            await AddAsync(leaveRequest);
            return true;
        }

        public async Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestList()
        {
            var leaveRequests = await context.LeaveRequests.Include(a => a.LeaveType).ToListAsync();
            var model = new AdminLeaveRequestViewVM
            {
                TotalRequests = leaveRequests.Count,
                ApprovedRequests = leaveRequests.Count(a => a.Approved == true),
                PendingRequests = leaveRequests.Count(a => a.Approved == null),
                RejectedRequests = leaveRequests.Count(a => a.Approved == false),
                LeaveRequests = mapper.Map<List<LeaveRequestVM>>(leaveRequests),
            };

            foreach (var leaveRequest in model.LeaveRequests)
            {
                leaveRequest.Employee = mapper.Map<EmployeeListVM>(await userManager.FindByIdAsync(leaveRequest.RequestingEmployeeId));
            }

            return model;
        }

        public async Task<List<LeaveRequest>> GetAllAsync(string employeeId)
        {
            return await context.LeaveRequests.Where(a => a.RequestingEmployeeId == employeeId).ToListAsync();
        }

        public async Task<LeaveRequestVM?> GetLeaveRequestAsync(int? id)
        {
            var leaveRequest = await context.LeaveRequests.Include(a => a.LeaveType).FirstOrDefaultAsync(a => a.Id == id);

            if (leaveRequest == null) return null;

            var model = mapper.Map<LeaveRequestVM>(leaveRequest);
            model.Employee = mapper.Map<EmployeeListVM>(await userManager.FindByIdAsync(leaveRequest?.RequestingEmployeeId));
            return model;
        }

        public async Task<EmployeeLeaveRequestViewVM> GetMyLeaveDetails()
        {
            var user = await userManager.GetUserAsync(httpContextAccessor?.HttpContext?.User);
            var allocations = (await leaveAllocationRepository.GetEmployeeAllocations(user.Id)).LeaveAllocations;
            var requests = mapper.Map<List<LeaveRequestVM>>(await GetAllAsync(user.Id));

            foreach (var req in requests)
            {
                req.AppliedDays = (req.EndDate - req.StartDate).Days;
            }

            var model = new EmployeeLeaveRequestViewVM(allocations, requests);

            return model;
        }
    }
}
