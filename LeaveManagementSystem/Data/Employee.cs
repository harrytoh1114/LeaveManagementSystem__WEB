using Microsoft.AspNetCore.Identity;

namespace LeaveManagementSystem.Data
{
    public class Employee : IdentityUser
    {
       public string? FirstName { get; set; }
       public string? LastName { get; set; }
       public string? TaxId { get; set; }
       public DateTime DOB { get; set; }
       public DateTime DateJoined { get; set; }

    }
}
