using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Models
{
    public class LeaveAllocationVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Number of Days")]
        [Range(1, 60, ErrorMessage = "Invalid number entered")]
        public int NumberOfDays { get; set; }

        [Required]
        [Display(Name = "Allocation Period")]
        public int Period { get; set; }

        public LeaveTypeVM? LeaveType { get; set; }
    }
}