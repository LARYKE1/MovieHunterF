using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieHunter.Migrations
{
    /// <inheritdoc />
    public partial class Final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeatsSeatId",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    SeatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsReserved = table.Column<bool>(type: "bit", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.SeatId);
                    table.ForeignKey(
                        name: "FK_Seats_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_SeatsSeatId",
                table: "Reservations",
                column: "SeatsSeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_MovieId",
                table: "Seats",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Seats_SeatsSeatId",
                table: "Reservations",
                column: "SeatsSeatId",
                principalTable: "Seats",
                principalColumn: "SeatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Seats_SeatsSeatId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_SeatsSeatId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "SeatsSeatId",
                table: "Reservations");
        }
    }
}
