using Microsoft.EntityFrameworkCore.Migrations;
using SovosProjectTest.Domain.Entities;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SovosProjectTest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    StockQuantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("1d9f84d7-6941-41d8-b18a-040d65a6b75a"), "Category 2", "Brand A Product 2", 200.0m, 30 },
                    { new Guid("7f2345cd-8c68-4f0f-917a-b909f98d7f84"), "Category 3", "Brand A Product 3", 150.0m, 25 },
                    { new Guid("673d67fb-bf0a-4cfc-8382-29e79e5e85c0"), "Category 4", "Brand A Product 4", 250.0m, 35 },
                    { new Guid("a29b4d5d-96e9-4cb9-b94d-f1ec3656605d"), "Category 5", "Brand A Product 5", 300.0m, 40 },
                    { new Guid("febc0871-1680-46bc-9a76-bf21da07979e"), "Category 2", "Brand B Product 1", 120.0m, 15 },
                    { new Guid("605cde5c-5104-4189-8618-9f50de3a5e88"), "Category 3", "Brand B Product 2", 130.0m, 20 },
                    { new Guid("00a43c5c-fb43-485a-9b64-c9c05d5918fe"), "Category 4", "Brand B Product 3", 140.0m, 25 },
                    { new Guid("7b5e52a7-f8da-4f5b-9630-bde8d88a3fef"), "Category 5", "Brand B Product 4", 220.0m, 30 },
                    { new Guid("3b8466e3-f4f1-4ac0-8f5f-d9974ac6273f"), "Category 1", "Brand B Product 5", 270.0m, 35 },
                    { new Guid("ce1f9b6e-43e7-489d-a9ae-6a3d85fd8031"), "Category 2", "Brand C Product 1", 180.0m, 25 },
                    { new Guid("7fbb5853-cb23-4211-b2a3-53f8ed303a61"), "Category 3", "Brand C Product 2", 190.0m, 20 },
                    { new Guid("c70b9aef-b1f0-4b96-94fd-c36b7b91d441"), "Category 4", "Brand C Product 3", 210.0m, 30 },
                    { new Guid("c6840d77-0061-497f-9234-cdb77f2304ea"), "Category 5", "Brand C Product 4", 230.0m, 40 },
                    { new Guid("1a5a4e9d-7755-4c0f-bcd1-c767f30e9441"), "Category 1", "Brand C Product 5", 260.0m, 45 },
                    { new Guid("643fbdb5-764f-4970-9c2a-3d0842f036fa"), "Category 1", "Brand D Product 1", 110.0m, 22 },
                    { new Guid("8f1e9f35-d619-45f6-b8fc-6876d7c9192b"), "Category 2", "Brand D Product 2", 200.0m, 28 },
                    { new Guid("ed539b07-80c3-4428-9f9e-036fcfe69e49"), "Category 3", "Brand D Product 3", 250.0m, 33 },
                    { new Guid("b2349b64-cbfa-4e02-bf84-eaf1b5fe4731"), "Category 4", "Brand D Product 4", 280.0m, 39 },
                    { new Guid("8f96fbd7-62b2-4975-b96b-510975f999d1"), "Category 5", "Brand D Product 5", 300.0m, 50 },
                    { new Guid("5b23f255-b850-44e5-9995-d05690e4c53b"), "Category 2", "Brand E Product 1", 120.0m, 20 },
                    { new Guid("f892377d-ea84-4de1-95ff-65994b365d0a"), "Category 3", "Brand E Product 2", 130.0m, 22 },
                    { new Guid("bb19ab2f-fc9b-452a-8a8f-cce6181e7a3e"), "Category 4", "Brand E Product 3", 140.0m, 30 },
                    { new Guid("bd4e59b0-cff6-4ea7-92e0-28ab0908e96f"), "Category 5", "Brand E Product 4", 150.0m, 35 },
                    { new Guid("9b0f8b97-2447-4232-b4d6-4f3c70216aeb"), "Category 1", "Brand E Product 5", 160.0m, 40 },
                    { new Guid("33fbdada-0d13-40a9-a4e5-6ac42398d1ac"), "Category 2", "Brand F Product 1", 170.0m, 25 },
                    { new Guid("209a6191-8f7f-42e0-8a6e-1c93a292e2b4"), "Category 3", "Brand F Product 2", 180.0m, 30 },
                    { new Guid("643978a7-cd42-4700-8f80-9ec8a55d55b1"), "Category 4", "Brand F Product 3", 190.0m, 33 },
                    { new Guid("f557d5db-0228-4d45-96c6-8cf8f421e14f"), "Category 5", "Brand F Product 4", 200.0m, 38 },
                    { new Guid("d2fae043-cd73-406d-beb4-1d9db17925e5"), "Category 1", "Brand F Product 5", 210.0m, 45 },
                    { new Guid("8f66fa80-b1ae-421b-a685-5058f27cda91"), "Category 2", "Brand G Product 1", 220.0m, 28 },
                    { new Guid("f34cc92f-bd2a-401f-8a7a-2432d1072b3b"), "Category 3", "Brand G Product 2", 230.0m, 35 },
                    { new Guid("a2ba2d1e-b42d-47fd-b44f-79ec0c0a570f"), "Category 4", "Brand G Product 3", 240.0m, 40 },
                    { new Guid("e3e2a7f9-92fc-45f3-b229-c69f5e455b36"), "Category 5", "Brand G Product 4", 250.0m, 42 },
                    { new Guid("4e85a51a-8360-426d-9ff9-438849f22d79"), "Category 1", "Brand G Product 5", 260.0m, 46 },
                    { new Guid("17e7b408-c4f1-488e-a7f9-8a0c03fcad96"), "Category 2", "Brand H Product 1", 270.0m, 30 },
                    { new Guid("b4108b7e-79ff-4306-bd8f-b679f536c8c6"), "Category 3", "Brand H Product 2", 280.0m, 35 },
                    { new Guid("3de1c245-e2ea-4d6e-a08b-f62c1f8de89e"), "Category 4", "Brand H Product 3", 290.0m, 40 },
                    { new Guid("8c56f85b-4b18-460d-bc92-34c2b6e9e9b3"), "Category 5", "Brand H Product 4", 300.0m, 45 },
                    { new Guid("5c6feff4-32be-47e4-9b51-0ed19033b5d3"), "Category 1", "Brand H Product 5", 310.0m, 50 },
                    { new Guid("b0c5b9d3-e0b7-4888-a052-e6c6ffb9a8d7"), "Category 1", "Brand I Product 1", 100.0m, 20 },
                    { new Guid("cb71e0fa-60e5-43da-8e95-4fc11b93e290"), "Category 2", "Brand I Product 2", 110.0m, 25 },
                    { new Guid("d7bdfc29-1d56-4743-bf98-c2b37b349be6"), "Category 3", "Brand I Product 3", 120.0m, 30 },
                    { new Guid("41f7bc02-d87f-4009-b9a9-c0f30e2a65c1"), "Category 4", "Brand I Product 4", 130.0m, 35 },
                    { new Guid("f3f8e8b9-3f84-44c2-bf6a-d28f0f2b8f72"), "Category 5", "Brand I Product 5", 140.0m, 40 },
                    { new Guid("9f9a3e90-b9ad-4e56-9e85-bd77274e801d"), "Category 1", "Brand J Product 1", 150.0m, 45 },
                    { new Guid("c972a635-cd66-4b50-89a2-07c2271d15cf"), "Category 2", "Brand J Product 2", 160.0m, 50 },
                    { new Guid("ec9a23b1-bdf3-4d07-bf53-84d5be3542a5"), "Category 3", "Brand J Product 3", 170.0m, 55 },
                    { new Guid("c0a2fdc0-d5d5-47a2-a0ea-f6573b47a7d1"), "Category 4", "Brand J Product 4", 180.0m, 60 },
                    { new Guid("e1be7a4b-d704-4929-bcc9-50825bb35b4a"), "Category 5", "Brand J Product 5", 190.0m, 65 },
                    { new Guid("fc9a16f4-745b-44ff-8915-dcd8ad57c92d"), "Category 1", "Brand K Product 1", 200.0m, 70 },
                    { new Guid("742a6f6a-d8d1-463f-95b8-bd4db68b6b3b"), "Category 2", "Brand K Product 2", 210.0m, 75 }
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
