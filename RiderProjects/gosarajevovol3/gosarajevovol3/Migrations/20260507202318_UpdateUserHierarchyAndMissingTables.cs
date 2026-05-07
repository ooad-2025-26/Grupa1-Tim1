using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gosarajevovol3.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserHierarchyAndMissingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attraction_Coordinates_CoordinatesId",
                table: "Attraction");

            migrationBuilder.DropForeignKey(
                name: "FK_Attraction_SmartPlanner_SmartPlannerId",
                table: "Attraction");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Coordinates_CoordinatesId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_SmartPlanner_SmartPlannerId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Hospitality_SmartPlanner_SmartPlannerId",
                table: "Hospitality");

            migrationBuilder.DropForeignKey(
                name: "FK_Preference_RegisteredUser_RegisteredUserId",
                table: "Preference");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Hospitality_HospitalityId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_RegisteredUser_RegisteredUserId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_SmartPlanner_Preference_PreferenceId",
                table: "SmartPlanner");

            migrationBuilder.DropForeignKey(
                name: "FK_SmartPlanner_RegisteredUser_RegisteredUserId",
                table: "SmartPlanner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SmartPlanner",
                table: "SmartPlanner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Preference",
                table: "Preference");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Event",
                table: "Event");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attraction",
                table: "Attraction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegisteredUser",
                table: "RegisteredUser");

            migrationBuilder.RenameTable(
                name: "SmartPlanner",
                newName: "SmartPlanners");

            migrationBuilder.RenameTable(
                name: "Review",
                newName: "Reviews");

            migrationBuilder.RenameTable(
                name: "Preference",
                newName: "Preferences");

            migrationBuilder.RenameTable(
                name: "Event",
                newName: "Events");

            migrationBuilder.RenameTable(
                name: "Attraction",
                newName: "Attractions");

            migrationBuilder.RenameTable(
                name: "RegisteredUser",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_SmartPlanner_RegisteredUserId",
                table: "SmartPlanners",
                newName: "IX_SmartPlanners_RegisteredUserId");

            migrationBuilder.RenameIndex(
                name: "IX_SmartPlanner_PreferenceId",
                table: "SmartPlanners",
                newName: "IX_SmartPlanners_PreferenceId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_RegisteredUserId",
                table: "Reviews",
                newName: "IX_Reviews_RegisteredUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_HospitalityId",
                table: "Reviews",
                newName: "IX_Reviews_HospitalityId");

            migrationBuilder.RenameIndex(
                name: "IX_Preference_RegisteredUserId",
                table: "Preferences",
                newName: "IX_Preferences_RegisteredUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_SmartPlannerId",
                table: "Events",
                newName: "IX_Events_SmartPlannerId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_CoordinatesId",
                table: "Events",
                newName: "IX_Events_CoordinatesId");

            migrationBuilder.RenameIndex(
                name: "IX_Attraction_SmartPlannerId",
                table: "Attractions",
                newName: "IX_Attractions_SmartPlannerId");

            migrationBuilder.RenameIndex(
                name: "IX_Attraction_CoordinatesId",
                table: "Attractions",
                newName: "IX_Attractions_CoordinatesId");

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "Users",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SmartPlanners",
                table: "SmartPlanners",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Preferences",
                table: "Preferences",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attractions",
                table: "Attractions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Newsletters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    OperatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newsletters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Newsletters_Users_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Newsletters_OperatorId",
                table: "Newsletters",
                column: "OperatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attractions_Coordinates_CoordinatesId",
                table: "Attractions",
                column: "CoordinatesId",
                principalTable: "Coordinates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attractions_SmartPlanners_SmartPlannerId",
                table: "Attractions",
                column: "SmartPlannerId",
                principalTable: "SmartPlanners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Coordinates_CoordinatesId",
                table: "Events",
                column: "CoordinatesId",
                principalTable: "Coordinates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_SmartPlanners_SmartPlannerId",
                table: "Events",
                column: "SmartPlannerId",
                principalTable: "SmartPlanners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hospitality_SmartPlanners_SmartPlannerId",
                table: "Hospitality",
                column: "SmartPlannerId",
                principalTable: "SmartPlanners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Preferences_Users_RegisteredUserId",
                table: "Preferences",
                column: "RegisteredUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Hospitality_HospitalityId",
                table: "Reviews",
                column: "HospitalityId",
                principalTable: "Hospitality",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_RegisteredUserId",
                table: "Reviews",
                column: "RegisteredUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SmartPlanners_Preferences_PreferenceId",
                table: "SmartPlanners",
                column: "PreferenceId",
                principalTable: "Preferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SmartPlanners_Users_RegisteredUserId",
                table: "SmartPlanners",
                column: "RegisteredUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attractions_Coordinates_CoordinatesId",
                table: "Attractions");

            migrationBuilder.DropForeignKey(
                name: "FK_Attractions_SmartPlanners_SmartPlannerId",
                table: "Attractions");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Coordinates_CoordinatesId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_SmartPlanners_SmartPlannerId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Hospitality_SmartPlanners_SmartPlannerId",
                table: "Hospitality");

            migrationBuilder.DropForeignKey(
                name: "FK_Preferences_Users_RegisteredUserId",
                table: "Preferences");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Hospitality_HospitalityId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_RegisteredUserId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_SmartPlanners_Preferences_PreferenceId",
                table: "SmartPlanners");

            migrationBuilder.DropForeignKey(
                name: "FK_SmartPlanners_Users_RegisteredUserId",
                table: "SmartPlanners");

            migrationBuilder.DropTable(
                name: "Newsletters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SmartPlanners",
                table: "SmartPlanners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Preferences",
                table: "Preferences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attractions",
                table: "Attractions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "SmartPlanners",
                newName: "SmartPlanner");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Review");

            migrationBuilder.RenameTable(
                name: "Preferences",
                newName: "Preference");

            migrationBuilder.RenameTable(
                name: "Events",
                newName: "Event");

            migrationBuilder.RenameTable(
                name: "Attractions",
                newName: "Attraction");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "RegisteredUser");

            migrationBuilder.RenameIndex(
                name: "IX_SmartPlanners_RegisteredUserId",
                table: "SmartPlanner",
                newName: "IX_SmartPlanner_RegisteredUserId");

            migrationBuilder.RenameIndex(
                name: "IX_SmartPlanners_PreferenceId",
                table: "SmartPlanner",
                newName: "IX_SmartPlanner_PreferenceId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_RegisteredUserId",
                table: "Review",
                newName: "IX_Review_RegisteredUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_HospitalityId",
                table: "Review",
                newName: "IX_Review_HospitalityId");

            migrationBuilder.RenameIndex(
                name: "IX_Preferences_RegisteredUserId",
                table: "Preference",
                newName: "IX_Preference_RegisteredUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_SmartPlannerId",
                table: "Event",
                newName: "IX_Event_SmartPlannerId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_CoordinatesId",
                table: "Event",
                newName: "IX_Event_CoordinatesId");

            migrationBuilder.RenameIndex(
                name: "IX_Attractions_SmartPlannerId",
                table: "Attraction",
                newName: "IX_Attraction_SmartPlannerId");

            migrationBuilder.RenameIndex(
                name: "IX_Attractions_CoordinatesId",
                table: "Attraction",
                newName: "IX_Attraction_CoordinatesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SmartPlanner",
                table: "SmartPlanner",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Preference",
                table: "Preference",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Event",
                table: "Event",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attraction",
                table: "Attraction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegisteredUser",
                table: "RegisteredUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attraction_Coordinates_CoordinatesId",
                table: "Attraction",
                column: "CoordinatesId",
                principalTable: "Coordinates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attraction_SmartPlanner_SmartPlannerId",
                table: "Attraction",
                column: "SmartPlannerId",
                principalTable: "SmartPlanner",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Coordinates_CoordinatesId",
                table: "Event",
                column: "CoordinatesId",
                principalTable: "Coordinates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_SmartPlanner_SmartPlannerId",
                table: "Event",
                column: "SmartPlannerId",
                principalTable: "SmartPlanner",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hospitality_SmartPlanner_SmartPlannerId",
                table: "Hospitality",
                column: "SmartPlannerId",
                principalTable: "SmartPlanner",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Preference_RegisteredUser_RegisteredUserId",
                table: "Preference",
                column: "RegisteredUserId",
                principalTable: "RegisteredUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Hospitality_HospitalityId",
                table: "Review",
                column: "HospitalityId",
                principalTable: "Hospitality",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_RegisteredUser_RegisteredUserId",
                table: "Review",
                column: "RegisteredUserId",
                principalTable: "RegisteredUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SmartPlanner_Preference_PreferenceId",
                table: "SmartPlanner",
                column: "PreferenceId",
                principalTable: "Preference",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SmartPlanner_RegisteredUser_RegisteredUserId",
                table: "SmartPlanner",
                column: "RegisteredUserId",
                principalTable: "RegisteredUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
