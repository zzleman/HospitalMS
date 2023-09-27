using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeusMed.DataAccess.Migrations
{
    public partial class ProblemDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProblemDescription",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProblemDescription",
                table: "Appointments");
        }
    }
}
