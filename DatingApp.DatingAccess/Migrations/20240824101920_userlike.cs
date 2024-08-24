using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatingApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class userlike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    SourceUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => new { x.SourceUserId, x.TargetUserId });
                    table.ForeignKey(
                        name: "FK_Likes_UserProfiles_SourceUserId",
                        column: x => x.SourceUserId,
                        principalTable: "UserProfiles",
                        principalColumn: "UserProfileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Likes_UserProfiles_TargetUserId",
                        column: x => x.TargetUserId,
                        principalTable: "UserProfiles",
                        principalColumn: "UserProfileId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Likes_TargetUserId",
                table: "Likes",
                column: "TargetUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Likes");
        }
    }
}
