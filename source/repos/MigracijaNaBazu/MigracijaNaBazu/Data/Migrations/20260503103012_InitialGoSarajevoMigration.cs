using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MigracijaNaBazu.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialGoSarajevoMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admini",
                columns: table => new
                {
                    SessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admini", x => x.SessionId);
                });

            migrationBuilder.CreateTable(
                name: "Koordinate",
                columns: table => new
                {
                    KoordinateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latituda = table.Column<double>(type: "float", nullable: false),
                    Longituda = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Koordinate", x => x.KoordinateId);
                });

            migrationBuilder.CreateTable(
                name: "Operateri",
                columns: table => new
                {
                    SessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperaterId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operateri", x => x.SessionId);
                });

            migrationBuilder.CreateTable(
                name: "Preferencije",
                columns: table => new
                {
                    PreferencijaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Historija = table.Column<int>(type: "int", nullable: false),
                    TradicionalnaHrana = table.Column<int>(type: "int", nullable: false),
                    Priroda = table.Column<int>(type: "int", nullable: false),
                    Kultura = table.Column<int>(type: "int", nullable: false),
                    NocniZivot = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preferencije", x => x.PreferencijaId);
                });

            migrationBuilder.CreateTable(
                name: "Newsletteri",
                columns: table => new
                {
                    NewsletterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naslov = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sadrzaj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumSlanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperaterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newsletteri", x => x.NewsletterId);
                    table.ForeignKey(
                        name: "FK_Newsletteri_Operateri_OperaterId",
                        column: x => x.OperaterId,
                        principalTable: "Operateri",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistrovaniKorisnici",
                columns: table => new
                {
                    SessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KoordinateId = table.Column<int>(type: "int", nullable: true),
                    NewsletterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrovaniKorisnici", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_RegistrovaniKorisnici_Koordinate_KoordinateId",
                        column: x => x.KoordinateId,
                        principalTable: "Koordinate",
                        principalColumn: "KoordinateId");
                    table.ForeignKey(
                        name: "FK_RegistrovaniKorisnici_Newsletteri_NewsletterId",
                        column: x => x.NewsletterId,
                        principalTable: "Newsletteri",
                        principalColumn: "NewsletterId");
                });

            migrationBuilder.CreateTable(
                name: "SmartPlanneri",
                columns: table => new
                {
                    PlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrovaniKorisnikId = table.Column<int>(type: "int", nullable: false),
                    PreferencijaId = table.Column<int>(type: "int", nullable: false),
                    DatumDolaska = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumOdlaska = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartPlanneri", x => x.PlanId);
                    table.ForeignKey(
                        name: "FK_SmartPlanneri_Preferencije_PreferencijaId",
                        column: x => x.PreferencijaId,
                        principalTable: "Preferencije",
                        principalColumn: "PreferencijaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SmartPlanneri_RegistrovaniKorisnici_RegistrovaniKorisnikId",
                        column: x => x.RegistrovaniKorisnikId,
                        principalTable: "RegistrovaniKorisnici",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Atrakcije",
                columns: table => new
                {
                    AtrakcijaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivAtrakcije = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KoordinateId = table.Column<int>(type: "int", nullable: false),
                    SlikaUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpisAtrakcije = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VrstaAtrakcije = table.Column<int>(type: "int", nullable: false),
                    SmartPlannerPlanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atrakcije", x => x.AtrakcijaId);
                    table.ForeignKey(
                        name: "FK_Atrakcije_Koordinate_KoordinateId",
                        column: x => x.KoordinateId,
                        principalTable: "Koordinate",
                        principalColumn: "KoordinateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Atrakcije_SmartPlanneri_SmartPlannerPlanId",
                        column: x => x.SmartPlannerPlanId,
                        principalTable: "SmartPlanneri",
                        principalColumn: "PlanId");
                });

            migrationBuilder.CreateTable(
                name: "Eventi",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivEventa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpisEventa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VrstaEventa = table.Column<int>(type: "int", nullable: false),
                    VrijemePocetka = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VrijemeZavrsetka = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SlikaUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StranicaUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KoordinateId = table.Column<int>(type: "int", nullable: false),
                    SmartPlannerPlanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventi", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Eventi_Koordinate_KoordinateId",
                        column: x => x.KoordinateId,
                        principalTable: "Koordinate",
                        principalColumn: "KoordinateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Eventi_SmartPlanneri_SmartPlannerPlanId",
                        column: x => x.SmartPlannerPlanId,
                        principalTable: "SmartPlanneri",
                        principalColumn: "PlanId");
                });

            migrationBuilder.CreateTable(
                name: "UgostiteljskiObjekti",
                columns: table => new
                {
                    ObjekatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivObjekta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KoordinateId = table.Column<int>(type: "int", nullable: false),
                    SlikaUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpisObjekta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VrstaObjekta = table.Column<int>(type: "int", nullable: false),
                    SmartPlannerPlanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UgostiteljskiObjekti", x => x.ObjekatId);
                    table.ForeignKey(
                        name: "FK_UgostiteljskiObjekti_Koordinate_KoordinateId",
                        column: x => x.KoordinateId,
                        principalTable: "Koordinate",
                        principalColumn: "KoordinateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UgostiteljskiObjekti_SmartPlanneri_SmartPlannerPlanId",
                        column: x => x.SmartPlannerPlanId,
                        principalTable: "SmartPlanneri",
                        principalColumn: "PlanId");
                });

            migrationBuilder.CreateTable(
                name: "Recenzije",
                columns: table => new
                {
                    RecenzijaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tekst = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ocjena = table.Column<double>(type: "float", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrovaniKorisnikId = table.Column<int>(type: "int", nullable: false),
                    AtrakcijaId = table.Column<int>(type: "int", nullable: true),
                    UgostiteljskiObjekatId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzije", x => x.RecenzijaId);
                    table.ForeignKey(
                        name: "FK_Recenzije_Atrakcije_AtrakcijaId",
                        column: x => x.AtrakcijaId,
                        principalTable: "Atrakcije",
                        principalColumn: "AtrakcijaId");
                    table.ForeignKey(
                        name: "FK_Recenzije_RegistrovaniKorisnici_RegistrovaniKorisnikId",
                        column: x => x.RegistrovaniKorisnikId,
                        principalTable: "RegistrovaniKorisnici",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recenzije_UgostiteljskiObjekti_UgostiteljskiObjekatId",
                        column: x => x.UgostiteljskiObjekatId,
                        principalTable: "UgostiteljskiObjekti",
                        principalColumn: "ObjekatId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atrakcije_KoordinateId",
                table: "Atrakcije",
                column: "KoordinateId");

            migrationBuilder.CreateIndex(
                name: "IX_Atrakcije_SmartPlannerPlanId",
                table: "Atrakcije",
                column: "SmartPlannerPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventi_KoordinateId",
                table: "Eventi",
                column: "KoordinateId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventi_SmartPlannerPlanId",
                table: "Eventi",
                column: "SmartPlannerPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Newsletteri_OperaterId",
                table: "Newsletteri",
                column: "OperaterId");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_AtrakcijaId",
                table: "Recenzije",
                column: "AtrakcijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_RegistrovaniKorisnikId",
                table: "Recenzije",
                column: "RegistrovaniKorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_UgostiteljskiObjekatId",
                table: "Recenzije",
                column: "UgostiteljskiObjekatId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrovaniKorisnici_KoordinateId",
                table: "RegistrovaniKorisnici",
                column: "KoordinateId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrovaniKorisnici_NewsletterId",
                table: "RegistrovaniKorisnici",
                column: "NewsletterId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartPlanneri_PreferencijaId",
                table: "SmartPlanneri",
                column: "PreferencijaId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartPlanneri_RegistrovaniKorisnikId",
                table: "SmartPlanneri",
                column: "RegistrovaniKorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_UgostiteljskiObjekti_KoordinateId",
                table: "UgostiteljskiObjekti",
                column: "KoordinateId");

            migrationBuilder.CreateIndex(
                name: "IX_UgostiteljskiObjekti_SmartPlannerPlanId",
                table: "UgostiteljskiObjekti",
                column: "SmartPlannerPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admini");

            migrationBuilder.DropTable(
                name: "Eventi");

            migrationBuilder.DropTable(
                name: "Recenzije");

            migrationBuilder.DropTable(
                name: "Atrakcije");

            migrationBuilder.DropTable(
                name: "UgostiteljskiObjekti");

            migrationBuilder.DropTable(
                name: "SmartPlanneri");

            migrationBuilder.DropTable(
                name: "Preferencije");

            migrationBuilder.DropTable(
                name: "RegistrovaniKorisnici");

            migrationBuilder.DropTable(
                name: "Koordinate");

            migrationBuilder.DropTable(
                name: "Newsletteri");

            migrationBuilder.DropTable(
                name: "Operateri");
        }
    }
}
