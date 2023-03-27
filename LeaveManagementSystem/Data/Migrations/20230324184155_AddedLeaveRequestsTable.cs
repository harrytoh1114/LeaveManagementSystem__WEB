using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Data.Migrations
{
    public partial class AddedLeaveRequestsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaveRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    DateRequested = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: true),
                    Cancelled = table.Column<bool>(type: "bit", nullable: false),
                    RequestingEmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f23e79f-edab-4b8e-9516-95fd23762126",
                column: "ConcurrencyStamp",
                value: "6013378d-3ba6-418c-89d3-32cd66e96bb5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3f7c45d-7901-4e93-ba99-5e512f539f9c",
                column: "ConcurrencyStamp",
                value: "d21b6282-6c01-48ad-abed-4194df801caf");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "449a191c-d102-45ec-b774-68ab5e269532",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "18ac0b16-166e-4bc3-b0e7-11c0efb5ba4a", "AQAAAAEAACcQAAAAEF/li9ahTpTJCSnhRZg/rt6NSxdsR8E2OqQ4/eglZ8aqAfDjCVuojkhdPTBzeK4+ww==", "823ceca8-3840-4156-bf54-695d2d2cb338" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "927c1d2a-011b-4cb0-b87a-4f4f1dda0a4d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1b1a7b51-e1ed-4c09-891d-e54306683911", "AQAAAAEAACcQAAAAEKr0H22a3xLHtP7aQrx9y2xczEQPUuYW6I0KoSjnyQf0TqXFF7Ms11yb9HAGyME/Ng==", "42a28e44-4f06-4409-80e5-e9391a0a1c5e" });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_LeaveTypeId",
                table: "LeaveRequests",
                column: "LeaveTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveRequests");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f23e79f-edab-4b8e-9516-95fd23762126",
                column: "ConcurrencyStamp",
                value: "48b5dbe4-a425-4d88-9aa7-b6f316b30173");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3f7c45d-7901-4e93-ba99-5e512f539f9c",
                column: "ConcurrencyStamp",
                value: "6297b3c1-28fd-4517-b9f7-4da8859b60c5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "449a191c-d102-45ec-b774-68ab5e269532",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "014ceb7c-8dab-4716-9762-34118fda1390", "AQAAAAEAACcQAAAAEIaPpWv9kDxvzBmHrHX5KfsJhuil/o3asjkSRBghN2aSUdlBJ6fsy5uTU1LQcAR35g==", "1b2ddb8b-55c0-42b3-bcbc-2c23eefbf3d4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "927c1d2a-011b-4cb0-b87a-4f4f1dda0a4d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "79565f80-0a56-46e5-ba2b-9b028202df3f", "AQAAAAEAACcQAAAAEPyRWOpvT8rLaP9jwBp15ADp/J6RIYMc6tunBtVepa3KZZPqCEu96dr02vfORYmyVA==", "a41621ac-2709-45c9-82a6-5d994232cb77" });
        }
    }
}
