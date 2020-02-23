using Microsoft.EntityFrameworkCore.Migrations;

namespace WhatsFor.Migrations
{
    public partial class FoodPics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllPosts_FoodPic_FoodPicId",
                table: "AllPosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodPic",
                table: "FoodPic");

            migrationBuilder.RenameTable(
                name: "FoodPic",
                newName: "FoodPics");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodPics",
                table: "FoodPics",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AllPosts_FoodPics_FoodPicId",
                table: "AllPosts",
                column: "FoodPicId",
                principalTable: "FoodPics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllPosts_FoodPics_FoodPicId",
                table: "AllPosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodPics",
                table: "FoodPics");

            migrationBuilder.RenameTable(
                name: "FoodPics",
                newName: "FoodPic");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodPic",
                table: "FoodPic",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AllPosts_FoodPic_FoodPicId",
                table: "AllPosts",
                column: "FoodPicId",
                principalTable: "FoodPic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
