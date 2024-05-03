using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LicenceApp.Migrations
{
    /// <inheritdoc />
    public partial class v009 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ATT_OBLIGATION",
                table: "ATTRIBUTE_LICENCE",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "U_ID",
                keyValue: 1,
                columns: new[] { "U_PASSWORD_HASH", "U_PASSWORD_SALT" },
                values: new object[] { new byte[] { 213, 119, 145, 182, 4, 46, 181, 119, 46, 55, 221, 221, 105, 122, 43, 110, 30, 236, 15, 112, 11, 151, 156, 127, 103, 179, 180, 106, 42, 143, 79, 119, 250, 252, 36, 134, 48, 168, 253, 155, 211, 209, 3, 90, 190, 222, 19, 10, 22, 102, 251, 117, 112, 90, 138, 96, 135, 131, 197, 205, 99, 11, 133, 199 }, new byte[] { 109, 20, 177, 18, 86, 141, 86, 98, 219, 73, 204, 6, 207, 224, 147, 195, 236, 145, 242, 209, 15, 166, 93, 62, 102, 96, 238, 24, 190, 123, 166, 76, 176, 206, 89, 28, 0, 39, 197, 6, 72, 11, 79, 168, 127, 188, 166, 2, 241, 88, 131, 172, 170, 151, 93, 221, 104, 166, 156, 252, 145, 216, 182, 138, 155, 130, 6, 249, 130, 64, 192, 66, 86, 211, 99, 200, 200, 100, 176, 131, 99, 29, 156, 73, 115, 238, 213, 122, 178, 160, 117, 45, 45, 10, 165, 2, 189, 185, 16, 18, 253, 158, 100, 53, 57, 46, 79, 183, 93, 46, 52, 121, 30, 190, 255, 15, 177, 103, 151, 233, 100, 240, 54, 169, 188, 121, 22, 56 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ATT_OBLIGATION",
                table: "ATTRIBUTE_LICENCE");

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "U_ID",
                keyValue: 1,
                columns: new[] { "U_PASSWORD_HASH", "U_PASSWORD_SALT" },
                values: new object[] { new byte[] { 42, 207, 173, 11, 47, 129, 168, 237, 223, 152, 132, 39, 88, 146, 162, 183, 190, 183, 178, 154, 104, 186, 154, 79, 181, 232, 72, 106, 129, 248, 63, 3, 209, 84, 120, 89, 177, 71, 43, 5, 204, 212, 105, 189, 180, 126, 45, 103, 167, 228, 110, 175, 44, 50, 177, 132, 224, 127, 118, 209, 94, 238, 177, 127 }, new byte[] { 127, 242, 160, 186, 92, 231, 234, 5, 91, 45, 169, 184, 206, 222, 4, 137, 195, 194, 25, 148, 243, 102, 16, 107, 159, 25, 66, 193, 25, 40, 129, 35, 117, 120, 106, 71, 7, 8, 113, 181, 29, 222, 142, 172, 165, 106, 58, 95, 160, 104, 232, 223, 118, 89, 18, 105, 122, 151, 115, 5, 108, 115, 213, 228, 100, 195, 137, 210, 82, 45, 196, 122, 159, 211, 59, 227, 229, 147, 125, 211, 121, 56, 242, 115, 88, 59, 177, 12, 168, 65, 103, 137, 118, 196, 222, 197, 41, 135, 115, 61, 90, 240, 17, 153, 68, 183, 63, 87, 143, 98, 67, 188, 12, 159, 170, 45, 148, 133, 248, 224, 236, 201, 154, 81, 104, 182, 33, 176 } });
        }
    }
}
