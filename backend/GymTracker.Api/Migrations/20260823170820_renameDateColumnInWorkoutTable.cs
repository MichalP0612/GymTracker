using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class renameDateColumnInWorkoutTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MuscleGroup",
                table: "Workouts",
                newName: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Workouts",
                newName: "MuscleGroup");
        }
    }
}
