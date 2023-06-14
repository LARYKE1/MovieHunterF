using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieHunter.Migrations
{
    /// <inheritdoc />
    public partial class @try : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DateId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            
            migrationBuilder.CreateIndex(
                name: "IX_Reservations_DateId",
                table: "Reservations",
                column: "DateId");

            

           
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
