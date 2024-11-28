using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SovosProjectTest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRowVersionToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RowVersion",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.Sql(@"
                UPDATE Products
                SET RowVersion = (hex(randomblob(4)) || '-' || hex(randomblob(2)) || '-4' ||
                          substr(hex(randomblob(2)), 2) || '-' || substr('89ab', abs(random() % 4) + 1, 1) ||
                          substr(hex(randomblob(2)), 2) || '-' || hex(randomblob(6)))");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Products");
        }
    }
}
