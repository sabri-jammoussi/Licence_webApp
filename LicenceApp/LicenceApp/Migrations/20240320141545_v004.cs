using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LicenceApp.Migrations
{
    /// <inheritdoc />
    public partial class v004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "U_NOM",
                table: "APPLICATION",
                newName: "APP_NOM");

            migrationBuilder.RenameColumn(
                name: "U_IDENTIFIANT",
                table: "APPLICATION",
                newName: "APP_IDENTIFIANT");

            migrationBuilder.RenameColumn(
                name: "U_DESCRIPTION",
                table: "APPLICATION",
                newName: "APP_DESCRIPTION");

            migrationBuilder.RenameColumn(
                name: "U_ID",
                table: "APPLICATION",
                newName: "APP_ID");

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "U_ID",
                keyValue: 1,
                columns: new[] { "U_PASSWORD_HASH", "U_PASSWORD_SALT" },
                values: new object[] { new byte[] { 169, 126, 205, 168, 81, 245, 175, 26, 105, 232, 199, 94, 172, 238, 160, 206, 7, 231, 250, 123, 252, 186, 88, 149, 237, 86, 32, 80, 217, 69, 53, 101, 216, 202, 22, 84, 223, 182, 206, 36, 192, 30, 111, 49, 225, 231, 59, 24, 145, 222, 210, 97, 143, 207, 212, 77, 233, 194, 244, 186, 102, 53, 141, 110 }, new byte[] { 172, 87, 175, 138, 252, 105, 103, 82, 74, 114, 124, 90, 58, 229, 45, 68, 244, 255, 174, 146, 171, 178, 153, 20, 99, 17, 178, 119, 132, 55, 125, 135, 88, 37, 120, 147, 168, 64, 81, 150, 147, 94, 30, 95, 161, 91, 90, 249, 108, 2, 97, 20, 115, 12, 251, 206, 93, 28, 122, 156, 8, 251, 142, 225, 159, 84, 178, 217, 169, 139, 12, 54, 132, 176, 165, 184, 246, 101, 110, 240, 158, 45, 78, 162, 224, 37, 231, 33, 138, 83, 166, 207, 225, 110, 153, 115, 29, 113, 225, 206, 152, 155, 237, 193, 245, 13, 145, 156, 218, 98, 29, 148, 36, 195, 226, 180, 207, 45, 241, 248, 165, 240, 47, 162, 222, 19, 141, 196 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "APP_NOM",
                table: "APPLICATION",
                newName: "U_NOM");

            migrationBuilder.RenameColumn(
                name: "APP_IDENTIFIANT",
                table: "APPLICATION",
                newName: "U_IDENTIFIANT");

            migrationBuilder.RenameColumn(
                name: "APP_DESCRIPTION",
                table: "APPLICATION",
                newName: "U_DESCRIPTION");

            migrationBuilder.RenameColumn(
                name: "APP_ID",
                table: "APPLICATION",
                newName: "U_ID");

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "U_ID",
                keyValue: 1,
                columns: new[] { "U_PASSWORD_HASH", "U_PASSWORD_SALT" },
                values: new object[] { new byte[] { 137, 219, 198, 194, 228, 161, 26, 143, 185, 22, 164, 80, 122, 65, 114, 145, 41, 45, 201, 242, 126, 130, 128, 94, 119, 13, 71, 185, 208, 49, 236, 246, 166, 163, 118, 67, 44, 183, 39, 130, 44, 152, 183, 188, 146, 246, 159, 144, 226, 2, 171, 109, 225, 176, 142, 180, 58, 230, 87, 246, 78, 173, 12, 134 }, new byte[] { 172, 101, 253, 171, 175, 92, 59, 2, 21, 97, 77, 62, 168, 8, 226, 48, 108, 2, 132, 234, 15, 61, 33, 165, 85, 200, 91, 132, 157, 52, 157, 4, 219, 104, 223, 72, 84, 235, 38, 149, 24, 63, 191, 120, 218, 159, 22, 104, 64, 128, 232, 209, 117, 10, 41, 44, 156, 26, 231, 228, 215, 3, 53, 67, 200, 161, 208, 126, 162, 36, 31, 203, 180, 124, 160, 28, 181, 7, 109, 8, 24, 245, 58, 186, 238, 218, 16, 222, 150, 69, 82, 22, 66, 49, 67, 174, 170, 82, 110, 253, 230, 163, 116, 233, 9, 255, 203, 84, 33, 31, 124, 102, 83, 89, 211, 200, 209, 90, 125, 32, 3, 45, 168, 254, 174, 106, 33, 12 } });
        }
    }
}
