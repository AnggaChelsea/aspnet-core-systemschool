using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetAngularAuthWebApi.Migrations
{
    /// <inheritdoc />
    public partial class addfileupload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameFileUpload",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameFileUpload",
                table: "Roles");
        }
    }
}
