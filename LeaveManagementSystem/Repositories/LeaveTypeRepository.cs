using LeaveManagementSystem.Contracts;
using LeaveManagementSystem.Data;

namespace LeaveManagementSystem.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
