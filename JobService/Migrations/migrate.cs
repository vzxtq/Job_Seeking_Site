using Microsoft.EntityFrameworkCore.Migrations;

namespace JobService.Migrations
{
    public partial class Migrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Jobs");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Jobs",
                type: "uuid",
                nullable: false,
                defaultValueSql: "gen_random_uuid()");
        }
    }
}
