using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gosarajevovol3.Migrations
{
    /// <inheritdoc />
    public partial class DodatakUseri1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { 4, 0, "2ce5cec8-23da-4fcf-9fb8-8c1c3c65d1a3", "duderija.amina2@gmail.com", true, false, null, "DUDERIJA.AMINA2@GMAIL.COM", "DUDERIJA.AMINA2@GMAIL.COM", "AQAAAAIAAYagAAAAEBDWPWnUUKfXIuEop4HZT48Q+4thK0OybYwxwZHmBWmcnCD0Cl3ApZm84SyxAhgKUA==", null, false, "5c590ead-2734-47b0-8714-67a43b676f33", false, "duderija.amina@@gmail.com", "RegisteredUser" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7c10ecdf-7915-4727-a526-5a4056364b1c", "AQAAAAIAAYagAAAAEOcnh3jkf03Is3ZbV5Bt6sxxRhSDsc0oUZWFaFa8bVbhEzYClAdUnnd5pVV8KP5s5w==", "58910ed1-19f3-470e-9bc1-60d9ea017486" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cd53ea36-9b99-4c7f-b9b8-b2979a13437c", "AQAAAAIAAYagAAAAED1uMlfhcb3YuIiE+k+iPoe4895txBTHAJ7NokbEeC2ZSU+Gjc+vB2cMtL6Qq+UE3A==", "19123784-6766-4442-9bc9-8f251dd9d121" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "646c5221-8297-4660-b092-cd47c1902e25", "AQAAAAIAAYagAAAAEC5clKUEP/mW2HT5JH4YJLtYYfUNvYq/8kidtb5t5Nah7LcJoQNZze2YZ6FXZNzQcQ==", "fcdf9814-04d6-49bd-9a3a-3e05767a0bc1" });
        }
    }
}
