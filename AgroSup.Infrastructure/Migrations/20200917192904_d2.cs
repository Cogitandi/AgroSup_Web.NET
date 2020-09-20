using Microsoft.EntityFrameworkCore.Migrations;

namespace AgroSup.Infrastructure.Migrations
{
    public partial class d2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parcels_Fields_FieldId",
                table: "Parcels");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcels_Fields_FieldId",
                table: "Parcels",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parcels_Fields_FieldId",
                table: "Parcels");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcels_Fields_FieldId",
                table: "Parcels",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
