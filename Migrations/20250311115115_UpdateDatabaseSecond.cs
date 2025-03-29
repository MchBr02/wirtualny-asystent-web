using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Client_ui.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseSecond : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "WorkoutVolume",
                table: "Workouts",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkoutVolume",
                table: "Workouts");
        }
    }
}
