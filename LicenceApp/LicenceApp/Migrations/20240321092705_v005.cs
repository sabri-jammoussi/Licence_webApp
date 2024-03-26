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
                    EV_VALEUR = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EN_ID = table.Column<int>(type: "int", nullable: false)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "enumerationValeurs");

            migrationBuilder.DropTable(
                name: "Enumerations");

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "U_ID",
                keyValue: 1,
                columns: new[] { "U_PASSWORD_HASH", "U_PASSWORD_SALT" },
                values: new object[] { new byte[] { 169, 126, 205, 168, 81, 245, 175, 26, 105, 232, 199, 94, 172, 238, 160, 206, 7, 231, 250, 123, 252, 186, 88, 149, 237, 86, 32, 80, 217, 69, 53, 101, 216, 202, 22, 84, 223, 182, 206, 36, 192, 30, 111, 49, 225, 231, 59, 24, 145, 222, 210, 97, 143, 207, 212, 77, 233, 194, 244, 186, 102, 53, 141, 110 }, new byte[] { 172, 87, 175, 138, 252, 105, 103, 82, 74, 114, 124, 90, 58, 229, 45, 68, 244, 255, 174, 146, 171, 178, 153, 20, 99, 17, 178, 119, 132, 55, 125, 135, 88, 37, 120, 147, 168, 64, 81, 150, 147, 94, 30, 95, 161, 91, 90, 249, 108, 2, 97, 20, 115, 12, 251, 206, 93, 28, 122, 156, 8, 251, 142, 225, 159, 84, 178, 217, 169, 139, 12, 54, 132, 176, 165, 184, 246, 101, 110, 240, 158, 45, 78, 162, 224, 37, 231, 33, 138, 83, 166, 207, 225, 110, 153, 115, 29, 113, 225, 206, 152, 155, 237, 193, 245, 13, 145, 156, 218, 98, 29, 148, 36, 195, 226, 180, 207, 45, 241, 248, 165, 240, 47, 162, 222, 19, 141, 196 } });
        }
    }
}
