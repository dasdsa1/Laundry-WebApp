using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication3.Data.Migrations
{
    public partial class PassAddressToScheduleView4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Addresses_RequestAddressId1",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_RequestAddressId1",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "RequestAddressId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "RequestAddressId1",
                table: "Schedules");

            migrationBuilder.AddColumn<byte>(
                name: "UserAddressId",
                table: "Schedules",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<int>(
                name: "UserAddressId1",
                table: "Schedules",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_UserAddressId1",
                table: "Schedules",
                column: "UserAddressId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Addresses_UserAddressId1",
                table: "Schedules",
                column: "UserAddressId1",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Addresses_UserAddressId1",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_UserAddressId1",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "UserAddressId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "UserAddressId1",
                table: "Schedules");

            migrationBuilder.AddColumn<byte>(
                name: "RequestAddressId",
                table: "Schedules",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<int>(
                name: "RequestAddressId1",
                table: "Schedules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_RequestAddressId1",
                table: "Schedules",
                column: "RequestAddressId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Addresses_RequestAddressId1",
                table: "Schedules",
                column: "RequestAddressId1",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
