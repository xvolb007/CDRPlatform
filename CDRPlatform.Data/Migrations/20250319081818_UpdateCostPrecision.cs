using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CDRPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCostPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                table: "CallDetailRecord",
                type: "decimal(20,3)",
                precision: 20,
                scale: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,10)",
                oldPrecision: 20,
                oldScale: 10);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                table: "CallDetailRecord",
                type: "decimal(20,10)",
                precision: 20,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,3)",
                oldPrecision: 20,
                oldScale: 3);
        }
    }
}
