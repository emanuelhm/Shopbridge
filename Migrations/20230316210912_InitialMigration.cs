using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShopBridge.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Category_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Category_Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Product_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    PictureUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Product_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Tag_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Tag_Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryProduct",
                columns: table => new
                {
                    CategoriesCategory_Id = table.Column<int>(type: "integer", nullable: false),
                    ProductsProduct_Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProduct", x => new { x.CategoriesCategory_Id, x.ProductsProduct_Id });
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Categories_CategoriesCategory_Id",
                        column: x => x.CategoriesCategory_Id,
                        principalTable: "Categories",
                        principalColumn: "Category_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Products_ProductsProduct_Id",
                        column: x => x.ProductsProduct_Id,
                        principalTable: "Products",
                        principalColumn: "Product_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTag",
                columns: table => new
                {
                    ProductsProduct_Id = table.Column<int>(type: "integer", nullable: false),
                    TagsTag_Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTag", x => new { x.ProductsProduct_Id, x.TagsTag_Id });
                    table.ForeignKey(
                        name: "FK_ProductTag_Products_ProductsProduct_Id",
                        column: x => x.ProductsProduct_Id,
                        principalTable: "Products",
                        principalColumn: "Product_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTag_Tags_TagsTag_Id",
                        column: x => x.TagsTag_Id,
                        principalTable: "Tags",
                        principalColumn: "Tag_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Category_Id", "Name" },
                values: new object[] { 1, "Category 01" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Product_Id", "Description", "Name", "PictureUrl", "Price" },
                values: new object[] { 1, "Product 01", "Product 01", null, 1.0 });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Tag_Id", "Name" },
                values: new object[] { 1, "Tag 01" });

            migrationBuilder.InsertData(
                table: "CategoryProduct",
                columns: new[] { "CategoriesCategory_Id", "ProductsProduct_Id" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "ProductTag",
                columns: new[] { "ProductsProduct_Id", "TagsTag_Id" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_ProductsProduct_Id",
                table: "CategoryProduct",
                column: "ProductsProduct_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTag_TagsTag_Id",
                table: "ProductTag",
                column: "TagsTag_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryProduct");

            migrationBuilder.DropTable(
                name: "ProductTag");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
