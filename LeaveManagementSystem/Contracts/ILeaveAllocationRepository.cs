using LeaveManagementSystem.Data;
using LeaveManagementSystem.Models;

namespace LeaveManagementSystem.Contracts
{
	public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
	{
		Task LeaveAllocation(int leaveTypeId);
		Task<bool> AllocationExists(string employeeId, int leaveTypeId, int period);
		Task<EmployeeAllocationVM> GetEmployeeAllocations(string employeeId);
		Task<LeaveAllocationEditVM> GetEmployeeAllocation(int id);
		Task<bool> UpdateLeaveAllocation(LeaveAllocationEditVM model);
	}
}
