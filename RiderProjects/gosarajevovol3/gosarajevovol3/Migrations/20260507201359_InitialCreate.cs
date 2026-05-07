using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gosarajevovol3.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coordinates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegisteredUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Preference",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    History = table.Column<int>(type: "int", nullable: false),
                    TraditionalFood = table.Column<int>(type: "int", nullable: false),
                    Nature = table.Column<int>(type: "int", nullable: false),
                    Culture = table.Column<int>(type: "int", nullable: false),
                    Nightlife = table.Column<int>(type: "int", nullable: false),
                    RegisteredUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preference", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Preference_RegisteredUser_RegisteredUserId",
                        column: x => x.RegisteredUserId,
                        principalTable: "RegisteredUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SmartPlanner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisteredUserId = table.Column<int>(type: "int", nullable: false),
                    PreferenceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartPlanner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmartPlanner_Preference_PreferenceId",
                        column: x => x.PreferenceId,
                        principalTable: "Preference",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SmartPlanner_RegisteredUser_RegisteredUserId",
                        column: x => x.RegisteredUserId,
                        principalTable: "RegisteredUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attraction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttractionName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AttractionDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AttractionType = table.Column<int>(type: "int", nullable: false),
                    CoordinatesId = table.Column<int>(type: "int", nullable: false),
                    LocationAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    SmartPlannerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attraction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attraction_Coordinates_CoordinatesId",
                        column: x => x.CoordinatesId,
                        principalTable: "Coordinates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attraction_SmartPlanner_SmartPlannerId",
                        column: x => x.SmartPlannerId,
                        principalTable: "SmartPlanner",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EventDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoordinatesId = table.Column<int>(type: "int", nullable: false),
                    LocationAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    SmartPlannerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Coordinates_CoordinatesId",
                        column: x => x.CoordinatesId,
                        principalTable: "Coordinates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_SmartPlanner_SmartPlannerId",
                        column: x => x.SmartPlannerId,
                        principalTable: "SmartPlanner",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hospitality",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalityName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    HospitalityDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    HospitalityType = table.Column<int>(type: "int", nullable: false),
                    LocationAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CoordinatesId = table.Column<int>(type: "int", nullable: false),
                    GooglePlaceId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SmartPlannerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitality", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hospitality_Coordinates_CoordinatesId",
                        column: x => x.CoordinatesId,
                        principalTable: "Coordinates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hospitality_SmartPlanner_SmartPlannerId",
                        column: x => x.SmartPlannerId,
                        principalTable: "SmartPlanner",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisteredUserId = table.Column<int>(type: "int", nullable: false),
                    HospitalityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_Hospitality_HospitalityId",
                        column: x => x.HospitalityId,
                        principalTable: "Hospitality",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Review_RegisteredUser_RegisteredUserId",
                        column: x => x.RegisteredUserId,
                        principalTable: "RegisteredUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attraction_CoordinatesId",
                table: "Attraction",
                column: "CoordinatesId");

            migrationBuilder.CreateIndex(
                name: "IX_Attraction_SmartPlannerId",
                table: "Attraction",
                column: "SmartPlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_CoordinatesId",
                table: "Event",
                column: "CoordinatesId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_SmartPlannerId",
                table: "Event",
                column: "SmartPlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitality_CoordinatesId",
                table: "Hospitality",
                column: "CoordinatesId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitality_SmartPlannerId",
                table: "Hospitality",
                column: "SmartPlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_Preference_RegisteredUserId",
                table: "Preference",
                column: "RegisteredUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Review_HospitalityId",
                table: "Review",
                column: "HospitalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_RegisteredUserId",
                table: "Review",
                column: "RegisteredUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartPlanner_PreferenceId",
                table: "SmartPlanner",
                column: "PreferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartPlanner_RegisteredUserId",
                table: "SmartPlanner",
                column: "RegisteredUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attraction");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Hospitality");

            migrationBuilder.DropTable(
                name: "Coordinates");

            migrationBuilder.DropTable(
                name: "SmartPlanner");

            migrationBuilder.DropTable(
                name: "Preference");

            migrationBuilder.DropTable(
                name: "RegisteredUser");
        }
    }
}
