using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BitirmeProjesiErp.Migrations.scf
{
    public partial class scfmig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeklifViewModel_Teklifs_teklif_key",
                table: "TeklifViewModel");

            migrationBuilder.AlterColumn<string>(
                name: "teklif_key",
                table: "TeklifViewModel",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_TeklifViewModel_Teklifs_teklif_key",
                table: "TeklifViewModel",
                column: "teklif_key",
                principalTable: "Teklifs",
                principalColumn: "_key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeklifViewModel_Teklifs_teklif_key",
                table: "TeklifViewModel");

            migrationBuilder.AlterColumn<string>(
                name: "teklif_key",
                table: "TeklifViewModel",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TeklifViewModel_Teklifs_teklif_key",
                table: "TeklifViewModel",
                column: "teklif_key",
                principalTable: "Teklifs",
                principalColumn: "_key",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
