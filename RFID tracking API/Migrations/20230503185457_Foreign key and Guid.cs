using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RFID_tracking_API.Migrations
{
    /// <inheritdoc />
    public partial class ForeignkeyandGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Shooters_ShooterId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Weapons_WeaponId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_RegularUsers_Shooters_UserId",
                table: "RegularUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_RegularUsers_Weapons_WeaponId",
                table: "RegularUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Shooters_Directors_DirectorAcceptedPictureIdId",
                table: "Shooters");

            migrationBuilder.DropIndex(
                name: "IX_Shooters_DirectorAcceptedPictureIdId",
                table: "Shooters");

            migrationBuilder.DropIndex(
                name: "IX_RegularUsers_UserId",
                table: "RegularUsers");

            migrationBuilder.DropIndex(
                name: "IX_RegularUsers_WeaponId",
                table: "RegularUsers");

            migrationBuilder.DropIndex(
                name: "IX_Loans_ShooterId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_WeaponId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "DirectorAcceptedPictureIdId",
                table: "Shooters");

            migrationBuilder.AddColumn<Guid>(
                name: "DirectorAcceptedPictureId",
                table: "Shooters",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DirectorAcceptedPictureId",
                table: "Shooters");

            migrationBuilder.AddColumn<Guid>(
                name: "DirectorAcceptedPictureIdId",
                table: "Shooters",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shooters_DirectorAcceptedPictureIdId",
                table: "Shooters",
                column: "DirectorAcceptedPictureIdId");

            migrationBuilder.CreateIndex(
                name: "IX_RegularUsers_UserId",
                table: "RegularUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegularUsers_WeaponId",
                table: "RegularUsers",
                column: "WeaponId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_ShooterId",
                table: "Loans",
                column: "ShooterId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_WeaponId",
                table: "Loans",
                column: "WeaponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Shooters_ShooterId",
                table: "Loans",
                column: "ShooterId",
                principalTable: "Shooters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Weapons_WeaponId",
                table: "Loans",
                column: "WeaponId",
                principalTable: "Weapons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegularUsers_Shooters_UserId",
                table: "RegularUsers",
                column: "UserId",
                principalTable: "Shooters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegularUsers_Weapons_WeaponId",
                table: "RegularUsers",
                column: "WeaponId",
                principalTable: "Weapons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shooters_Directors_DirectorAcceptedPictureIdId",
                table: "Shooters",
                column: "DirectorAcceptedPictureIdId",
                principalTable: "Directors",
                principalColumn: "Id");
        }
    }
}
