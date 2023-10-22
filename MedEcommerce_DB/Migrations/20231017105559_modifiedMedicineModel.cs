using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedEcommerce_DB.Migrations
{
    public partial class modifiedMedicineModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Seller",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seller",
                table: "Medicines");
        }
    }
}
