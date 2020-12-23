using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryWeb.Data.Migrations
{
    public partial class Movement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProvinceName",
                table: "Provinces",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Exist",
                table: "Brunshes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Movements",
                columns: table => new
                {
                    ID_Movement = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Exist = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movements", x => x.ID_Movement);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movements");

            migrationBuilder.DropColumn(
                name: "Exist",
                table: "Brunshes");

            migrationBuilder.AlterColumn<string>(
                name: "ProvinceName",
                table: "Provinces",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
