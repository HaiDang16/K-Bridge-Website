using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace K_Bridge.Migrations
{
    public partial class FixTimeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Users",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "DeleteAt",
                table: "Users",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Posts",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "DeleteAt",
                table: "Posts",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Posts",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Forums",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "DeleteAt",
                table: "Forums",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Forums",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Admin_Accounts",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "DeleteAt",
                table: "Admin_Accounts",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Admin_Accounts",
                newName: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserID",
                table: "Posts",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_UserID",
                table: "Posts",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UserID",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UserID",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Users",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Users",
                newName: "DeleteAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Users",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Posts",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Posts",
                newName: "DeleteAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Posts",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Forums",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Forums",
                newName: "DeleteAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Forums",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Admin_Accounts",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Admin_Accounts",
                newName: "DeleteAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Admin_Accounts",
                newName: "CreateAt");
        }
    }
}
