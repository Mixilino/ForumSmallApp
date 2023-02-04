using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumWebApi.Migrations
{
    public partial class poststate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostState",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostState",
                table: "Posts");
        }
    }
}
