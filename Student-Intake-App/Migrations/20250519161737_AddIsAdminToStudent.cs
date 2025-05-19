using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Intake_App.Migrations
{
    /// <inheritdoc />
    public partial class AddIsAdminToStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Students");
        }
    }
}
