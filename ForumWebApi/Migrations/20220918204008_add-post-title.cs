using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumWebApi.Migrations
{
    public partial class addposttitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostTitle",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostTitle",
                table: "Posts");
        }
    }
}
