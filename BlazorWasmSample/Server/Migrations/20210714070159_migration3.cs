using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorWasmSample.Server.Migrations
{
    public partial class migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DbTableEntity_AspNetUsers_ApplicationUserId",
                table: "DbTableEntity");

            migrationBuilder.DropIndex(
                name: "IX_DbTableEntity_ApplicationUserId",
                table: "DbTableEntity");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "DbTableEntity");

            migrationBuilder.CreateTable(
                name: "ApplicationUserDbTableEntity",
                columns: table => new
                {
                    AvailableDbTablesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserDbTableEntity", x => new { x.AvailableDbTablesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserDbTableEntity_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserDbTableEntity_DbTableEntity_AvailableDbTablesId",
                        column: x => x.AvailableDbTablesId,
                        principalTable: "DbTableEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserDbTableEntity_UsersId",
                table: "ApplicationUserDbTableEntity",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserDbTableEntity");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "DbTableEntity",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DbTableEntity_ApplicationUserId",
                table: "DbTableEntity",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DbTableEntity_AspNetUsers_ApplicationUserId",
                table: "DbTableEntity",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
