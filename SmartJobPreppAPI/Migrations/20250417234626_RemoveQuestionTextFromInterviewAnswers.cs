using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartJobPreppAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveQuestionTextFromInterviewAnswers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Question",
                table: "InterviewAnswers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Question",
                table: "InterviewAnswers",
                type: "nvarchar(max)", 
                nullable: true);
        }

    }
}
