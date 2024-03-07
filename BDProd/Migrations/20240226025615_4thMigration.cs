using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDProd.Migrations
{
    /// <inheritdoc />
    public partial class _4thMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoImgs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    opr_id = table.Column<int>(type: "int", nullable: true),
                    event_id = table.Column<int>(type: "int", nullable: false),
                    ref_id = table.Column<int>(type: "int", nullable: true),
                    npp = table.Column<int>(type: "int", nullable: true),
                    cnt_img = table.Column<short>(type: "smallint", nullable: true),
                    cnt_add = table.Column<short>(type: "smallint", nullable: true),
                    cnt_mod = table.Column<short>(type: "smallint", nullable: true),
                    cnt_del = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoImgs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PGroups",
                columns: table => new
                {
                    REF_ID1 = table.Column<int>(type: "int", nullable: false),
                    REF_ID2 = table.Column<int>(type: "int", nullable: false),
                    TMCREATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OP_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PGroups", x => new { x.REF_ID1, x.REF_ID2 });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoImgs");

            migrationBuilder.DropTable(
                name: "PGroups");
        }
    }
}
