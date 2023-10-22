using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedEcommerce_DB.Migrations
{
    public partial class changeinMedicineModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Medicines",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Medicines");
        }
    }
}
