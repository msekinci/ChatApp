using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatApp.Migrations
{
    public partial class ChatTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Types",
                table: "Chats");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Chats",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Chats",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Chats");

            migrationBuilder.AddColumn<int>(
                name: "Types",
                table: "Chats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
