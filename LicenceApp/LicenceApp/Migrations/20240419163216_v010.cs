using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LicenceApp.Migrations
{
    /// <inheritdoc />
    public partial class v010 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ATTRIBUTE_LICENCE_VALUE",
                columns: table => new
                {
                    ATTV_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ATTV_VALEUR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ATT_ID = table.Column<int>(type: "int", nullable: false),
                    LIC_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATTRIBUTE_LICENCE_VALUE", x => x.ATTV_ID);
                    table.ForeignKey(
                        name: "FK_ATTRIBUTE_LICENCE_VALUE_LICENCE_LIC_ID",
                        column: x => x.LIC_ID,
                        principalTable: "LICENCE",
                        principalColumn: "LIC_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "U_ID",
                keyValue: 1,
                columns: new[] { "U_PASSWORD_HASH", "U_PASSWORD_SALT" },
                values: new object[] { new byte[] { 58, 134, 218, 10, 111, 47, 105, 44, 253, 204, 29, 99, 196, 127, 214, 168, 231, 119, 105, 30, 165, 198, 198, 84, 85, 240, 225, 107, 226, 84, 141, 203, 27, 91, 25, 214, 56, 40, 19, 137, 91, 54, 217, 148, 48, 182, 90, 130, 7, 167, 202, 107, 30, 81, 65, 136, 126, 73, 222, 112, 25, 231, 187, 204 }, new byte[] { 217, 69, 241, 62, 80, 89, 27, 83, 62, 86, 131, 170, 129, 239, 105, 80, 13, 27, 201, 215, 167, 176, 134, 249, 119, 174, 129, 134, 6, 43, 20, 41, 22, 55, 207, 130, 64, 252, 28, 185, 183, 227, 15, 10, 69, 7, 129, 112, 127, 64, 64, 105, 219, 139, 30, 81, 231, 86, 105, 131, 63, 129, 153, 60, 53, 142, 200, 158, 154, 44, 75, 3, 29, 31, 125, 18, 32, 56, 174, 209, 10, 100, 93, 144, 6, 97, 155, 140, 166, 239, 39, 245, 47, 228, 3, 240, 198, 107, 195, 2, 80, 215, 70, 99, 121, 46, 104, 61, 2, 222, 162, 175, 127, 93, 248, 61, 128, 25, 230, 217, 86, 177, 111, 26, 45, 123, 80, 196 } });

            migrationBuilder.CreateIndex(
                name: "IX_ATTRIBUTE_LICENCE_VALUE_LIC_ID",
                table: "ATTRIBUTE_LICENCE_VALUE",
                column: "LIC_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ATTRIBUTE_LICENCE_VALUE");

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "U_ID",
                keyValue: 1,
                columns: new[] { "U_PASSWORD_HASH", "U_PASSWORD_SALT" },
                values: new object[] { new byte[] { 213, 119, 145, 182, 4, 46, 181, 119, 46, 55, 221, 221, 105, 122, 43, 110, 30, 236, 15, 112, 11, 151, 156, 127, 103, 179, 180, 106, 42, 143, 79, 119, 250, 252, 36, 134, 48, 168, 253, 155, 211, 209, 3, 90, 190, 222, 19, 10, 22, 102, 251, 117, 112, 90, 138, 96, 135, 131, 197, 205, 99, 11, 133, 199 }, new byte[] { 109, 20, 177, 18, 86, 141, 86, 98, 219, 73, 204, 6, 207, 224, 147, 195, 236, 145, 242, 209, 15, 166, 93, 62, 102, 96, 238, 24, 190, 123, 166, 76, 176, 206, 89, 28, 0, 39, 197, 6, 72, 11, 79, 168, 127, 188, 166, 2, 241, 88, 131, 172, 170, 151, 93, 221, 104, 166, 156, 252, 145, 216, 182, 138, 155, 130, 6, 249, 130, 64, 192, 66, 86, 211, 99, 200, 200, 100, 176, 131, 99, 29, 156, 73, 115, 238, 213, 122, 178, 160, 117, 45, 45, 10, 165, 2, 189, 185, 16, 18, 253, 158, 100, 53, 57, 46, 79, 183, 93, 46, 52, 121, 30, 190, 255, 15, 177, 103, 151, 233, 100, 240, 54, 169, 188, 121, 22, 56 } });
        }
    }
}
