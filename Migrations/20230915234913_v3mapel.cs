using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetAngularAuthWebApi.Migrations
{
    /// <inheritdoc />
    public partial class v3mapel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jadwal_Mapel_MapelId",
                table: "Jadwal");

            migrationBuilder.DropForeignKey(
                name: "FK_Jadwal_SchoolClasses_SchoolClassId",
                table: "Jadwal");

            migrationBuilder.DropForeignKey(
                name: "FK_Mapel_Teachers_TeacherId",
                table: "Mapel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mapel",
                table: "Mapel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Jadwal",
                table: "Jadwal");

            migrationBuilder.RenameTable(
                name: "Mapel",
                newName: "Mapels");

            migrationBuilder.RenameTable(
                name: "Jadwal",
                newName: "Jadwals");

            migrationBuilder.RenameIndex(
                name: "IX_Mapel_TeacherId",
                table: "Mapels",
                newName: "IX_Mapels_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Jadwal_SchoolClassId",
                table: "Jadwals",
                newName: "IX_Jadwals_SchoolClassId");

            migrationBuilder.RenameIndex(
                name: "IX_Jadwal_MapelId",
                table: "Jadwals",
                newName: "IX_Jadwals_MapelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mapels",
                table: "Mapels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Jadwals",
                table: "Jadwals",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jadwals_Mapels_MapelId",
                table: "Jadwals",
                column: "MapelId",
                principalTable: "Mapels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jadwals_SchoolClasses_SchoolClassId",
                table: "Jadwals",
                column: "SchoolClassId",
                principalTable: "SchoolClasses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mapels_Teachers_TeacherId",
                table: "Mapels",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jadwals_Mapels_MapelId",
                table: "Jadwals");

            migrationBuilder.DropForeignKey(
                name: "FK_Jadwals_SchoolClasses_SchoolClassId",
                table: "Jadwals");

            migrationBuilder.DropForeignKey(
                name: "FK_Mapels_Teachers_TeacherId",
                table: "Mapels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mapels",
                table: "Mapels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Jadwals",
                table: "Jadwals");

            migrationBuilder.RenameTable(
                name: "Mapels",
                newName: "Mapel");

            migrationBuilder.RenameTable(
                name: "Jadwals",
                newName: "Jadwal");

            migrationBuilder.RenameIndex(
                name: "IX_Mapels_TeacherId",
                table: "Mapel",
                newName: "IX_Mapel_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Jadwals_SchoolClassId",
                table: "Jadwal",
                newName: "IX_Jadwal_SchoolClassId");

            migrationBuilder.RenameIndex(
                name: "IX_Jadwals_MapelId",
                table: "Jadwal",
                newName: "IX_Jadwal_MapelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mapel",
                table: "Mapel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Jadwal",
                table: "Jadwal",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jadwal_Mapel_MapelId",
                table: "Jadwal",
                column: "MapelId",
                principalTable: "Mapel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jadwal_SchoolClasses_SchoolClassId",
                table: "Jadwal",
                column: "SchoolClassId",
                principalTable: "SchoolClasses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mapel_Teachers_TeacherId",
                table: "Mapel",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
