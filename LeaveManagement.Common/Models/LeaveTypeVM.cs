using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Common.Models
{
	public class LeaveTypeVM
    {
        public int Id { get; set; }

        [Display(Name = "Leave Type")]
        [Required(ErrorMessage = "Please enter a valid leave name")]
        public string Name { get; set; }

        [Display(Name = "Default number of Days")]
        [Required(ErrorMessage = "Please enter a valid number between 1 & 25")]
        [Range(1, 25, ErrorMessage = "Please enter a valid number between 1 & 25")]
        public int DefaultDays { get; set; }
    }
}
