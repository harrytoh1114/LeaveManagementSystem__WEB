using LeaveManagement.Common.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagement.Data.Configuration
{
    public class RoleSeedConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole 
                { 
                    Id = "3f23e79f-edab-4b8e-9516-95fd23762126", 
                    Name =  Roles.Administrator,
                    NormalizedName = Roles.Administrator.ToUpper()
                },
                new IdentityRole
                {
                    Id = "d3f7c45d-7901-4e93-ba99-5e512f539f9c",
                    Name = Roles.User,
                    NormalizedName = Roles.User.ToUpper()
                }
            );

        }
    }
}