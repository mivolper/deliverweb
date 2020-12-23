using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryWeb.Data.Migrations
{
    public partial class Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID_Order = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Barcode = table.Column<string>(nullable: true),
                    Customer = table.Column<string>(nullable: true),
                    CustomerPhone = table.Column<string>(nullable: true),
                    Recipient = table.Column<string>(nullable: true),
                    RecipientPhone1 = table.Column<string>(nullable: true),
                    RecipientPhone2 = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    DeliveryType = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    PackagePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PackageNumber = table.Column<int>(nullable: false),
                    DeliveryPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Delegate = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    User = table.Column<string>(nullable: true),
                    Exist = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    ID_City = table.Column<int>(nullable: false),
                    DateState = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID_Order);
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
