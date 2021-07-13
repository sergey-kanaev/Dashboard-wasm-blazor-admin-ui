using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorWasmSample.Server.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllowCreateDashboard",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AllowDeleteDashboard",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AllowReadDashboard",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AllowUpdateDashboard",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DashboardModels",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    XmlContent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User_Dashboard",
                columns: table => new
                {
                    DashboardId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_Dashboard");

            migrationBuilder.DropTable(
                name: "DashboardModels");

            migrationBuilder.DropColumn(
                name: "AllowCreateDashboard",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AllowDeleteDashboard",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AllowReadDashboard",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AllowUpdateDashboard",
                table: "AspNetUsers");
        }
    }
}
