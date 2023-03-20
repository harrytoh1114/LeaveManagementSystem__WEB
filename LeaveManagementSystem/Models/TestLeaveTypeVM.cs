using LeaveManagementSystem.Data;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Models
{
    public class TestLeaveTypeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Default Days")]
        public int DefaultDays { get; set; }

        public TestLeaveTypeVM(LeaveType leaveType)
        {
            this.Id = leaveType.Id;
            this.Name = leaveType.Name;
            this.DefaultDays = leaveType.DefaultDays;
        }

}
}
