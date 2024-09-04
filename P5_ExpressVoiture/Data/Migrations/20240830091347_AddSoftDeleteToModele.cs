using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P5_ExpressVoiture.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeleteToModele : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SoftDelete",
                table: "Modeles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoftDelete",
                table: "Modeles");
        }
    }
}
