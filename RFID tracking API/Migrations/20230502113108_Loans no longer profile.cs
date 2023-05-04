using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RFID_tracking_API.Migrations
{
    public partial class Loansnolongerprofile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MailAddress",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Loans");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MailAddress",
                table: "Loans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Loans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Loans",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
