using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourWebsite.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddImageInTours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Tours");
        }
    }
}
