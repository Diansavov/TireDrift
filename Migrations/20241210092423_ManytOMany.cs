using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TireDrift.Migrations
{
    /// <inheritdoc />
    public partial class ManytOMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_Orders_OrderId",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Tires_Orders_OrderId",
                table: "Tires");

            migrationBuilder.DropIndex(
                name: "IX_Tires_OrderId",
                table: "Tires");

            migrationBuilder.DropIndex(
                name: "IX_Service_OrderId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Tires");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Service");

            migrationBuilder.CreateTable(
                name: "OrderService",
                columns: table => new
                {
                    OrdersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ServicesId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderService", x => new { x.OrdersId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_OrderService_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderService_Service_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderTire",
                columns: table => new
                {
                    OrdersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TiresId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTire", x => new { x.OrdersId, x.TiresId });
                    table.ForeignKey(
                        name: "FK_OrderTire_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderTire_Tires_TiresId",
                        column: x => x.TiresId,
                        principalTable: "Tires",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderService_ServicesId",
                table: "OrderService",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTire_TiresId",
                table: "OrderTire",
                column: "TiresId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderService");

            migrationBuilder.DropTable(
                name: "OrderTire");

            migrationBuilder.AddColumn<string>(
                name: "OrderId",
                table: "Tires",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderId",
                table: "Service",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tires_OrderId",
                table: "Tires",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_OrderId",
                table: "Service",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Orders_OrderId",
                table: "Service",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tires_Orders_OrderId",
                table: "Tires",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
