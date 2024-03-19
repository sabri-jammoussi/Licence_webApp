using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LicenceApp.Migrations
{
    /// <inheritdoc />
    public partial class v002 : Migration
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
                    U_IDENTIFIANT = table.Column<int>(type: "int", nullable: true),
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
                values: new object[] { new byte[] { 218, 216, 251, 42, 55, 131, 202, 244, 199, 227, 234, 180, 125, 152, 241, 222, 113, 14, 117, 20, 138, 182, 153, 7, 200, 6, 211, 107, 191, 158, 25, 13, 140, 47, 82, 208, 193, 165, 252, 26, 125, 193, 134, 222, 160, 46, 208, 50, 251, 163, 241, 120, 203, 18, 131, 0, 179, 195, 71, 255, 216, 141, 100, 32 }, new byte[] { 212, 57, 135, 242, 20, 84, 84, 175, 208, 27, 193, 94, 177, 152, 163, 2, 253, 10, 146, 162, 242, 199, 221, 89, 252, 78, 141, 13, 156, 17, 138, 39, 112, 166, 32, 134, 190, 133, 63, 162, 108, 8, 12, 144, 135, 24, 40, 226, 133, 135, 34, 101, 174, 184, 199, 147, 59, 42, 242, 126, 188, 201, 8, 17, 180, 52, 215, 143, 78, 19, 64, 237, 171, 138, 72, 107, 16, 5, 71, 42, 179, 117, 253, 205, 129, 148, 237, 215, 130, 155, 10, 127, 106, 239, 234, 96, 100, 233, 196, 249, 121, 232, 118, 89, 224, 101, 173, 74, 213, 57, 166, 135, 217, 117, 43, 224, 131, 119, 90, 83, 190, 192, 116, 13, 97, 236, 156, 247 } });
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
                values: new object[] { new byte[] { 34, 77, 63, 43, 62, 226, 107, 108, 111, 57, 119, 95, 203, 12, 133, 69, 167, 133, 193, 216, 21, 221, 145, 234, 103, 227, 41, 194, 61, 197, 168, 101, 126, 31, 120, 186, 250, 125, 113, 108, 215, 44, 100, 110, 90, 159, 1, 245, 32, 166, 251, 94, 239, 106, 133, 95, 16, 36, 210, 99, 110, 75, 55, 214 }, new byte[] { 55, 156, 16, 166, 29, 68, 38, 188, 140, 40, 38, 186, 124, 229, 163, 80, 248, 214, 64, 61, 76, 102, 42, 62, 55, 6, 253, 34, 171, 241, 161, 33, 163, 224, 42, 130, 69, 5, 194, 39, 180, 245, 73, 202, 166, 248, 33, 209, 169, 104, 8, 248, 12, 107, 37, 2, 48, 239, 74, 90, 95, 146, 14, 156, 119, 90, 138, 126, 144, 111, 104, 178, 29, 96, 1, 220, 46, 80, 66, 180, 186, 28, 149, 132, 8, 29, 56, 32, 22, 22, 152, 163, 122, 235, 220, 86, 22, 207, 48, 175, 184, 147, 29, 221, 131, 105, 190, 200, 222, 6, 53, 88, 249, 147, 95, 211, 215, 74, 49, 8, 147, 250, 180, 195, 76, 168, 245, 38 } });
        }
    }
}
