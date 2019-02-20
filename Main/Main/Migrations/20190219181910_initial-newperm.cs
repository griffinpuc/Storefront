using Microsoft.EntityFrameworkCore.Migrations;

namespace Main.Migrations
{
    public partial class initialnewperm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "permission",
                table: "DBCreds",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "permission",
                table: "DBCreds");
        }
    }
}
