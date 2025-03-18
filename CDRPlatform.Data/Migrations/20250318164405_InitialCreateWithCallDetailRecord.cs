using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CDRPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateWithCallDetailRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CallDetailRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CallerId = table.Column<long>(type: "bigint", nullable: true),
                    Recipient = table.Column<long>(type: "bigint", nullable: true),
                    CallDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(20,10)", precision: 20, scale: 10, nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallDetailRecord", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CallDetailRecord");
        }
    }
}
