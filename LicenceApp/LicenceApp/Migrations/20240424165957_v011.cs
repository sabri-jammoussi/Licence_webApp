using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LicenceApp.Migrations
{
    /// <inheritdoc />
    public partial class v011 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ATT_ENUMERATION_VALUE",
                table: "ATTRIBUTE_LICENCE",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "U_ID",
                keyValue: 1,
                columns: new[] { "U_PASSWORD_HASH", "U_PASSWORD_SALT" },
                values: new object[] { new byte[] { 149, 250, 200, 227, 102, 193, 170, 83, 199, 148, 216, 17, 193, 146, 134, 63, 79, 86, 160, 119, 70, 72, 71, 154, 129, 0, 41, 5, 18, 209, 93, 186, 152, 77, 33, 216, 194, 143, 106, 140, 152, 28, 155, 3, 251, 141, 60, 61, 175, 211, 6, 196, 91, 109, 128, 148, 66, 213, 212, 79, 45, 209, 99, 212 }, new byte[] { 164, 228, 125, 9, 152, 5, 106, 255, 121, 79, 185, 14, 182, 221, 87, 147, 98, 29, 224, 254, 7, 216, 207, 46, 10, 159, 31, 194, 143, 204, 58, 45, 55, 208, 102, 154, 190, 240, 251, 183, 230, 198, 51, 128, 255, 104, 91, 203, 118, 109, 19, 239, 38, 235, 120, 134, 109, 228, 72, 87, 62, 51, 173, 0, 144, 84, 195, 120, 63, 41, 214, 168, 76, 12, 164, 144, 84, 134, 40, 250, 84, 54, 77, 112, 187, 38, 6, 116, 181, 136, 233, 18, 222, 32, 170, 162, 140, 74, 106, 178, 158, 166, 130, 198, 68, 55, 159, 92, 32, 234, 31, 208, 30, 102, 80, 26, 225, 12, 50, 38, 214, 221, 35, 77, 88, 66, 6, 179 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ATT_ENUMERATION_VALUE",
                table: "ATTRIBUTE_LICENCE");

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "U_ID",
                keyValue: 1,
                columns: new[] { "U_PASSWORD_HASH", "U_PASSWORD_SALT" },
                values: new object[] { new byte[] { 58, 134, 218, 10, 111, 47, 105, 44, 253, 204, 29, 99, 196, 127, 214, 168, 231, 119, 105, 30, 165, 198, 198, 84, 85, 240, 225, 107, 226, 84, 141, 203, 27, 91, 25, 214, 56, 40, 19, 137, 91, 54, 217, 148, 48, 182, 90, 130, 7, 167, 202, 107, 30, 81, 65, 136, 126, 73, 222, 112, 25, 231, 187, 204 }, new byte[] { 217, 69, 241, 62, 80, 89, 27, 83, 62, 86, 131, 170, 129, 239, 105, 80, 13, 27, 201, 215, 167, 176, 134, 249, 119, 174, 129, 134, 6, 43, 20, 41, 22, 55, 207, 130, 64, 252, 28, 185, 183, 227, 15, 10, 69, 7, 129, 112, 127, 64, 64, 105, 219, 139, 30, 81, 231, 86, 105, 131, 63, 129, 153, 60, 53, 142, 200, 158, 154, 44, 75, 3, 29, 31, 125, 18, 32, 56, 174, 209, 10, 100, 93, 144, 6, 97, 155, 140, 166, 239, 39, 245, 47, 228, 3, 240, 198, 107, 195, 2, 80, 215, 70, 99, 121, 46, 104, 61, 2, 222, 162, 175, 127, 93, 248, 61, 128, 25, 230, 217, 86, 177, 111, 26, 45, 123, 80, 196 } });
        }
    }
}
