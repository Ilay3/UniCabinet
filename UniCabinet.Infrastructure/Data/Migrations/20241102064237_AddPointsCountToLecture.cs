using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniCabinet.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPointsCountToLecture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PointsCount",
                table: "Lectures",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PointsCount",
                table: "Lectures");
        }
    }
}
