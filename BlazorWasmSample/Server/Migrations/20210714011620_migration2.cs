using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorWasmSample.Server.Migrations
{
    public partial class migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataSources_AspNetUsers_ApplicationUserId",
                table: "DataSources");

            migrationBuilder.DropIndex(
                name: "IX_DataSources_ApplicationUserId",
                table: "DataSources");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "DataSources");

            migrationBuilder.CreateTable(
                name: "ApplicationUserDashboardDataSourceEntity",
                columns: table => new
                {
                    AvailableDashboardDataSourcesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserDashboardDataSourceEntity", x => new { x.AvailableDashboardDataSourcesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserDashboardDataSourceEntity_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserDashboardDataSourceEntity_DataSources_AvailableDashboardDataSourcesId",
                        column: x => x.AvailableDashboardDataSourcesId,
                        principalTable: "DataSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserDashboardDataSourceEntity_UsersId",
                table: "ApplicationUserDashboardDataSourceEntity",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserDashboardDataSourceEntity");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "DataSources",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DataSources_ApplicationUserId",
                table: "DataSources",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DataSources_AspNetUsers_ApplicationUserId",
                table: "DataSources",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
