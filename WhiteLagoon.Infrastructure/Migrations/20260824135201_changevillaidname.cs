using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhiteLagoon.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changevillaidname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillaNumbers_Villas_Id",
                table: "VillaNumbers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Villas",
                newName: "VillaId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "VillaNumbers",
                newName: "VillaId");

            migrationBuilder.RenameIndex(
                name: "IX_VillaNumbers_Id",
                table: "VillaNumbers",
                newName: "IX_VillaNumbers_VillaId");

            migrationBuilder.AddForeignKey(
                name: "FK_VillaNumbers_Villas_VillaId",
                table: "VillaNumbers",
                column: "VillaId",
                principalTable: "Villas",
                principalColumn: "VillaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillaNumbers_Villas_VillaId",
                table: "VillaNumbers");

            migrationBuilder.RenameColumn(
                name: "VillaId",
                table: "Villas",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "VillaId",
                table: "VillaNumbers",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_VillaNumbers_VillaId",
                table: "VillaNumbers",
                newName: "IX_VillaNumbers_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VillaNumbers_Villas_Id",
                table: "VillaNumbers",
                column: "Id",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
