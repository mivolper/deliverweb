using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryWeb.Data.Migrations
{
    public partial class CustomerOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerOrders",
                columns: table => new
                {
                    ID_SubOrder = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerCode = table.Column<string>(nullable: true),
                    Recipient = table.Column<string>(nullable: false),
                    RecipientPhone1 = table.Column<string>(nullable: false),
                    RecipientPhone2 = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    ID_City = table.Column<int>(nullable: false),
                    PackagePrice = table.Column<float>(nullable: false),
                    PackageNumber = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrders", x => x.ID_SubOrder);
                    table.ForeignKey(
                        name: "FK_CustomerOrders_Cities_ID_City",
                        column: x => x.ID_City,
                        principalTable: "Cities",
                        principalColumn: "ID_City",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrders_ID_City",
                table: "CustomerOrders",
                column: "ID_City");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerOrders");
        }
    }
}
