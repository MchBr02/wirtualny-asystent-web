using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Client_ui.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkoutName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkoutQuality = table.Column<int>(type: "int", nullable: false),
                    WorkoutTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    WorkoutDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkoutFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExcersiseQuality = table.Column<int>(type: "int", nullable: false),
                    ExeciseWeight = table.Column<float>(type: "real", nullable: false),
                    ExerciseReps = table.Column<int>(type: "int", nullable: false),
                    ExerciseSets = table.Column<int>(type: "int", nullable: false),
                    ExerciseTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_Workouts_WorkoutFK",
                        column: x => x.WorkoutFK,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_WorkoutFK",
                table: "Exercises",
                column: "WorkoutFK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Workouts");
        }
    }
}
