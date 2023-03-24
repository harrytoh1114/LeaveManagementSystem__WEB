using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Data.Migrations
{
    public partial class AddedPeriodColumnToLeaveAllocationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Period",
                table: "LeaveAllocations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f23e79f-edab-4b8e-9516-95fd23762126",
                column: "ConcurrencyStamp",
                value: "0d5718a1-cf85-48f9-bb38-5b62f0443d9c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3f7c45d-7901-4e93-ba99-5e512f539f9c",
                column: "ConcurrencyStamp",
                value: "925f5305-354b-46f9-999a-a0f9f344a53e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "449a191c-d102-45ec-b774-68ab5e269532",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5df2f990-6f48-4aaa-bd57-616fc6badb81", "AQAAAAEAACcQAAAAEP3FU/SrGD3xGKOPJFD2ZS0U29aeuqwMtv1uSqu411JAZDh3BLUvXLx2w4N6pP2XqQ==", "3d5a9e99-aed3-41f0-89c4-1b0045609b26" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "927c1d2a-011b-4cb0-b87a-4f4f1dda0a4d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3e49b71b-4f5c-4630-b035-9440464c084f", "AQAAAAEAACcQAAAAEObu75FIblonqqLOZv/Zr49TU/0Wj/uOIEH8VrYkkboPPLnP3V22QiUF6Ex1ySEEfw==", "ce9de014-7f16-43aa-9d31-5ef19e28070a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Period",
                table: "LeaveAllocations");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f23e79f-edab-4b8e-9516-95fd23762126",
                column: "ConcurrencyStamp",
                value: "474150bf-5893-49d2-ada8-d0bfab74bc09");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3f7c45d-7901-4e93-ba99-5e512f539f9c",
                column: "ConcurrencyStamp",
                value: "b3ae11f8-4bed-4a0f-bbc4-52f966f793c0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "449a191c-d102-45ec-b774-68ab5e269532",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1fad81f2-376b-4558-8d84-81ee29ca0176", "AQAAAAEAACcQAAAAEIhhmARddKwt/2a098/zO2FVozISmqwhpMvBK8if2t67Y6lJyI84b2pio6gqPYFZCQ==", "948cf81b-e07e-4a32-8cd3-85986a08d764" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "927c1d2a-011b-4cb0-b87a-4f4f1dda0a4d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "293d9323-05fa-4dce-9135-7a8cd13687e5", "AQAAAAEAACcQAAAAEKTY7OxwxxsJUTz0YNgAUtDSYyHhw8VKHWmF6hkKwOnoaCpG3mHUW3yUhb6bHBYuuA==", "a787fb0e-da4d-4694-b6e6-3d0c25786560" });
        }
    }
}
