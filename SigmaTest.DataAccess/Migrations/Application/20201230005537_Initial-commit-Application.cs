using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SigmaTest.DataAccess.Migrations.Application
{
    public partial class InitialcommitApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shortner",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LongUrl = table.Column<string>(nullable: false),
                    ShortenedUrl = table.Column<string>(nullable: false),
                    added = table.Column<DateTime>(nullable: false),
                    Ip = table.Column<string>(maxLength: 50, nullable: false),
                    NumOfClicks = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shortner", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shortner");
        }
    }
}
