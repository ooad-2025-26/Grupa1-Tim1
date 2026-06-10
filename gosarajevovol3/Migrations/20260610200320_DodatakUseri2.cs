using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gosarajevovol3.Migrations
{
    /// <inheritdoc />
    public partial class DodatakUseri2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "31a28d92-de97-404a-b118-37e73711ae63", "AQAAAAIAAYagAAAAENMtxL9v2ubPFwG5xZH9GF3hS0czOZp9bZJWt6i2L5VLeGSfKbabMNE823wkCKQ12A==", "b68262d8-ca40-4569-9b8b-ac5b67372ac9" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7e2120e5-79c0-4dcc-bda6-2cf4ad920504", "AQAAAAIAAYagAAAAEJ/byETwt4fbcowu2Hn8aDl6aPb53EavYFDnNNvWr7f1jfrti4n+Klx2pmck+rmL2g==", "194d9180-4f29-4d0e-b9a0-37c9865c8070" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "560658ca-8845-401d-b1f0-c411efeddf47", "AQAAAAIAAYagAAAAEPauaDxACCbbr6h8Sag2gR1ZR1qqImpenFl1H7A5ZitxtwjrRDazoV0+xTc7omvlmQ==", "9618f3b9-88a0-462e-a6c7-64ec3206ec62" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "abe31c70-6d39-48b2-b281-3c958480bb6d", "AQAAAAIAAYagAAAAEOWRmXsmDiKlMmHps/mlBsV9wGj4njGPSrhPVi+Q/X1iRsXcdMUfjeDvJ4i5P3oC/A==", "e8e7e7ca-5ec7-4593-84b8-ce3f489608fc", "duderija.amina2@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "50cddd94-d709-47b9-8456-26e39d958857", "AQAAAAIAAYagAAAAEJLmwAwottkkKPBviiMX1AJYj6CRCbPgKISbCcKPxf34ZksctIN5TRyfKzcuuVOLvw==", "8741caa8-5c70-48ad-bc7b-d33cb000de31" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b8416f68-a9d3-4ead-8b4c-f1014d12020b", "AQAAAAIAAYagAAAAEPsTF1/t8C5j66ktayUG1Enm6uyCXV3V2M10G9RMeFitiHqhQQ7k/1KL6ba+yvOZLQ==", "47dfa8ad-e12c-46ce-a49c-2f13f2215a23" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b494f02e-78ef-42a0-8aa9-9f0f02a1ff7c", "AQAAAAIAAYagAAAAEEbZwACdR8woc6t+v/v4ZND8FWYhirhoAV7Kn4TPO2kkCbtF58zQ0yN73s2/D6Wmfg==", "4708116c-ccf7-488e-bf76-8ffa96f317b8" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "2ce5cec8-23da-4fcf-9fb8-8c1c3c65d1a3", "AQAAAAIAAYagAAAAEBDWPWnUUKfXIuEop4HZT48Q+4thK0OybYwxwZHmBWmcnCD0Cl3ApZm84SyxAhgKUA==", "5c590ead-2734-47b0-8714-67a43b676f33", "duderija.amina@@gmail.com" });
        }
    }
}
