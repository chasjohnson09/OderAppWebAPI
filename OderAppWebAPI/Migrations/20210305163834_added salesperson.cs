using Microsoft.EntityFrameworkCore.Migrations;

namespace OderAppWebAPI.Migrations
{
    public partial class addedsalesperson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalesPersonId",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalesPersonId1",
                table: "Order",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SalesPerson",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Statecode = table.Column<string>(maxLength: 2, nullable: false),
                    Sales = table.Column<decimal>(type: "Decimal (9,2)", nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesPerson", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_SalesPersonId1",
                table: "Order",
                column: "SalesPersonId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_SalesPerson_SalesPersonId1",
                table: "Order",
                column: "SalesPersonId1",
                principalTable: "SalesPerson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_SalesPerson_SalesPersonId1",
                table: "Order");

            migrationBuilder.DropTable(
                name: "SalesPerson");

            migrationBuilder.DropIndex(
                name: "IX_Order_SalesPersonId1",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "SalesPersonId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "SalesPersonId1",
                table: "Order");
        }
    }
}
