using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAPGEMINI_CROPDEAL.Migrations
{
    /// <inheritdoc />
    public partial class addedisavailable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Crops",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Crops");
        }
    }
}
