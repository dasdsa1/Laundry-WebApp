using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication3.Data.Migrations
{
    public partial class SomeMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_AspNetUsers_ApplicationUserId1",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_ApplicationUserId1",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "Schedules");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Schedules",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ApplicationUserId",
                table: "Schedules",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_AspNetUsers_ApplicationUserId",
                table: "Schedules",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_AspNetUsers_ApplicationUserId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_ApplicationUserId",
                table: "Schedules");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "Schedules",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "Schedules",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ApplicationUserId1",
                table: "Schedules",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_AspNetUsers_ApplicationUserId1",
                table: "Schedules",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
