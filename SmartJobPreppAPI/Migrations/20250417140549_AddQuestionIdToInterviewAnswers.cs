using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartJobPreppAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestionIdToInterviewAnswers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "InterviewAnswers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InterviewAnswers_QuestionId",
                table: "InterviewAnswers",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewAnswers_Questions_QuestionId",
                table: "InterviewAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterviewAnswers_Questions_QuestionId",
                table: "InterviewAnswers");

            migrationBuilder.DropIndex(
                name: "IX_InterviewAnswers_QuestionId",
                table: "InterviewAnswers");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "InterviewAnswers");

        }
    }
}
