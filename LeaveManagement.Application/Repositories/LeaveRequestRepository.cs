﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using LeaveManagement.Application.Contracts;
using LeaveManagement.Data;
using LeaveManagement.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace LeaveManagement.Application.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<Employee> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly AutoMapper.IConfigurationProvider configurationProvider;
		private readonly IEmailSender emailSender;

		public LeaveRequestRepository(ApplicationDbContext context, IMapper mapper, UserManager<Employee> userManager, IHttpContextAccessor httpContextAccessor, ILeaveAllocationRepository leaveAllocationRepository, AutoMapper.IConfigurationProvider configurationProvider, IEmailSender emailSender) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.configurationProvider = configurationProvider;
			this.emailSender = emailSender;
		}

        public async Task CancelLeaveRequest(int leaveRequestId)
        {
            var leaveRequest = await GetAsync(leaveRequestId);
            leaveRequest.Cancelled = true;
            await UpdateAsync(leaveRequest);

            var user = (await userManager.FindByIdAsync(leaveRequest.RequestingEmployeeId));

            await emailSender.SendEmailAsync(user.Email, $"Leave Request Cancelled", $"Your leave request from " + $"{leaveRequest.StartDate} to {leaveRequest.EndDate} has been cancelled successfully");

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

            var user = (await userManager.FindByIdAsync(leaveRequest.RequestingEmployeeId));
            var approvalStatus = approved ? "Approved" : "Declined";

            await emailSender.SendEmailAsync(user.Email, $"Leave Request {approvalStatus}", $"Your leave request from " + $"{leaveRequest.StartDate} to {leaveRequest.EndDate} has been {approvalStatus}");

        }

        public async Task<bool> CreateLeaveRequest(LeaveRequestCreateVM model)
        {
            var user = (await userManager.GetUserAsync(httpContextAccessor?.HttpContext?.User));
            var leaveAllocation = await leaveAllocationRepository.GetEmployeeAllocation(user.Id, model.LeaveTypeId);
            var requestedDays = (model.EndDate - model.StartDate).TotalDays;

            if (leaveAllocation == null)
            {
                return false;
            }

            if (requestedDays > leaveAllocation.NumberOfDays)
            {
                return false;
            }

            var leaveRequest = mapper.Map<LeaveRequest>(model);
            leaveRequest.DateRequested = DateTime.Now;
            leaveRequest.RequestingEmployeeId = (await userManager.GetUserAsync(httpContextAccessor?.HttpContext?.User)).Id;

            await AddAsync(leaveRequest);

            await emailSender.SendEmailAsync(user.Email, "Leave Request Submitted Successfully", $"Your leave request from " + $"{leaveRequest.StartDate} to {leaveRequest.EndDate} has been submitted for approval");

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

        public async Task<List<LeaveRequestVM>> GetAllAsync(string employeeId)
        {
            return await context.LeaveRequests.Where(a => a.RequestingEmployeeId == employeeId).ProjectTo<LeaveRequestVM>(configurationProvider).ToListAsync();
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
            var requests = await GetAllAsync(user.Id);

            foreach (var req in requests)
            {
                req.AppliedDays = (req.EndDate - req.StartDate).Days;
            }

            var model = new EmployeeLeaveRequestViewVM(allocations, requests);

            return model;
        }


    }
}