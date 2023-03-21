using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuariosApi_.Migrations
{
    public partial class SecondMigration_FieldUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Competencias",
                table: "tbAPI_Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InfoAdicionais",
                table: "tbAPI_Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "tbAPI_Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "9f4a7b86-8d33-4968-a8de-635fc9f2b4b7");

            migrationBuilder.UpdateData(
                table: "tbAPI_Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "698006e3-38fd-417a-ab67-5fcfcb6edd27");

            migrationBuilder.UpdateData(
                table: "tbAPI_Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "953ba123-84fb-4ed9-bbe0-153c939ec4c4");

            migrationBuilder.UpdateData(
                table: "tbAPI_Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a3c56f3f-e581-45ec-b273-b04b9409d699", "AQAAAAEAACcQAAAAEOd/SyipmRfXoVYPqkeN4s6GOU+gNvBkmTTbwp1IWZp/15nBHwz3mzVL3klLtSvdLg==", "46e21451-fa3e-4710-bb21-f54f1cc74ab9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Competencias",
                table: "tbAPI_Users");

            migrationBuilder.DropColumn(
                name: "InfoAdicionais",
                table: "tbAPI_Users");

            migrationBuilder.UpdateData(
                table: "tbAPI_Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8de58f2d-4a22-43a6-afd7-fef7079f5579");

            migrationBuilder.UpdateData(
                table: "tbAPI_Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "85f656b1-85c4-4c71-b543-def3c2558d90");

            migrationBuilder.UpdateData(
                table: "tbAPI_Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "2bf4e674-2326-46c4-884f-7c62b2c0edf8");

            migrationBuilder.UpdateData(
                table: "tbAPI_Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1323f342-3ef5-4039-8aee-fa2afd4c5b41", "AQAAAAEAACcQAAAAEJm+Ju/QJRj43O3dzFzLKCPuQNUFrNlPqSnadHDm8UGQRrG174phofyT4fJpaJuZ1Q==", "980513f5-6677-43c4-abd4-b8951c801406" });
        }
    }
}
