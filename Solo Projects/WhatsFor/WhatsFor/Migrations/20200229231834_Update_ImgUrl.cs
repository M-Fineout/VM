using Microsoft.EntityFrameworkCore.Migrations;

namespace WhatsFor.Migrations
{
    public partial class Update_ImgUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PicLink",
                table: "FoodPics");

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "FoodPics",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "FoodPics");

            migrationBuilder.AddColumn<string>(
                name: "PicLink",
                table: "FoodPics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
