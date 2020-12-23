using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryWeb.Data.Migrations
{
    public partial class Brunsh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brunshes",
                columns: table => new
                {
                    ID_Brunsh = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrunshName = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Exist = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brunshes", x => x.ID_Brunsh);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brunshes");
        }
    }
}
