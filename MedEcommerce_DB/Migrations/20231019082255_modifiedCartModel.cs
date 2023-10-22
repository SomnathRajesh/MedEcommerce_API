using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedEcommerce_DB.Migrations
{
    public partial class modifiedCartModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Medicines_MedId",
                table: "Carts",
                column: "MedId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Medicines_MedId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_MedId",
                table: "Carts");
        }
    }
}
