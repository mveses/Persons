using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persons.Migrations
{
    /// <inheritdoc />
    public partial class AddSpouseFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_User_SpouseId",
                table: "User",
                column: "SpouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_SpouseId",
                table: "User",
                column: "SpouseId",
                principalTable: "User",
                principalColumn: "Id");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_User_SpouseId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_SpouseId",
                table: "User");
        }
    }
}
