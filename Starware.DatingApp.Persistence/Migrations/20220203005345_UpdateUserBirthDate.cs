using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Starware.DatingApp.Persistence.Migrations
{
    public partial class UpdateUserBirthDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Birthdate",
                table: "Users",
                newName: "BirthDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "Users",
                newName: "Birthdate");
        }
    }
}
