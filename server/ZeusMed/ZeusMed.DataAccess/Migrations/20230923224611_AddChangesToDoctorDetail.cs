using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeusMed.DataAccess.Migrations
{
    public partial class AddChangesToDoctorDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorDetails_Doctors_Id",
                table: "DoctorDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorDetails",
                table: "DoctorDetails");

            migrationBuilder.RenameTable(
                name: "DoctorDetails",
                newName: "DoctorDetail");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorDetail",
                table: "DoctorDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorDetail_Doctors_Id",
                table: "DoctorDetail",
                column: "Id",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorDetail_Doctors_Id",
                table: "DoctorDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorDetail",
                table: "DoctorDetail");

            migrationBuilder.RenameTable(
                name: "DoctorDetail",
                newName: "DoctorDetails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorDetails",
                table: "DoctorDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorDetails_Doctors_Id",
                table: "DoctorDetails",
                column: "Id",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
