using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartJobPreppAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCompanyToJobDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "JobDescriptions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company",
                table: "JobDescriptions");
        }
    }
}
