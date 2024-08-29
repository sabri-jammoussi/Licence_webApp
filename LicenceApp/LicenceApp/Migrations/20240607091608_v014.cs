using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LicenceApp.Migrations
{
    /// <inheritdoc />
    public partial class v014 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "APP_DESCRIPTION",
                table: "APPLICATION",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "U_ID",
                keyValue: 1,
                columns: new[] { "U_PASSWORD_HASH", "U_PASSWORD_SALT" },
                values: new object[] { new byte[] { 15, 127, 113, 91, 159, 252, 249, 212, 19, 43, 145, 17, 30, 7, 107, 129, 235, 49, 112, 199, 21, 9, 208, 166, 109, 216, 19, 152, 183, 48, 210, 46, 44, 37, 252, 168, 59, 24, 248, 96, 0, 82, 67, 169, 185, 20, 147, 109, 180, 200, 6, 185, 83, 17, 63, 121, 153, 14, 210, 94, 89, 187, 57, 80 }, new byte[] { 26, 110, 38, 110, 21, 242, 131, 179, 180, 14, 6, 24, 65, 20, 84, 148, 108, 196, 61, 239, 86, 198, 234, 18, 236, 92, 253, 108, 34, 60, 126, 209, 87, 21, 53, 224, 241, 28, 129, 46, 83, 107, 184, 171, 162, 160, 228, 20, 229, 132, 61, 156, 190, 76, 88, 145, 254, 110, 153, 34, 154, 52, 13, 191, 55, 242, 59, 128, 86, 52, 92, 106, 13, 243, 115, 251, 100, 124, 49, 151, 70, 124, 57, 183, 36, 209, 237, 184, 49, 35, 137, 254, 112, 245, 49, 25, 213, 69, 129, 97, 18, 128, 183, 205, 122, 195, 143, 28, 251, 240, 141, 86, 87, 35, 110, 45, 13, 188, 237, 79, 196, 176, 154, 5, 159, 127, 154, 159 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "APP_DESCRIPTION",
                table: "APPLICATION",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "U_ID",
                keyValue: 1,
                columns: new[] { "U_PASSWORD_HASH", "U_PASSWORD_SALT" },
                values: new object[] { new byte[] { 25, 7, 1, 50, 190, 183, 39, 190, 134, 51, 178, 92, 125, 209, 214, 69, 220, 223, 123, 241, 187, 58, 68, 186, 40, 46, 140, 180, 229, 8, 11, 146, 124, 128, 70, 127, 148, 44, 78, 191, 41, 220, 43, 69, 86, 57, 169, 124, 151, 180, 39, 55, 240, 211, 145, 218, 169, 162, 191, 37, 26, 180, 202, 95 }, new byte[] { 179, 66, 227, 136, 149, 83, 138, 42, 194, 251, 11, 247, 56, 227, 167, 196, 125, 113, 205, 84, 54, 54, 183, 73, 88, 63, 42, 230, 183, 138, 27, 92, 66, 0, 78, 0, 113, 84, 99, 111, 73, 36, 199, 180, 82, 200, 220, 123, 43, 170, 27, 142, 121, 155, 64, 29, 0, 26, 121, 208, 230, 93, 69, 109, 111, 88, 11, 99, 151, 180, 18, 34, 198, 162, 174, 203, 255, 25, 244, 152, 156, 189, 55, 55, 37, 168, 212, 38, 246, 154, 127, 94, 102, 22, 239, 184, 192, 149, 65, 107, 117, 224, 78, 194, 119, 102, 10, 28, 177, 91, 138, 141, 25, 101, 28, 181, 22, 24, 194, 117, 50, 215, 123, 38, 19, 49, 90, 29 } });
        }
    }
}
