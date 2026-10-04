using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddWeightUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Unit",
                table: "WorkoutSets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "WorkoutSets");
        }
    }
}
