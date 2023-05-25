using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieHunter.Migrations
{
    /// <inheritdoc />
    public partial class May : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvailableDate_Movies_MovieId",
                table: "AvailableDate");

            migrationBuilder.AddForeignKey(
                name: "FK_Available_Movie",
                table: "AvailableDate",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Available_Movie",
                table: "AvailableDate");

            migrationBuilder.AddForeignKey(
                name: "FK_AvailableDate_Movies_MovieId",
                table: "AvailableDate",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
