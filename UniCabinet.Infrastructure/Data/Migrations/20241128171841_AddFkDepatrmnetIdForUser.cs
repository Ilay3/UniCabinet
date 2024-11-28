using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniCabinet.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFkDepatrmnetIdForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_DepatrmnetId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "DepatrmnetId",
                table: "Users",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_DepatrmnetId",
                table: "Users",
                newName: "IX_Users_DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Users",
                newName: "DepatrmnetId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                newName: "IX_Users_DepatrmnetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_DepatrmnetId",
                table: "Users",
                column: "DepatrmnetId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
