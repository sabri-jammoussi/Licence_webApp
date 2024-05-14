using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LicenceApp.Migrations
{
    /// <inheritdoc />
    public partial class v012 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PARTENAIRE",
                columns: table => new
                {
                    APP_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PR_RAISON_SOCIAL = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PR_TELEPHONE = table.Column<int>(type: "int", maxLength: 8, nullable: false),
                    PR_EMAIL = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PR_VILLE = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PR_ADRESSE = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PR_RESPONSABLE = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PR_PAYS = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PARTENAIRE", x => x.APP_ID);
                });

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "U_ID",
                keyValue: 1,
                columns: new[] { "U_PASSWORD_HASH", "U_PASSWORD_SALT" },
                values: new object[] { new byte[] { 91, 169, 97, 225, 194, 10, 89, 192, 44, 231, 210, 86, 148, 184, 47, 144, 242, 50, 251, 164, 97, 163, 194, 20, 99, 238, 237, 229, 212, 180, 95, 115, 77, 244, 91, 143, 202, 188, 247, 157, 7, 62, 67, 221, 208, 74, 221, 72, 252, 140, 197, 206, 241, 27, 152, 96, 199, 230, 125, 251, 238, 55, 205, 80 }, new byte[] { 192, 152, 6, 87, 252, 234, 241, 214, 145, 42, 121, 43, 180, 42, 208, 188, 73, 85, 240, 242, 218, 112, 99, 231, 154, 36, 97, 53, 143, 48, 51, 69, 52, 241, 216, 169, 220, 33, 65, 172, 231, 70, 0, 118, 91, 80, 147, 81, 23, 142, 141, 181, 136, 167, 27, 179, 38, 140, 74, 239, 78, 60, 122, 192, 1, 4, 29, 82, 0, 226, 109, 191, 237, 134, 42, 81, 22, 222, 75, 192, 40, 9, 119, 15, 151, 84, 183, 94, 73, 41, 29, 174, 65, 150, 138, 125, 182, 132, 68, 115, 208, 89, 228, 248, 7, 183, 126, 186, 5, 43, 198, 179, 123, 36, 189, 27, 161, 226, 231, 238, 182, 34, 57, 145, 187, 63, 50, 108 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PARTENAIRE");

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "U_ID",
                keyValue: 1,
                columns: new[] { "U_PASSWORD_HASH", "U_PASSWORD_SALT" },
                values: new object[] { new byte[] { 149, 250, 200, 227, 102, 193, 170, 83, 199, 148, 216, 17, 193, 146, 134, 63, 79, 86, 160, 119, 70, 72, 71, 154, 129, 0, 41, 5, 18, 209, 93, 186, 152, 77, 33, 216, 194, 143, 106, 140, 152, 28, 155, 3, 251, 141, 60, 61, 175, 211, 6, 196, 91, 109, 128, 148, 66, 213, 212, 79, 45, 209, 99, 212 }, new byte[] { 164, 228, 125, 9, 152, 5, 106, 255, 121, 79, 185, 14, 182, 221, 87, 147, 98, 29, 224, 254, 7, 216, 207, 46, 10, 159, 31, 194, 143, 204, 58, 45, 55, 208, 102, 154, 190, 240, 251, 183, 230, 198, 51, 128, 255, 104, 91, 203, 118, 109, 19, 239, 38, 235, 120, 134, 109, 228, 72, 87, 62, 51, 173, 0, 144, 84, 195, 120, 63, 41, 214, 168, 76, 12, 164, 144, 84, 134, 40, 250, 84, 54, 77, 112, 187, 38, 6, 116, 181, 136, 233, 18, 222, 32, 170, 162, 140, 74, 106, 178, 158, 166, 130, 198, 68, 55, 159, 92, 32, 234, 31, 208, 30, 102, 80, 26, 225, 12, 50, 38, 214, 221, 35, 77, 88, 66, 6, 179 } });
        }
    }
}
