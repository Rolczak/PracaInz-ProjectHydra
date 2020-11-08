using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHydraAPI.Migrations
{
    public partial class AddTimeAndCommentToClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Classes",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Time",
                table: "Classes",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Classes");
        }
    }
}
