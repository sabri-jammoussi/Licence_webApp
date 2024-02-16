using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LicenceApp.Migrations
{
    /// <inheritdoc />
    public partial class Passwor_Hash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USERS2",
                keyColumn: "U_ID",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "U_PASSWORD",
                table: "USERS2");

            migrationBuilder.AddColumn<byte[]>(
                name: "U_PASSWORD_HASH",
                table: "USERS2",
                type: "varbinary(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "U_PASSWORD_SALT",
                table: "USERS2",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "U_PASSWORD_HASH",
                table: "USERS2");

            migrationBuilder.DropColumn(
                name: "U_PASSWORD_SALT",
                table: "USERS2");

            migrationBuilder.AddColumn<string>(
                name: "U_PASSWORD",
                table: "USERS2",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.InsertData(
                table: "USERS2",
                columns: new[] { "U_ID", "U_EMAIL", "U_FIRSTNAME", "U_LASTNAME", "U_PASSWORD", "U_ROLE" },
                values: new object[] { 1, "sabrijm123@gmail.com", "sabri", "jammoussi", "Sabri123", (short)0 });
        }
    }
}
