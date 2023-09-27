using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeusMed.DataAccess.Migrations
{
    public partial class AvailableTimeTable3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvailableTimes");

            migrationBuilder.RenameColumn(
                name: "Fullname",
                table: "Appointments",
                newName: "Surname");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Appointments",
                newName: "Fullname");

            migrationBuilder.CreateTable(
                name: "AvailableTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvailableTimes_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvailableTimes_DoctorId",
                table: "AvailableTimes",
                column: "DoctorId");
        }
    }
}
