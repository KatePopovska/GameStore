using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Host.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "catalog_genre_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "catalog_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "catalog_platform_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "CatalogGenre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Genre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogGenre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogPlatform",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Platform = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogPlatform", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogPublisher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Publisher = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogPublisher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Catalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    InStock = table.Column<bool>(type: "boolean", nullable: false),
                    PublisherId = table.Column<int>(type: "integer", nullable: false),
                    GenreId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Catalog_CatalogGenre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "CatalogGenre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Catalog_CatalogPublisher_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "CatalogPublisher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatalogGamePlatforms",
                columns: table => new
                {
                    CatalogGameId = table.Column<int>(type: "integer", nullable: false),
                    CatalogPlatformId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogGamePlatforms", x => new { x.CatalogGameId, x.CatalogPlatformId });
                    table.ForeignKey(
                        name: "FK_CatalogGamePlatforms_Catalog_CatalogGameId",
                        column: x => x.CatalogGameId,
                        principalTable: "Catalog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatalogGamePlatforms_CatalogPlatform_CatalogPlatformId",
                        column: x => x.CatalogPlatformId,
                        principalTable: "CatalogPlatform",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_GenreId",
                table: "Catalog",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_PublisherId",
                table: "Catalog",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogGamePlatforms_CatalogPlatformId",
                table: "CatalogGamePlatforms",
                column: "CatalogPlatformId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogGamePlatforms");

            migrationBuilder.DropTable(
                name: "Catalog");

            migrationBuilder.DropTable(
                name: "CatalogPlatform");

            migrationBuilder.DropTable(
                name: "CatalogGenre");

            migrationBuilder.DropTable(
                name: "CatalogPublisher");

            migrationBuilder.DropSequence(
                name: "catalog_genre_hilo");

            migrationBuilder.DropSequence(
                name: "catalog_hilo");

            migrationBuilder.DropSequence(
                name: "catalog_platform_hilo");
        }
    }
}
