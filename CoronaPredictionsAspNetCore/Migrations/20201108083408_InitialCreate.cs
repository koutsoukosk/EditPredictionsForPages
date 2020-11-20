using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoronaPredictionsAspNetCore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Nickname = table.Column<string>(maxLength: 50, nullable: true),
                    WorkDescription = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerID);
                });

            migrationBuilder.CreateTable(
                name: "Prediction",
                columns: table => new
                {
                    PredictionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfPrediction = table.Column<int>(nullable: false),
                    DateOfPrediction = table.Column<DateTime>(nullable: false),
                    PlayerName = table.Column<string>(maxLength: 50, nullable: false),
                    CasesOfPrediction = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prediction", x => x.PredictionID);
                });

            migrationBuilder.CreateTable(
                name: "RealCasesEachDay",
                columns: table => new
                {
                    RealCaseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfRealCases = table.Column<int>(nullable: false),
                    DateOfRealCases = table.Column<DateTime>(nullable: false),
                    RealCasesNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealCasesEachDay", x => x.RealCaseID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Prediction");

            migrationBuilder.DropTable(
                name: "RealCasesEachDay");
        }
    }
}
