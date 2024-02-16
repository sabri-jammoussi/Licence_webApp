using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LicenceApp.Migrations
{
    /// <inheritdoc />
    public partial class New_seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USERS2",
                columns: table => new
                {
                    U_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    U_FIRSTNAME = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    U_LASTNAME = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    U_EMAIL = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    U_PASSWORD = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    U_ROLE = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS2", x => x.U_ID);
                });

            migrationBuilder.InsertData(
                table: "USERS2",
                columns: new[] { "U_ID", "U_EMAIL", "U_FIRSTNAME", "U_LASTNAME", "U_PASSWORD", "U_ROLE" },
                values: new object[] { 1, "sabrijm123@gmail.com", "sabri", "jammoussi", "Sabri123", (short)0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USERS2");
        }
    }
}
