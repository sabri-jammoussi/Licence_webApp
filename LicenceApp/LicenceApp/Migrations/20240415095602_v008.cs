using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LicenceApp.Migrations
{
    /// <inheritdoc />
    public partial class v008 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LICENCE",
                columns: table => new
                {
                    LIC_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LIC_DATE_EXP = table.Column<DateTime>(type: "Date", nullable: true),
                    LIC_USER_ID = table.Column<int>(type: "int", nullable: false),
                    APP_ID = table.Column<int>(type: "int", nullable: false),
                    CL_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LICENCE", x => x.LIC_ID);
                    table.ForeignKey(
                        name: "FK_LICENCE_APPLICATION_APP_ID",
                        column: x => x.APP_ID,
                        principalTable: "APPLICATION",
                        principalColumn: "APP_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LICENCE_CLIENTS_CL_ID",
                        column: x => x.CL_ID,
                        principalTable: "CLIENTS",
                        principalColumn: "CL_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "U_ID",
                keyValue: 1,
                columns: new[] { "U_PASSWORD_HASH", "U_PASSWORD_SALT" },
                values: new object[] { new byte[] { 42, 207, 173, 11, 47, 129, 168, 237, 223, 152, 132, 39, 88, 146, 162, 183, 190, 183, 178, 154, 104, 186, 154, 79, 181, 232, 72, 106, 129, 248, 63, 3, 209, 84, 120, 89, 177, 71, 43, 5, 204, 212, 105, 189, 180, 126, 45, 103, 167, 228, 110, 175, 44, 50, 177, 132, 224, 127, 118, 209, 94, 238, 177, 127 }, new byte[] { 127, 242, 160, 186, 92, 231, 234, 5, 91, 45, 169, 184, 206, 222, 4, 137, 195, 194, 25, 148, 243, 102, 16, 107, 159, 25, 66, 193, 25, 40, 129, 35, 117, 120, 106, 71, 7, 8, 113, 181, 29, 222, 142, 172, 165, 106, 58, 95, 160, 104, 232, 223, 118, 89, 18, 105, 122, 151, 115, 5, 108, 115, 213, 228, 100, 195, 137, 210, 82, 45, 196, 122, 159, 211, 59, 227, 229, 147, 125, 211, 121, 56, 242, 115, 88, 59, 177, 12, 168, 65, 103, 137, 118, 196, 222, 197, 41, 135, 115, 61, 90, 240, 17, 153, 68, 183, 63, 87, 143, 98, 67, 188, 12, 159, 170, 45, 148, 133, 248, 224, 236, 201, 154, 81, 104, 182, 33, 176 } });

            migrationBuilder.CreateIndex(
                name: "IX_LICENCE_APP_ID",
                table: "LICENCE",
                column: "APP_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LICENCE_CL_ID",
                table: "LICENCE",
                column: "CL_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LICENCE");

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "U_ID",
                keyValue: 1,
                columns: new[] { "U_PASSWORD_HASH", "U_PASSWORD_SALT" },
                values: new object[] { new byte[] { 189, 243, 83, 27, 190, 221, 190, 156, 252, 160, 165, 151, 219, 135, 49, 9, 92, 24, 204, 101, 222, 134, 128, 147, 170, 149, 41, 158, 186, 1, 51, 146, 143, 185, 227, 81, 168, 199, 65, 132, 114, 142, 159, 38, 128, 226, 24, 44, 49, 9, 81, 8, 139, 202, 25, 160, 34, 198, 207, 146, 125, 129, 196, 10 }, new byte[] { 6, 21, 55, 130, 52, 176, 65, 30, 133, 147, 224, 44, 75, 69, 255, 94, 182, 92, 227, 169, 185, 94, 6, 147, 34, 189, 202, 71, 173, 48, 0, 28, 198, 184, 40, 147, 39, 99, 254, 91, 235, 79, 228, 159, 235, 36, 206, 103, 241, 8, 105, 167, 179, 40, 27, 15, 181, 144, 196, 15, 127, 141, 98, 90, 70, 215, 139, 123, 234, 193, 150, 154, 128, 124, 35, 62, 233, 240, 207, 159, 193, 161, 173, 187, 206, 65, 66, 213, 143, 238, 98, 116, 18, 228, 250, 68, 35, 230, 140, 204, 44, 45, 189, 205, 103, 210, 224, 152, 25, 214, 1, 178, 90, 94, 47, 121, 121, 249, 123, 228, 124, 78, 39, 177, 8, 29, 31, 204 } });
        }
    }
}
