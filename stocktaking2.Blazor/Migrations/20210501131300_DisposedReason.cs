using Microsoft.EntityFrameworkCore.Migrations;

namespace stocktaking2.Blazor.Migrations
{
    public partial class DisposedReason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisposeReason",
                table: "Units",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisposeReason",
                table: "Units");
        }
    }
}
