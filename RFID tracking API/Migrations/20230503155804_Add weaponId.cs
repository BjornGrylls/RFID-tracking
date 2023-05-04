using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RFID_tracking_API.Migrations
{
    /// <inheritdoc />
    public partial class AddweaponId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Weapons_WeaponRegistrationNumber",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_RegularUsers_Weapons_WeaponRegistrationNumber",
                table: "RegularUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Weapons",
                table: "Weapons");

            migrationBuilder.DropIndex(
                name: "IX_RegularUsers_WeaponRegistrationNumber",
                table: "RegularUsers");

            migrationBuilder.DropIndex(
                name: "IX_Loans_WeaponRegistrationNumber",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "WeaponRegistrationNumber",
                table: "RegularUsers");

            migrationBuilder.DropColumn(
                name: "WeaponRegistrationNumber",
                table: "Loans");

            migrationBuilder.AlterColumn<string>(
                name: "FriendlyName",
                table: "Weapons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNumber",
                table: "Weapons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Weapons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "WeaponId",
                table: "RegularUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "WeaponId",
                table: "Loans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Weapons",
                table: "Weapons",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RegularUsers_WeaponId",
                table: "RegularUsers",
                column: "WeaponId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_WeaponId",
                table: "Loans",
                column: "WeaponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Weapons_WeaponId",
                table: "Loans",
                column: "WeaponId",
                principalTable: "Weapons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegularUsers_Weapons_WeaponId",
                table: "RegularUsers",
                column: "WeaponId",
                principalTable: "Weapons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Weapons_WeaponId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_RegularUsers_Weapons_WeaponId",
                table: "RegularUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Weapons",
                table: "Weapons");

            migrationBuilder.DropIndex(
                name: "IX_RegularUsers_WeaponId",
                table: "RegularUsers");

            migrationBuilder.DropIndex(
                name: "IX_Loans_WeaponId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "WeaponId",
                table: "RegularUsers");

            migrationBuilder.DropColumn(
                name: "WeaponId",
                table: "Loans");

            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNumber",
                table: "Weapons",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FriendlyName",
                table: "Weapons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "WeaponRegistrationNumber",
                table: "RegularUsers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WeaponRegistrationNumber",
                table: "Loans",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Weapons",
                table: "Weapons",
                column: "RegistrationNumber");

            migrationBuilder.CreateIndex(
                name: "IX_RegularUsers_WeaponRegistrationNumber",
                table: "RegularUsers",
                column: "WeaponRegistrationNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_WeaponRegistrationNumber",
                table: "Loans",
                column: "WeaponRegistrationNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Weapons_WeaponRegistrationNumber",
                table: "Loans",
                column: "WeaponRegistrationNumber",
                principalTable: "Weapons",
                principalColumn: "RegistrationNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegularUsers_Weapons_WeaponRegistrationNumber",
                table: "RegularUsers",
                column: "WeaponRegistrationNumber",
                principalTable: "Weapons",
                principalColumn: "RegistrationNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
