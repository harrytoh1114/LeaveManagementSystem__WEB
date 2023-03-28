using AutoMapper;
using AutoMapper.QueryableExtensions;
using LeaveManagement.Common.Constants;
using LeaveManagement.Data;
using LeaveManagement.Application.Contracts;
using LeaveManagement.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Application.Repositories
{
	public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<Employee> _userManger;
		private readonly ILeaveTypeRepository _leaveTypeRepository;
		private readonly IMapper mapper;
		private readonly AutoMapper.IConfigurationProvider configurationProvider;
		private readonly IEmailSender emailSender;

		public LeaveAllocationRepository(ApplicationDbContext context, UserManager<Employee> userManger, ILeaveTypeRepository leaveTypeRepository, IMapper mapper, AutoMapper.IConfigurationProvider configurationProvider, IEmailSender emailSender) : base(context)
		{
			_context = context;
			_userManger = userManger;
			_leaveTypeRepository = leaveTypeRepository;
			this.mapper = mapper;
			this.configurationProvider = configurationProvider;
			this.emailSender = emailSender;
		}

		public async Task<bool> AllocationExists(string employeeId, int leaveTypeId, int period)
		{
			return await _context.LeaveAllocations.AnyAsync(a => a.EmployeeId == employeeId &&
																 a.LeaveTypeId == leaveTypeId &&
																 a.Period == period);
		}

		public async Task<EmployeeAllocationVM> GetEmployeeAllocations(string employeeId)
		{
			var allocations = await _context.LeaveAllocations
				.Include(a => a.LeaveType)
				.Where(a => a.EmployeeId == employeeId)
				.ProjectTo<LeaveAllocationVM>(configurationProvider)
				.ToListAsync();

			var employee = await _userManger.FindByIdAsync(employeeId);

			var employeeAllocationModel = mapper.Map<EmployeeAllocationVM>(employee);

			employeeAllocationModel.LeaveAllocations = allocations;

			return employeeAllocationModel;
		}

		public async Task<LeaveAllocationEditVM> GetEmployeeAllocation(int id)
		{
			var allocation = await _context.LeaveAllocations
				.Include(a => a.LeaveType)
				.ProjectTo<LeaveAllocationEditVM>(configurationProvider)
				.FirstOrDefaultAsync(a => a.Id == id);


			if (allocation == null)
			{
				return null;
			}

			var employee = await _userManger.FindByIdAsync(allocation.EmployeeId);


			allocation.Employee = mapper.Map<EmployeeListVM>(await _userManger.FindByIdAsync(allocation.EmployeeId));

			return allocation;
		}

		public async Task LeaveAllocation(int leaveTypeId)
		{
			var employees = await _userManger.GetUsersInRoleAsync(Roles.User);
			var period = DateTime.Now.Year;
			var leaveType = await _leaveTypeRepository.GetAsync(leaveTypeId);
			var allocations = new List<LeaveAllocation>();
			var employeeWithNewAllocations = new List<Employee>();

			foreach (var employee in employees)
			{
				if (await AllocationExists(employee.Id, leaveTypeId, period))
					continue;

				var allocation = new LeaveAllocation
				{
					EmployeeId = employee.Id,
					LeaveTypeId = leaveTypeId,
					Period = period,
					NumberOfDays = leaveType.DefaultDays
				};

				allocations.Add(allocation);
			}

			await AddRangeAsync(allocations);

			foreach (var employee in employeeWithNewAllocations)
			{
				await emailSender.SendEmailAsync(employee.Email, $"Leave Allocation Posted for {period}", $"Your leave request from " + $"Your {leaveType.Name} " + $"has been posted for the period of {period}. You have been given {leaveType.DefaultDays}.");
			}

		}

		public async Task<bool> UpdateLeaveAllocation(LeaveAllocationEditVM model)
		{
			var leaveAllocation = await GetAsync(model.Id);

			if (leaveAllocation == null)
			{
				return false;
			}

			leaveAllocation.Period = model.Period;
			leaveAllocation.NumberOfDays = model.NumberOfDays;
			await UpdateAsync(leaveAllocation);

			return true;



		}

		public async Task<LeaveAllocation?> GetEmployeeAllocation(string employeeId, int leaveTypeId)
		{
			return await _context.LeaveAllocations.FirstOrDefaultAsync(a => a.EmployeeId == employeeId && a.LeaveTypeId == leaveTypeId);

		}
	}
}
