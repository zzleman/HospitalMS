using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeusMed.DataAccess.Migrations
{
    public partial class birth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Births",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChosenGender = table.Column<int>(type: "int", nullable: false),
                    ChosenBloodGroup = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Births", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Births_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Births_DoctorId",
                table: "Births",
                column: "DoctorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Births");
        }
    }
}
