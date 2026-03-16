using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAPGEMINI_CROPDEAL.Migrations
{
    /// <inheritdoc />
    public partial class ADDEDADMIN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Farmers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Farmers");
        }
    }
}
