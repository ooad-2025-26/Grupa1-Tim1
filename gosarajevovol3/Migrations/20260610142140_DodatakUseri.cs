using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace gosarajevovol3.Migrations
{
    /// <inheritdoc />
    public partial class DodatakUseri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[,]
                {
                    { 1, 0, "7c10ecdf-7915-4727-a526-5a4056364b1c", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEOcnh3jkf03Is3ZbV5Bt6sxxRhSDsc0oUZWFaFa8bVbhEzYClAdUnnd5pVV8KP5s5w==", null, false, "58910ed1-19f3-470e-9bc1-60d9ea017486", false, "admin@gmail.com", "Admin" },
                    { 2, 0, "cd53ea36-9b99-4c7f-b9b8-b2979a13437c", "operator@gmail.com", true, false, null, "OPERATOR@GMAIL.COM", "OPERATOR@GMAIL.COM", "AQAAAAIAAYagAAAAED1uMlfhcb3YuIiE+k+iPoe4895txBTHAJ7NokbEeC2ZSU+Gjc+vB2cMtL6Qq+UE3A==", null, false, "19123784-6766-4442-9bc9-8f251dd9d121", false, "operator@gmail.com", "Operator" },
                    { 3, 0, "646c5221-8297-4660-b092-cd47c1902e25", "korisnik@gmail.com", true, false, null, "KORISNIK@GMAIL.COM", "KORISNIK@GMAIL.COM", "AQAAAAIAAYagAAAAEC5clKUEP/mW2HT5JH4YJLtYYfUNvYq/8kidtb5t5Nah7LcJoQNZze2YZ6FXZNzQcQ==", null, false, "fcdf9814-04d6-49bd-9a3a-3e05767a0bc1", false, "korisnik@gmail.com", "RegisteredUser" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
