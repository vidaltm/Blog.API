using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Api.Migrations
{
    public partial class InclusaoEditor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NomeEditor",
                table: "DadosBlog",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeEditor",
                table: "DadosBlog");
        }
    }
}
