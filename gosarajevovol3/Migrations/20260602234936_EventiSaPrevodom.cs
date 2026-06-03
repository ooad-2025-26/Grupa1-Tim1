using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gosarajevovol3.Migrations
{
    /// <inheritdoc />
    public partial class EventiSaPrevodom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventDescriptionEn",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventNameEn",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationAddressEn",
                table: "Events",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventDescriptionEn",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventNameEn",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "LocationAddressEn",
                table: "Events");
        }
    }
}
