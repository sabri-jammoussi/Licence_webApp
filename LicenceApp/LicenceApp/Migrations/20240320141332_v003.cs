using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LicenceApp.Migrations
{
    /// <inheritdoc />
    public partial class v003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "U_VILLE",
                table: "CLIENTS",
                newName: "CL_VILLE");

            migrationBuilder.RenameColumn(
                name: "U_TELEPHONE",
                table: "CLIENTS",
                newName: "CL_TELEPHONE");

            migrationBuilder.RenameColumn(
                name: "U_RAISON_SOCIAL",
                table: "CLIENTS",
                newName: "CL_RAISON_SOCIAL");

            migrationBuilder.RenameColumn(
                name: "U_PAYS",
                table: "CLIENTS",
                newName: "CL_PAYS");

            migrationBuilder.RenameColumn(
                name: "U_IDENTIFIANT",
                table: "CLIENTS",
                newName: "CL_IDENTIFIANT");

            migrationBuilder.RenameColumn(
                name: "U_EMAIL",
                table: "CLIENTS",
                newName: "CL_EMAIL");

            migrationBuilder.RenameColumn(
                name: "U_CODE_POSTAL",
                table: "CLIENTS",
                newName: "CL_CODE_POSTAL");

            migrationBuilder.RenameColumn(
                name: "U_ADRESSE",
                table: "CLIENTS",
                newName: "CL_ADRESSE");

            migrationBuilder.RenameColumn(
                name: "U_ID",
                table: "CLIENTS",
                newName: "CL_ID");

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "U_ID",
                keyValue: 1,
                columns: new[] { "U_PASSWORD_HASH", "U_PASSWORD_SALT" },
                values: new object[] { new byte[] { 137, 219, 198, 194, 228, 161, 26, 143, 185, 22, 164, 80, 122, 65, 114, 145, 41, 45, 201, 242, 126, 130, 128, 94, 119, 13, 71, 185, 208, 49, 236, 246, 166, 163, 118, 67, 44, 183, 39, 130, 44, 152, 183, 188, 146, 246, 159, 144, 226, 2, 171, 109, 225, 176, 142, 180, 58, 230, 87, 246, 78, 173, 12, 134 }, new byte[] { 172, 101, 253, 171, 175, 92, 59, 2, 21, 97, 77, 62, 168, 8, 226, 48, 108, 2, 132, 234, 15, 61, 33, 165, 85, 200, 91, 132, 157, 52, 157, 4, 219, 104, 223, 72, 84, 235, 38, 149, 24, 63, 191, 120, 218, 159, 22, 104, 64, 128, 232, 209, 117, 10, 41, 44, 156, 26, 231, 228, 215, 3, 53, 67, 200, 161, 208, 126, 162, 36, 31, 203, 180, 124, 160, 28, 181, 7, 109, 8, 24, 245, 58, 186, 238, 218, 16, 222, 150, 69, 82, 22, 66, 49, 67, 174, 170, 82, 110, 253, 230, 163, 116, 233, 9, 255, 203, 84, 33, 31, 124, 102, 83, 89, 211, 200, 209, 90, 125, 32, 3, 45, 168, 254, 174, 106, 33, 12 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CL_VILLE",
                table: "CLIENTS",
                newName: "U_VILLE");

            migrationBuilder.RenameColumn(
                name: "CL_TELEPHONE",
                table: "CLIENTS",
                newName: "U_TELEPHONE");

            migrationBuilder.RenameColumn(
                name: "CL_RAISON_SOCIAL",
                table: "CLIENTS",
                newName: "U_RAISON_SOCIAL");

            migrationBuilder.RenameColumn(
                name: "CL_PAYS",
                table: "CLIENTS",
                newName: "U_PAYS");

            migrationBuilder.RenameColumn(
                name: "CL_IDENTIFIANT",
                table: "CLIENTS",
                newName: "U_IDENTIFIANT");

            migrationBuilder.RenameColumn(
                name: "CL_EMAIL",
                table: "CLIENTS",
                newName: "U_EMAIL");

            migrationBuilder.RenameColumn(
                name: "CL_CODE_POSTAL",
                table: "CLIENTS",
                newName: "U_CODE_POSTAL");

            migrationBuilder.RenameColumn(
                name: "CL_ADRESSE",
                table: "CLIENTS",
                newName: "U_ADRESSE");

            migrationBuilder.RenameColumn(
                name: "CL_ID",
                table: "CLIENTS",
                newName: "U_ID");

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "U_ID",
                keyValue: 1,
                columns: new[] { "U_PASSWORD_HASH", "U_PASSWORD_SALT" },
                values: new object[] { new byte[] { 218, 216, 251, 42, 55, 131, 202, 244, 199, 227, 234, 180, 125, 152, 241, 222, 113, 14, 117, 20, 138, 182, 153, 7, 200, 6, 211, 107, 191, 158, 25, 13, 140, 47, 82, 208, 193, 165, 252, 26, 125, 193, 134, 222, 160, 46, 208, 50, 251, 163, 241, 120, 203, 18, 131, 0, 179, 195, 71, 255, 216, 141, 100, 32 }, new byte[] { 212, 57, 135, 242, 20, 84, 84, 175, 208, 27, 193, 94, 177, 152, 163, 2, 253, 10, 146, 162, 242, 199, 221, 89, 252, 78, 141, 13, 156, 17, 138, 39, 112, 166, 32, 134, 190, 133, 63, 162, 108, 8, 12, 144, 135, 24, 40, 226, 133, 135, 34, 101, 174, 184, 199, 147, 59, 42, 242, 126, 188, 201, 8, 17, 180, 52, 215, 143, 78, 19, 64, 237, 171, 138, 72, 107, 16, 5, 71, 42, 179, 117, 253, 205, 129, 148, 237, 215, 130, 155, 10, 127, 106, 239, 234, 96, 100, 233, 196, 249, 121, 232, 118, 89, 224, 101, 173, 74, 213, 57, 166, 135, 217, 117, 43, 224, 131, 119, 90, 83, 190, 192, 116, 13, 97, 236, 156, 247 } });
        }
    }
}
