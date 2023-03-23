using LeaveManagementSystem.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagementSystem.Configuration
{
    public class UserSeedConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {

            var hasher = new PasswordHasher<Employee>();

            builder.HasData(
                new Employee
                {
                    Id = "449a191c-d102-45ec-b774-68ab5e269532",
                    Email = "test@test.com",
                    NormalizedEmail = "TEST@TEST.COM",
                    UserName = "test@test.com",
                    NormalizedUserName = "TEST@TEST.COM",
                    FirstName = "Harry",
                    LastName = "Toh",
                    PasswordHash = hasher.HashPassword(null, "!Verztec123"),
                    EmailConfirmed = true,
                },
                new Employee
                {
                    Id = "927c1d2a-011b-4cb0-b87a-4f4f1dda0a4d",
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    UserName = "admin@admin.com",
                    NormalizedUserName = "ADMIN@ADMIN.COM",
                    FirstName = "Admin",
                    LastName = "Toh",
                    PasswordHash = hasher.HashPassword(null, "!Verztec123"),
                    EmailConfirmed = true,
                }
            );;
        }
    }
}