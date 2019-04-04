using Microsoft.EntityFrameworkCore.Migrations;

namespace Main.Migrations
{
    public partial class contact2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Message",
                table: "DBContacts",
                newName: "Phone");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "DBContacts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "DBContacts");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "DBContacts",
                newName: "Message");
        }
    }
}
