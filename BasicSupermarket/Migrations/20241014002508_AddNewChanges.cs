using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasicSupermarket.Migrations
{
    /// <inheritdoc />
    public partial class AddNewChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContactInfo",
                table: "Manufacturers",
                newName: "Phone");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Manufacturers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Manufacturers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Manufacturers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Manufacturers");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Manufacturers",
                newName: "ContactInfo");
        }
    }
}
