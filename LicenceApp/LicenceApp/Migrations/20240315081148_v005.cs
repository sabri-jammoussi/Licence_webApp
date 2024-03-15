using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LicenceApp.Migrations
{
    /// <inheritdoc />
    public partial class v005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "APPLICATION",
                columns: table => new
                {
                    U_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    U_IDENTIFIANT = table.Column<int>(type: "int", maxLength: 20, nullable: true),
                    U_NOM = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    U_DESCRIPTION = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APPLICATION", x => x.U_ID);
                });

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "U_ID",
                keyValue: 1,
                columns: new[] { "U_PASSWORD_HASH", "U_PASSWORD_SALT" },
                values: new object[] { new byte[] { 197, 26, 94, 167, 174, 146, 220, 231, 249, 2, 184, 28, 184, 221, 33, 170, 7, 108, 226, 34, 219, 92, 20, 159, 143, 145, 17, 91, 143, 0, 172, 227, 247, 126, 230, 15, 60, 76, 70, 143, 70, 70, 118, 202, 246, 3, 40, 56, 141, 5, 240, 206, 187, 50, 55, 230, 164, 79, 255, 48, 41, 119, 138, 153 }, new byte[] { 194, 151, 5, 240, 204, 215, 48, 85, 254, 131, 207, 38, 111, 192, 76, 59, 142, 152, 61, 49, 39, 15, 229, 151, 248, 137, 55, 157, 239, 29, 158, 146, 122, 74, 102, 108, 35, 89, 145, 178, 93, 106, 113, 51, 227, 188, 58, 129, 128, 8, 252, 187, 239, 103, 129, 210, 177, 119, 75, 120, 164, 129, 30, 194, 154, 208, 232, 183, 42, 28, 122, 253, 199, 185, 76, 32, 233, 152, 188, 0, 8, 246, 68, 190, 193, 119, 235, 196, 163, 143, 90, 89, 48, 154, 2, 12, 220, 62, 22, 53, 0, 36, 169, 122, 43, 236, 140, 152, 2, 242, 94, 133, 255, 243, 225, 236, 208, 102, 192, 201, 14, 109, 124, 134, 241, 134, 194, 6 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APPLICATION");

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "U_ID",
                keyValue: 1,
                columns: new[] { "U_PASSWORD_HASH", "U_PASSWORD_SALT" },
                values: new object[] { new byte[] { 161, 87, 39, 218, 209, 191, 155, 207, 83, 103, 48, 180, 38, 157, 47, 82, 118, 7, 208, 156, 230, 32, 29, 147, 16, 206, 227, 168, 112, 49, 248, 122, 177, 11, 10, 101, 156, 29, 192, 149, 227, 41, 175, 111, 228, 44, 16, 206, 109, 158, 96, 143, 177, 142, 26, 166, 179, 134, 222, 25, 118, 66, 23, 12 }, new byte[] { 58, 4, 137, 172, 225, 34, 25, 202, 244, 75, 56, 11, 92, 64, 221, 83, 204, 13, 241, 168, 57, 130, 185, 85, 236, 236, 224, 69, 30, 163, 129, 52, 178, 238, 97, 119, 23, 122, 139, 175, 116, 229, 42, 177, 187, 36, 10, 123, 7, 159, 72, 92, 216, 225, 158, 137, 241, 222, 245, 186, 74, 65, 136, 93, 157, 215, 213, 148, 57, 199, 253, 128, 190, 153, 184, 43, 5, 190, 253, 245, 229, 124, 121, 99, 106, 64, 206, 141, 24, 49, 184, 111, 94, 133, 12, 156, 201, 25, 201, 94, 170, 180, 210, 235, 53, 60, 30, 79, 37, 236, 127, 135, 6, 221, 30, 157, 219, 19, 213, 229, 3, 15, 93, 120, 247, 253, 205, 81 } });
        }
    }
}
