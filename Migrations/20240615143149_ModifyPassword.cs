using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Iu_InstaShare_Api.Migrations
{
    /// <inheritdoc />
    public partial class ModifyPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "UserProfiles");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "UserProfiles",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "UserProfiles",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "UserProfiles");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
