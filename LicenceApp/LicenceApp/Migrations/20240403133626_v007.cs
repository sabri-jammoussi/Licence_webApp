using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LicenceApp.Migrations
{
    /// <inheritdoc />
    public partial class v007 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ATTRIBUTE_LICENCE",
                columns: table => new
                {
                    ATT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ATT_INTUTILE = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ATT_DESCRIPTION = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ATT_TYPE = table.Column<short>(type: "smallint", nullable: false),
                    APP_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATTRIBUTE_LICENCE", x => x.ATT_ID);
                    table.ForeignKey(
                        name: "FK_ATTRIBUTE_LICENCE_APPLICATION_APP_ID",
                        column: x => x.APP_ID,
                        principalTable: "APPLICATION",
                        principalColumn: "APP_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "U_ID",
                keyValue: 1,
                columns: new[] { "U_PASSWORD_HASH", "U_PASSWORD_SALT" },
                values: new object[] { new byte[] { 189, 243, 83, 27, 190, 221, 190, 156, 252, 160, 165, 151, 219, 135, 49, 9, 92, 24, 204, 101, 222, 134, 128, 147, 170, 149, 41, 158, 186, 1, 51, 146, 143, 185, 227, 81, 168, 199, 65, 132, 114, 142, 159, 38, 128, 226, 24, 44, 49, 9, 81, 8, 139, 202, 25, 160, 34, 198, 207, 146, 125, 129, 196, 10 }, new byte[] { 6, 21, 55, 130, 52, 176, 65, 30, 133, 147, 224, 44, 75, 69, 255, 94, 182, 92, 227, 169, 185, 94, 6, 147, 34, 189, 202, 71, 173, 48, 0, 28, 198, 184, 40, 147, 39, 99, 254, 91, 235, 79, 228, 159, 235, 36, 206, 103, 241, 8, 105, 167, 179, 40, 27, 15, 181, 144, 196, 15, 127, 141, 98, 90, 70, 215, 139, 123, 234, 193, 150, 154, 128, 124, 35, 62, 233, 240, 207, 159, 193, 161, 173, 187, 206, 65, 66, 213, 143, 238, 98, 116, 18, 228, 250, 68, 35, 230, 140, 204, 44, 45, 189, 205, 103, 210, 224, 152, 25, 214, 1, 178, 90, 94, 47, 121, 121, 249, 123, 228, 124, 78, 39, 177, 8, 29, 31, 204 } });

            migrationBuilder.CreateIndex(
                name: "IX_ATTRIBUTE_LICENCE_APP_ID",
                table: "ATTRIBUTE_LICENCE",
                column: "APP_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ATTRIBUTE_LICENCE");

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "U_ID",
                keyValue: 1,
                columns: new[] { "U_PASSWORD_HASH", "U_PASSWORD_SALT" },
                values: new object[] { new byte[] { 45, 108, 72, 114, 216, 36, 60, 66, 33, 28, 38, 96, 127, 46, 81, 244, 237, 219, 131, 214, 22, 155, 40, 128, 232, 201, 14, 82, 40, 102, 140, 26, 194, 57, 28, 28, 172, 126, 70, 68, 180, 211, 89, 116, 234, 159, 220, 14, 72, 114, 23, 55, 153, 99, 0, 111, 7, 79, 112, 27, 10, 12, 129, 19 }, new byte[] { 176, 243, 65, 132, 9, 162, 46, 243, 77, 231, 231, 21, 149, 6, 10, 125, 74, 176, 146, 222, 77, 9, 244, 25, 176, 60, 179, 248, 196, 218, 246, 232, 65, 225, 112, 13, 42, 142, 144, 93, 162, 113, 243, 222, 8, 189, 162, 27, 124, 204, 34, 109, 160, 237, 183, 250, 165, 35, 231, 12, 200, 27, 146, 222, 7, 30, 181, 89, 56, 235, 172, 35, 25, 210, 60, 183, 87, 185, 169, 11, 83, 20, 171, 145, 125, 123, 169, 120, 10, 174, 244, 115, 160, 237, 247, 192, 176, 35, 60, 27, 253, 70, 83, 56, 194, 84, 29, 64, 29, 130, 4, 27, 88, 157, 144, 206, 196, 159, 223, 167, 87, 30, 214, 15, 6, 87, 255, 30 } });
        }
    }
}
