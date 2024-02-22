using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDProd.Migrations
{
    /// <inheritdoc />
    public partial class _3Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefProd",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RefLabos",
                columns: table => new
                {
                    RLAB_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RLAB_NOM = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    RLAB_ADRESSE1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RLAB_ADRESSE2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RLAB_CPOSTAL = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    RLAB_VILLE = table.Column<string>(type: "nvarchar(39)", maxLength: 39, nullable: false),
                    RLAB_TEL = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    RLAB_FAX = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    RLAB_URL = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    RLAB_SRC = table.Column<byte>(type: "tinyint", nullable: true),
                    RLAB_ID_SRC = table.Column<int>(type: "int", nullable: true),
                    RLAB_codeWP = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    RLAB_DELETED = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefLabos", x => x.RLAB_ID);
                });

            migrationBuilder.CreateTable(
                name: "RefProds",
                columns: table => new
                {
                    REF_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    REF_CODE13 = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    REF_LABIDMAJ = table.Column<int>(type: "int", nullable: true),
                    REF_NOM = table.Column<string>(type: "nvarchar(41)", maxLength: 41, nullable: false),
                    REF_ID_REMPLPAR = table.Column<int>(type: "int", nullable: true),
                    REF_SORTIE = table.Column<bool>(type: "bit", nullable: true),
                    REF_MAJ = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefProds", x => x.REF_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefLabos");

            migrationBuilder.DropTable(
                name: "RefProds");

            migrationBuilder.DropColumn(
                name: "RefProd",
                table: "Images");
        }
    }
}
