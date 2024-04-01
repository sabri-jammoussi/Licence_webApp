using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LicenceApp.Migrations
{
    /// <inheritdoc />
    public partial class v006 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "enumerationValeurs");

            migrationBuilder.DropTable(
                name: "Enumerations");

            migrationBuilder.CreateTable(
                name: "ENUMERATION",
                columns: table => new
                {
                    EN_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EN_CODE = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EN_NOM = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENUMERATION", x => x.EN_ID);
                });

            migrationBuilder.CreateTable(
                name: "ENUMERATION_VALUE",
                columns: table => new
                {
                    EV_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EV_VALEUR = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EN_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENUMERATION_VALUE", x => x.EV_ID);
                    table.ForeignKey(
                        name: "FK_ENUMERATION_VALUE_ENUMERATION_EN_ID",
                        column: x => x.EN_ID,
                        principalTable: "ENUMERATION",
                        principalColumn: "EN_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "U_ID",
                keyValue: 1,
                columns: new[] { "U_PASSWORD_HASH", "U_PASSWORD_SALT" },
                values: new object[] { new byte[] { 45, 108, 72, 114, 216, 36, 60, 66, 33, 28, 38, 96, 127, 46, 81, 244, 237, 219, 131, 214, 22, 155, 40, 128, 232, 201, 14, 82, 40, 102, 140, 26, 194, 57, 28, 28, 172, 126, 70, 68, 180, 211, 89, 116, 234, 159, 220, 14, 72, 114, 23, 55, 153, 99, 0, 111, 7, 79, 112, 27, 10, 12, 129, 19 }, new byte[] { 176, 243, 65, 132, 9, 162, 46, 243, 77, 231, 231, 21, 149, 6, 10, 125, 74, 176, 146, 222, 77, 9, 244, 25, 176, 60, 179, 248, 196, 218, 246, 232, 65, 225, 112, 13, 42, 142, 144, 93, 162, 113, 243, 222, 8, 189, 162, 27, 124, 204, 34, 109, 160, 237, 183, 250, 165, 35, 231, 12, 200, 27, 146, 222, 7, 30, 181, 89, 56, 235, 172, 35, 25, 210, 60, 183, 87, 185, 169, 11, 83, 20, 171, 145, 125, 123, 169, 120, 10, 174, 244, 115, 160, 237, 247, 192, 176, 35, 60, 27, 253, 70, 83, 56, 194, 84, 29, 64, 29, 130, 4, 27, 88, 157, 144, 206, 196, 159, 223, 167, 87, 30, 214, 15, 6, 87, 255, 30 } });

            migrationBuilder.CreateIndex(
                name: "IX_ENUMERATION_VALUE_EN_ID",
                table: "ENUMERATION_VALUE",
                column: "EN_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ENUMERATION_VALUE");

            migrationBuilder.DropTable(
                name: "ENUMERATION");

            migrationBuilder.CreateTable(
                name: "Enumerations",
                columns: table => new
                {
                    EN_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EN_CODE = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EN_NOM = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enumerations", x => x.EN_ID);
                });

            migrationBuilder.CreateTable(
                name: "enumerationValeurs",
                columns: table => new
                {
                    EV_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EN_ID = table.Column<int>(type: "int", nullable: false),
                    EV_VALEUR = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enumerationValeurs", x => x.EV_ID);
                    table.ForeignKey(
                        name: "FK_enumerationValeurs_Enumerations_EN_ID",
                        column: x => x.EN_ID,
                        principalTable: "Enumerations",
                        principalColumn: "EN_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "U_ID",
                keyValue: 1,
                columns: new[] { "U_PASSWORD_HASH", "U_PASSWORD_SALT" },
                values: new object[] { new byte[] { 70, 117, 160, 78, 166, 138, 239, 155, 46, 5, 13, 163, 254, 226, 108, 129, 104, 45, 166, 81, 54, 187, 62, 85, 221, 118, 118, 197, 5, 165, 34, 48, 206, 63, 20, 31, 215, 244, 255, 146, 225, 42, 20, 92, 119, 65, 66, 64, 133, 63, 219, 119, 88, 11, 20, 73, 148, 65, 241, 29, 235, 59, 80, 63 }, new byte[] { 13, 27, 227, 110, 197, 191, 68, 73, 79, 206, 137, 26, 68, 193, 118, 243, 91, 72, 108, 255, 51, 124, 42, 193, 246, 33, 110, 125, 35, 240, 126, 176, 225, 102, 59, 80, 234, 25, 112, 165, 67, 154, 82, 107, 183, 73, 242, 159, 241, 12, 200, 122, 92, 8, 60, 190, 220, 138, 162, 32, 202, 6, 104, 202, 151, 63, 115, 15, 171, 232, 13, 188, 104, 135, 178, 58, 226, 106, 69, 169, 139, 36, 112, 242, 44, 1, 150, 217, 163, 169, 206, 104, 172, 254, 63, 178, 228, 161, 35, 70, 214, 110, 192, 47, 25, 73, 201, 162, 252, 239, 69, 51, 154, 74, 159, 118, 136, 54, 191, 55, 87, 78, 139, 74, 248, 3, 174, 128 } });

            migrationBuilder.CreateIndex(
                name: "IX_enumerationValeurs_EN_ID",
                table: "enumerationValeurs",
                column: "EN_ID");
        }
    }
}
