using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RFID_tracking_API.Migrations
{
    public partial class AddLoanstoo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShooterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WeaponRegistrationNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoanStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoanEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loans_Shooters_ShooterId",
                        column: x => x.ShooterId,
                        principalTable: "Shooters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loans_Weapons_WeaponRegistrationNumber",
                        column: x => x.WeaponRegistrationNumber,
                        principalTable: "Weapons",
                        principalColumn: "RegistrationNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_ShooterId",
                table: "Loans",
                column: "ShooterId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_WeaponRegistrationNumber",
                table: "Loans",
                column: "WeaponRegistrationNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loans");
        }
    }
}
