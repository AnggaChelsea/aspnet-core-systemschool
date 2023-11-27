using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetAngularAuthWebApi.Migrations
{
    /// <inheritdoc />
    public partial class salesreporting_ral_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExcelTams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    No = table.Column<int>(type: "int", nullable: true),
                    NoFrame = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dateproduction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vehicle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConversionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BodyBuilder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoSkrb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dealer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cabang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelTams", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExcelTams");
        }
    }
}
