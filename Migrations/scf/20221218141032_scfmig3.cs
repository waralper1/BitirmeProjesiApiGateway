using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BitirmeProjesiErp.Migrations.scf
{
    public partial class scfmig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeklifKalemis_Teklifs_teklif_key",
                table: "TeklifKalemis");

            migrationBuilder.AlterColumn<string>(
                name: "teklif_key",
                table: "TeklifKalemis",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_TeklifKalemis_Teklifs_teklif_key",
                table: "TeklifKalemis",
                column: "teklif_key",
                principalTable: "Teklifs",
                principalColumn: "_key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeklifKalemis_Teklifs_teklif_key",
                table: "TeklifKalemis");

            migrationBuilder.AlterColumn<string>(
                name: "teklif_key",
                table: "TeklifKalemis",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TeklifKalemis_Teklifs_teklif_key",
                table: "TeklifKalemis",
                column: "teklif_key",
                principalTable: "Teklifs",
                principalColumn: "_key",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
