using Microsoft.EntityFrameworkCore.Migrations;

namespace ForumWebApi.Migrations
{
    public partial class ChangePostStateToContentFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add new column
            migrationBuilder.AddColumn<int>(
                name: "ContentFlag",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Copy data from PostState to ContentFlag
            migrationBuilder.Sql(@"
                UPDATE Posts 
                SET ContentFlag = CASE 
                    WHEN PostState = 1 THEN 0  -- Verified -> Normal
                    ELSE 1  -- Others -> Flagged
                END");

            // Drop old column
            migrationBuilder.DropColumn(
                name: "PostState",
                table: "Posts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Add back old column
            migrationBuilder.AddColumn<int>(
                name: "PostState",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Copy data back
            migrationBuilder.Sql(@"
                UPDATE Posts 
                SET PostState = CASE 
                    WHEN ContentFlag = 0 THEN 1  -- Normal -> Verified
                    ELSE 0  -- Flagged -> In_Verification
                END");

            // Drop new column
            migrationBuilder.DropColumn(
                name: "ContentFlag",
                table: "Posts");
        }
    }
} 