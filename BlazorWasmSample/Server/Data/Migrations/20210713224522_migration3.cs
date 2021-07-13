using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorWasmSample.Server.Data.Migrations
{
    public partial class migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_Dashboard");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "DashboardModels",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateTable(
                name: "ApplicationUserDashboardModel",
                columns: table => new
                {
                    DashboardsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserDashboardModel", x => new { x.DashboardsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserDashboardModel_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserDashboardModel_DashboardModels_DashboardsId",
                        column: x => x.DashboardsId,
                        principalTable: "DashboardModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConnectionStrings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConnectionString = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionStrings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DashboardDataSourceEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardDataSourceEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DashboardDataSourceEntity_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DbTableEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbTableEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DbTableEntity_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserConnectionStringEntity",
                columns: table => new
                {
                    ConnectionStringsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserConnectionStringEntity", x => new { x.ConnectionStringsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserConnectionStringEntity_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserConnectionStringEntity_ConnectionStrings_ConnectionStringsId",
                        column: x => x.ConnectionStringsId,
                        principalTable: "ConnectionStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserConnectionStringEntity_UsersId",
                table: "ApplicationUserConnectionStringEntity",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserDashboardModel_UsersId",
                table: "ApplicationUserDashboardModel",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_DashboardDataSourceEntity_ApplicationUserId",
                table: "DashboardDataSourceEntity",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DbTableEntity_ApplicationUserId",
                table: "DbTableEntity",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserConnectionStringEntity");

            migrationBuilder.DropTable(
                name: "ApplicationUserDashboardModel");

            migrationBuilder.DropTable(
                name: "DashboardDataSourceEntity");

            migrationBuilder.DropTable(
                name: "DbTableEntity");

            migrationBuilder.DropTable(
                name: "ConnectionStrings");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "DashboardModels",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateTable(
                name: "User_Dashboard",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DashboardId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Dashboard", x => new { x.UserId, x.DashboardId });
                    table.ForeignKey(
                        name: "FK_User_Dashboard_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Dashboard_DashboardModels_DashboardId",
                        column: x => x.DashboardId,
                        principalTable: "DashboardModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_Dashboard_DashboardId",
                table: "User_Dashboard",
                column: "DashboardId");
        }
    }
}
