using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniCabinet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_CourseId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "DisciplineDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineDetails_CourseId",
                table: "DisciplineDetails",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplineDetails_Courses_CourseId",
                table: "DisciplineDetails",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisciplineDetails_Courses_CourseId",
                table: "DisciplineDetails");

            migrationBuilder.DropIndex(
                name: "IX_DisciplineDetails_CourseId",
                table: "DisciplineDetails");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "DisciplineDetails");
        }
    }
}
