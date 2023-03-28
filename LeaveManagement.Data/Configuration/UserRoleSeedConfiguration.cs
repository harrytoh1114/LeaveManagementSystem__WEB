using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagement.Data.Configuration
{
    public class UserRoleSeedConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>  
                { 
                    RoleId = "3f23e79f-edab-4b8e-9516-95fd23762126",
                    UserId = "927c1d2a-011b-4cb0-b87a-4f4f1dda0a4d"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "d3f7c45d-7901-4e93-ba99-5e512f539f9c",
                    UserId = "449a191c-d102-45ec-b774-68ab5e269532"
                }
           );
        }
    }
}