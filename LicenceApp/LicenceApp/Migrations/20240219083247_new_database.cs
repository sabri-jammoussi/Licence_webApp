using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LicenceApp.Migrations
{
    /// <inheritdoc />
    public partial class new_database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_USERS2",
                table: "USERS2");

            migrationBuilder.RenameTable(
                name: "USERS2",
                newName: "USERS");

            migrationBuilder.AlterColumn<byte[]>(
                name: "U_PASSWORD_HASH",
                table: "USERS",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_USERS",
                table: "USERS",
                column: "U_ID");

            migrationBuilder.InsertData(
                table: "USERS",
                columns: new[] { "U_ID", "U_EMAIL", "U_FIRSTNAME", "U_LASTNAME", "U_PASSWORD_HASH", "U_PASSWORD_SALT", "U_ROLE" },
                values: new object[] { 1, "sabrijm123@gmail.com", "sabri", "jammoussi", new byte[] { 144, 111, 105, 83, 171, 165, 211, 1, 209, 142, 172, 55, 32, 47, 113, 11, 28, 224, 38, 83, 255, 71, 41, 217, 23, 241, 175, 180, 35, 3, 37, 90, 145, 43, 3, 104, 39, 70, 234, 111, 106, 65, 237, 9, 143, 4, 139, 125, 206, 159, 153, 43, 67, 187, 129, 96, 55, 173, 174, 9, 21, 199, 20, 253 }, new byte[] { 104, 30, 72, 204, 111, 206, 236, 20, 95, 117, 6, 29, 161, 107, 139, 123, 47, 251, 29, 198, 91, 49, 17, 110, 105, 212, 203, 234, 242, 208, 180, 145, 167, 173, 211, 61, 212, 37, 27, 108, 118, 58, 162, 193, 146, 119, 95, 122, 98, 142, 237, 101, 63, 118, 19, 23, 172, 122, 63, 197, 82, 197, 239, 240, 161, 71, 17, 145, 30, 11, 122, 237, 253, 98, 214, 112, 6, 108, 24, 1, 31, 218, 57, 13, 125, 32, 97, 255, 37, 11, 245, 76, 214, 183, 249, 177, 236, 168, 121, 51, 168, 104, 82, 165, 84, 153, 101, 119, 172, 112, 55, 73, 123, 137, 61, 251, 61, 74, 88, 73, 8, 228, 66, 237, 221, 87, 92, 3 }, (short)0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_USERS",
                table: "USERS");

            migrationBuilder.DeleteData(
                table: "USERS",
                keyColumn: "U_ID",
                keyValue: 1);

            migrationBuilder.RenameTable(
                name: "USERS",
                newName: "USERS2");

            migrationBuilder.AlterColumn<byte[]>(
                name: "U_PASSWORD_HASH",
                table: "USERS2",
                type: "varbinary(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_USERS2",
                table: "USERS2",
                column: "U_ID");
        }
    }
}
