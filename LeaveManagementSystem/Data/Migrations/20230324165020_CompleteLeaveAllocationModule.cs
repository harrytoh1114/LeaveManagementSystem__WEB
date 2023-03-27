using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Data.Migrations
{
    public partial class CompleteLeaveAllocationModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f23e79f-edab-4b8e-9516-95fd23762126",
                column: "ConcurrencyStamp",
                value: "4b9173b3-6876-4eb1-909b-3a8eeb6de455");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3f7c45d-7901-4e93-ba99-5e512f539f9c",
                column: "ConcurrencyStamp",
                value: "ef3f4b87-79de-42fb-9900-700bf492d3e6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "449a191c-d102-45ec-b774-68ab5e269532",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4966486e-5dbd-4da5-b17b-024c81fe1cb1", "AQAAAAEAACcQAAAAEB0gPXfJcDA4er6mJZzZ8b0dFpauVW2ec1ANy3cRPno99ieucg45E68knmFR68YDOA==", "d6368665-3546-4703-acdf-e73090027b1a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "927c1d2a-011b-4cb0-b87a-4f4f1dda0a4d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a1efefda-1e6c-46e7-879c-1d1b7395808f", "AQAAAAEAACcQAAAAEDzXZf/hHeoWF1PFIJA9UubGkoQ1uowBgvPj+9EaSpiWboDLlOHuU1PEnfsgmyly1Q==", "0678f39f-88f2-4e9f-bcc1-f8a79293e241" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
