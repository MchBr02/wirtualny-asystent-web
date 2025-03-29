using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Client_ui.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseThird : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "ExerciseVolume",
                table: "Exercises",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "ExerciseDTOs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExerciseQuality = table.Column<int>(type: "int", nullable: false),
                    ExeciseWeight = table.Column<float>(type: "real", nullable: false),
                    ExerciseReps = table.Column<int>(type: "int", nullable: false),
                    ExerciseSets = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExerciseTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    WorkoutId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseDTOs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseDTOs_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseDTOs_WorkoutId",
                table: "ExerciseDTOs",
                column: "WorkoutId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseDTOs");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "ExerciseVolume",
                table: "Exercises");
        }
    }
}
