using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Emenu");

            migrationBuilder.CreateTable(
                name: "Attributes",
                schema: "Emenu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Variants",
                schema: "Emenu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Variants_Attributes_AttributeId",
                        column: x => x.AttributeId,
                        principalSchema: "Emenu",
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                schema: "Emenu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdditionalProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "Emenu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Images_MainImageId",
                        column: x => x.MainImageId,
                        principalSchema: "Emenu",
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductLanguages",
                schema: "Emenu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    languageCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductLanguages_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Emenu",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariants",
                schema: "Emenu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VariantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVariants_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Emenu",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductVariants_Variants_VariantId",
                        column: x => x.VariantId,
                        principalSchema: "Emenu",
                        principalTable: "Variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VariantImages",
                schema: "Emenu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductVariantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariantImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VariantImages_Images_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "Emenu",
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VariantImages_ProductVariants_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalSchema: "Emenu",
                        principalTable: "ProductVariants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_AdditionalProductId",
                schema: "Emenu",
                table: "Images",
                column: "AdditionalProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLanguages_ProductId",
                schema: "Emenu",
                table: "ProductLanguages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_MainImageId",
                schema: "Emenu",
                table: "Products",
                column: "MainImageId",
                unique: true,
                filter: "[MainImageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_ProductId",
                schema: "Emenu",
                table: "ProductVariants",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_VariantId",
                schema: "Emenu",
                table: "ProductVariants",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantImages_ImageId",
                schema: "Emenu",
                table: "VariantImages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantImages_ProductVariantId",
                schema: "Emenu",
                table: "VariantImages",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Variants_AttributeId",
                schema: "Emenu",
                table: "Variants",
                column: "AttributeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products_AdditionalProductId",
                schema: "Emenu",
                table: "Images",
                column: "AdditionalProductId",
                principalSchema: "Emenu",
                principalTable: "Products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Products_AdditionalProductId",
                schema: "Emenu",
                table: "Images");

            migrationBuilder.DropTable(
                name: "ProductLanguages",
                schema: "Emenu");

            migrationBuilder.DropTable(
                name: "VariantImages",
                schema: "Emenu");

            migrationBuilder.DropTable(
                name: "ProductVariants",
                schema: "Emenu");

            migrationBuilder.DropTable(
                name: "Variants",
                schema: "Emenu");

            migrationBuilder.DropTable(
                name: "Attributes",
                schema: "Emenu");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "Emenu");

            migrationBuilder.DropTable(
                name: "Images",
                schema: "Emenu");
        }
    }
}
