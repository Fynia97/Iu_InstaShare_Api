﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Iu_InstaShare_Api.Migrations
{
    /// <inheritdoc />
    public partial class ModifyFriendsWithStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Friends",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Friends");
        }
    }
}
