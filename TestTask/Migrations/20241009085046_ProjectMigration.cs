using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTask.Migrations
{
    /// <inheritdoc />
    public partial class ProjectMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "developers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_developers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DeveloperId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_games_developers_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "developers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "gametogengres",
                columns: table => new
                {
                    GameId = table.Column<Guid>(type: "uuid", nullable: false),
                    GenreId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gametogengres", x => new { x.GameId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_gametogengres_games_GameId",
                        column: x => x.GameId,
                        principalTable: "games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gametogengres_genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_games_DeveloperId",
                table: "games",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_gametogengres_GenreId",
                table: "gametogengres",
                column: "GenreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gametogengres");

            migrationBuilder.DropTable(
                name: "games");

            migrationBuilder.DropTable(
                name: "genres");

            migrationBuilder.DropTable(
                name: "developers");
        }
    }
}
