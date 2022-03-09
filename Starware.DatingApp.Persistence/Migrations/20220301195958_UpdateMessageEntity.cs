using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Starware.DatingApp.Persistence.Migrations
{
    public partial class UpdateMessageEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateSent",
                table: "Message");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Message",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Message",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Message",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Message",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Message");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSent",
                table: "Message",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
