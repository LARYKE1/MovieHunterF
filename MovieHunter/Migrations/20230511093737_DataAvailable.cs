using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieHunter.Migrations
{
    /// <inheritdoc />
    public partial class DataAvailable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DateId",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AvailableDate",
                columns: table => new
                {
                    DateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableDate", x => x.DateId);
                    table.ForeignKey(
                        name: "FK_AvailableDate_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_DateId",
                table: "Reservations",
                column: "DateId");

            migrationBuilder.CreateIndex(
                name: "IX_AvailableDate_MovieId",
                table: "AvailableDate",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AvailableDate_DateId",
                table: "Reservations",
                column: "DateId",
                principalTable: "AvailableDate",
                principalColumn: "DateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AvailableDate_DateId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "AvailableDate");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_DateId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "DateId",
                table: "Reservations");
        }
    }
}
