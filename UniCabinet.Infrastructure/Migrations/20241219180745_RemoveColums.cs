using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniCabinet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveColums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PracticalNumber",
                table: "Practicals");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "PointsCount",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "AutoExamThreshold",
                table: "DisciplineDetails");

            migrationBuilder.DropColumn(
                name: "ExamCount",
                table: "DisciplineDetails");

            migrationBuilder.DropColumn(
                name: "MinLecturesRequired",
                table: "DisciplineDetails");

            migrationBuilder.DropColumn(
                name: "MinPracticalsRequired",
                table: "DisciplineDetails");

            migrationBuilder.DropColumn(
                name: "PassCount",
                table: "DisciplineDetails");

            migrationBuilder.DropColumn(
                name: "SubExamCount",
                table: "DisciplineDetails");

            migrationBuilder.RenameColumn(
                name: "PointAvarage",
                table: "ExamResults",
                newName: "GradeAvarage");

            migrationBuilder.RenameColumn(
                name: "FinalPoint",
                table: "ExamResults",
                newName: "FinalGrade");

            migrationBuilder.AddColumn<string>(
                name: "PracticalName",
                table: "Practicals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Lectures",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PracticalName",
                table: "Practicals");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Lectures");

            migrationBuilder.RenameColumn(
                name: "GradeAvarage",
                table: "ExamResults",
                newName: "PointAvarage");

            migrationBuilder.RenameColumn(
                name: "FinalGrade",
                table: "ExamResults",
                newName: "FinalPoint");

            migrationBuilder.AddColumn<int>(
                name: "PracticalNumber",
                table: "Practicals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Number",
                table: "Lectures",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PointsCount",
                table: "Lectures",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "AutoExamThreshold",
                table: "DisciplineDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExamCount",
                table: "DisciplineDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinLecturesRequired",
                table: "DisciplineDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinPracticalsRequired",
                table: "DisciplineDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PassCount",
                table: "DisciplineDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubExamCount",
                table: "DisciplineDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
