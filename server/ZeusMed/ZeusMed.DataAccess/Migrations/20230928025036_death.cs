using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeusMed.DataAccess.Migrations
{
    public partial class death : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deaths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeathTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeathPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CauseOfDeath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChosenGender = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deaths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deaths_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deaths_DoctorId",
                table: "Deaths",
                column: "DoctorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deaths");
        }
    }
}
