using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RFID_tracking_API.Migrations
{
    public partial class PermitremovedfromDBfornow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Weapons_Permits_PermitId",
                table: "Weapons");

            migrationBuilder.DropTable(
                name: "Permits");

            migrationBuilder.DropIndex(
                name: "IX_Weapons_PermitId",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "PermitId",
                table: "Weapons");

            migrationBuilder.AddColumn<string>(
                name: "FriendlyName",
                table: "Weapons",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FriendlyName",
                table: "Weapons");

            migrationBuilder.AddColumn<Guid>(
                name: "PermitId",
                table: "Weapons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Permits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DirectorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShooterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Association = table.Column<int>(type: "int", nullable: false),
                    PermitEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PermitStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShooterCPR = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permits_Directors_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "Directors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permits_Shooters_ShooterId",
                        column: x => x.ShooterId,
                        principalTable: "Shooters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_PermitId",
                table: "Weapons",
                column: "PermitId");

            migrationBuilder.CreateIndex(
                name: "IX_Permits_DirectorId",
                table: "Permits",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Permits_ShooterId",
                table: "Permits",
                column: "ShooterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Weapons_Permits_PermitId",
                table: "Weapons",
                column: "PermitId",
                principalTable: "Permits",
                principalColumn: "Id");
        }
    }
}
