using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHydraAPI.Migrations
{
    public partial class AddRankToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RankId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RankId",
                table: "AspNetUsers",
                column: "RankId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Ranks_RankId",
                table: "AspNetUsers",
                column: "RankId",
                principalTable: "Ranks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Ranks_RankId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RankId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RankId",
                table: "AspNetUsers");
        }
    }
}
