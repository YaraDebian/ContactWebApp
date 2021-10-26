using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class AddContactToLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Log_Contact_ContactId",
                table: "Log");

            migrationBuilder.AddForeignKey(
                name: "FK_Log_Contact_ContactId",
                table: "Log",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Log_Contact_ContactId",
                table: "Log");

            migrationBuilder.AddForeignKey(
                name: "FK_Log_Contact_ContactId",
                table: "Log",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
