using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LicenceApp.Migrations
{
    /// <inheritdoc />
    public partial class v001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "U_LASTNAME",
                table: "USERS",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "U_FIRSTNAME",
                table: "USERS",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "U_EMAIL",
                table: "USERS",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CLIENTS",
                columns: table => new
                {
                    U_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    U_RAISON_SOCIAL = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    U_IDENTIFIANT = table.Column<int>(type: "int", maxLength: 13, nullable: true),
                    U_TELEPHONE = table.Column<int>(type: "int", maxLength: 8, nullable: false),
                    U_EMAIL = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    U_VILLE = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    U_ADRESSE = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    U_CODE_POSTAL = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    U_PAYS = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTS", x => x.U_ID);
                });

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "U_ID",
                keyValue: 1,
                columns: new[] { "U_PASSWORD_HASH", "U_PASSWORD_SALT" },
                values: new object[] { new byte[] { 34, 77, 63, 43, 62, 226, 107, 108, 111, 57, 119, 95, 203, 12, 133, 69, 167, 133, 193, 216, 21, 221, 145, 234, 103, 227, 41, 194, 61, 197, 168, 101, 126, 31, 120, 186, 250, 125, 113, 108, 215, 44, 100, 110, 90, 159, 1, 245, 32, 166, 251, 94, 239, 106, 133, 95, 16, 36, 210, 99, 110, 75, 55, 214 }, new byte[] { 55, 156, 16, 166, 29, 68, 38, 188, 140, 40, 38, 186, 124, 229, 163, 80, 248, 214, 64, 61, 76, 102, 42, 62, 55, 6, 253, 34, 171, 241, 161, 33, 163, 224, 42, 130, 69, 5, 194, 39, 180, 245, 73, 202, 166, 248, 33, 209, 169, 104, 8, 248, 12, 107, 37, 2, 48, 239, 74, 90, 95, 146, 14, 156, 119, 90, 138, 126, 144, 111, 104, 178, 29, 96, 1, 220, 46, 80, 66, 180, 186, 28, 149, 132, 8, 29, 56, 32, 22, 22, 152, 163, 122, 235, 220, 86, 22, 207, 48, 175, 184, 147, 29, 221, 131, 105, 190, 200, 222, 6, 53, 88, 249, 147, 95, 211, 215, 74, 49, 8, 147, 250, 180, 195, 76, 168, 245, 38 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CLIENTS");

            migrationBuilder.AlterColumn<string>(
                name: "U_LASTNAME",
                table: "USERS",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "U_FIRSTNAME",
                table: "USERS",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "U_EMAIL",
                table: "USERS",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.UpdateData(
                table: "USERS",
                keyColumn: "U_ID",
                keyValue: 1,
                columns: new[] { "U_PASSWORD_HASH", "U_PASSWORD_SALT" },
                values: new object[] { new byte[] { 144, 111, 105, 83, 171, 165, 211, 1, 209, 142, 172, 55, 32, 47, 113, 11, 28, 224, 38, 83, 255, 71, 41, 217, 23, 241, 175, 180, 35, 3, 37, 90, 145, 43, 3, 104, 39, 70, 234, 111, 106, 65, 237, 9, 143, 4, 139, 125, 206, 159, 153, 43, 67, 187, 129, 96, 55, 173, 174, 9, 21, 199, 20, 253 }, new byte[] { 104, 30, 72, 204, 111, 206, 236, 20, 95, 117, 6, 29, 161, 107, 139, 123, 47, 251, 29, 198, 91, 49, 17, 110, 105, 212, 203, 234, 242, 208, 180, 145, 167, 173, 211, 61, 212, 37, 27, 108, 118, 58, 162, 193, 146, 119, 95, 122, 98, 142, 237, 101, 63, 118, 19, 23, 172, 122, 63, 197, 82, 197, 239, 240, 161, 71, 17, 145, 30, 11, 122, 237, 253, 98, 214, 112, 6, 108, 24, 1, 31, 218, 57, 13, 125, 32, 97, 255, 37, 11, 245, 76, 214, 183, 249, 177, 236, 168, 121, 51, 168, 104, 82, 165, 84, 153, 101, 119, 172, 112, 55, 73, 123, 137, 61, 251, 61, 74, 88, 73, 8, 228, 66, 237, 221, 87, 92, 3 } });
        }
    }
}
