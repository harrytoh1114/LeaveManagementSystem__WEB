using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Data.Migrations
{
    public partial class AddedDefaultUsersAndRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3f23e79f-edab-4b8e-9516-95fd23762126", "474150bf-5893-49d2-ada8-d0bfab74bc09", "Administrator", "ADMINISTRATOR" },
                    { "d3f7c45d-7901-4e93-ba99-5e512f539f9c", "b3ae11f8-4bed-4a0f-bbc4-52f966f793c0", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DOB", "DateJoined", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TaxId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "449a191c-d102-45ec-b774-68ab5e269532", 0, "1fad81f2-376b-4558-8d84-81ee29ca0176", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "test@test.com", true, "Harry", "Toh", false, null, "TEST@TEST.COM", "TEST@TEST.COM", "AQAAAAEAACcQAAAAEIhhmARddKwt/2a098/zO2FVozISmqwhpMvBK8if2t67Y6lJyI84b2pio6gqPYFZCQ==", null, false, "948cf81b-e07e-4a32-8cd3-85986a08d764", null, false, "test@test.com" },
                    { "927c1d2a-011b-4cb0-b87a-4f4f1dda0a4d", 0, "293d9323-05fa-4dce-9135-7a8cd13687e5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@admin.com", true, "Admin", "Toh", false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEKTY7OxwxxsJUTz0YNgAUtDSYyHhw8VKHWmF6hkKwOnoaCpG3mHUW3yUhb6bHBYuuA==", null, false, "a787fb0e-da4d-4694-b6e6-3d0c25786560", null, false, "admin@admin.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d3f7c45d-7901-4e93-ba99-5e512f539f9c", "449a191c-d102-45ec-b774-68ab5e269532" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3f23e79f-edab-4b8e-9516-95fd23762126", "927c1d2a-011b-4cb0-b87a-4f4f1dda0a4d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d3f7c45d-7901-4e93-ba99-5e512f539f9c", "449a191c-d102-45ec-b774-68ab5e269532" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3f23e79f-edab-4b8e-9516-95fd23762126", "927c1d2a-011b-4cb0-b87a-4f4f1dda0a4d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f23e79f-edab-4b8e-9516-95fd23762126");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3f7c45d-7901-4e93-ba99-5e512f539f9c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "449a191c-d102-45ec-b774-68ab5e269532");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "927c1d2a-011b-4cb0-b87a-4f4f1dda0a4d");
        }
    }
}
