using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Iu_InstaShare_Api.Migrations
{
    /// <inheritdoc />
    public partial class ModifyLendWithIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lends_Books_BookId",
                table: "Lends");

            migrationBuilder.DropForeignKey(
                name: "FK_Lends_UserProfiles_BorrowerId",
                table: "Lends");

            migrationBuilder.AddForeignKey(
                name: "FK_Lends_Books_BookId",
                table: "Lends",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lends_UserProfiles_BorrowerId",
                table: "Lends",
                column: "BorrowerId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lends_Books_BookId",
                table: "Lends");

            migrationBuilder.DropForeignKey(
                name: "FK_Lends_UserProfiles_BorrowerId",
                table: "Lends");

            migrationBuilder.AddForeignKey(
                name: "FK_Lends_Books_BookId",
                table: "Lends",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lends_UserProfiles_BorrowerId",
                table: "Lends",
                column: "BorrowerId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
