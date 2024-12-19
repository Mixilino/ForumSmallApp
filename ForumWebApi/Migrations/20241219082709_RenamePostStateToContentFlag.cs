using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumWebApi.Migrations
{
    public partial class RenamePostStateToContentFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostState",
                table: "Posts",
                newName: "ContentFlag");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContentFlag",
                table: "Posts",
                newName: "PostState");
        }
    }
}
