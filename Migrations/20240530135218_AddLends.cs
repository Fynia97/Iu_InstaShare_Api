using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Iu_InstaShare_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddLends : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "LendOut",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Lends",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LendFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LendTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BorrowerId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LendStatus = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lends_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lends_UserProfiles_BorrowerId",
                        column: x => x.BorrowerId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lends_BookId",
                table: "Lends",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Lends_BorrowerId",
                table: "Lends",
                column: "BorrowerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lends");

            migrationBuilder.DropColumn(
                name: "LendOut",
                table: "Books");
        }
    }
}
