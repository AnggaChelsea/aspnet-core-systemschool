using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetAngularAuthWebApi.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_SchoolClasses_SchoolClassId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_SchoolClassId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "SchoolClassId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "AddressSchool",
                table: "SchoolClasses");

            migrationBuilder.RenameColumn(
                name: "DescriptionCourse",
                table: "Teachers",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "LiveTimeStart",
                table: "SchoolClasses",
                newName: "TimeStart");

            migrationBuilder.RenameColumn(
                name: "LiveTimeClose",
                table: "SchoolClasses",
                newName: "TimeClose");

            migrationBuilder.CreateTable(
                name: "Mapel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NamaMapel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsTugas = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mapel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mapel_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jadwal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JamMapel = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KelasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MapelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jadwal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jadwal_Mapel_MapelId",
                        column: x => x.MapelId,
                        principalTable: "Mapel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jadwal_SchoolClasses_SchoolClassId",
                        column: x => x.SchoolClassId,
                        principalTable: "SchoolClasses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jadwal_MapelId",
                table: "Jadwal",
                column: "MapelId");

            migrationBuilder.CreateIndex(
                name: "IX_Jadwal_SchoolClassId",
                table: "Jadwal",
                column: "SchoolClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Mapel_TeacherId",
                table: "Mapel",
                column: "TeacherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jadwal");

            migrationBuilder.DropTable(
                name: "Mapel");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Teachers",
                newName: "DescriptionCourse");

            migrationBuilder.RenameColumn(
                name: "TimeStart",
                table: "SchoolClasses",
                newName: "LiveTimeStart");

            migrationBuilder.RenameColumn(
                name: "TimeClose",
                table: "SchoolClasses",
                newName: "LiveTimeClose");

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolClassId",
                table: "Teachers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "AddressSchool",
                table: "SchoolClasses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_SchoolClassId",
                table: "Teachers",
                column: "SchoolClassId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_SchoolClasses_SchoolClassId",
                table: "Teachers",
                column: "SchoolClassId",
                principalTable: "SchoolClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
