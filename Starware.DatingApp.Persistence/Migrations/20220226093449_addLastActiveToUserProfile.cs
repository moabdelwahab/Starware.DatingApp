using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Starware.DatingApp.Persistence.Migrations
{
    public partial class addLastActiveToUserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastActive",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "PublicId",
                table: "Photo",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastActive",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "PublicId",
                table: "Photo",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
