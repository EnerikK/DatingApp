using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatingApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addingphotos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Photos");

            migrationBuilder.AddColumn<Guid>(
                name: "UserProfileId",
                table: "Photos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserProfileId",
                table: "Photos",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_UserProfiles_UserProfileId",
                table: "Photos",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "UserProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_UserProfiles_UserProfileId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_UserProfileId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "Photos");

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
