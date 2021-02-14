using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Basics2.Homework.DataAccess.MSSQL.Migrations
{
    public partial class CreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: false),
                    volume = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Showcases",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: false),
                    volume = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    create_date = table.Column<DateTime>(type: "date", nullable: false),
                    remove_date = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Showcases", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Showcase_Product",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    showcase_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    product_count = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    product_cost = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Showcase_Product", x => x.id);
                    table.ForeignKey(
                        name: "FK_Showcase_Product_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Showcase_Product_Showcases_showcase_id",
                        column: x => x.showcase_id,
                        principalTable: "Showcases",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Showcase_Product_product_id",
                table: "Showcase_Product",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Showcase_Product_showcase_id",
                table: "Showcase_Product",
                column: "showcase_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Showcase_Product");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Showcases");
        }
    }
}
