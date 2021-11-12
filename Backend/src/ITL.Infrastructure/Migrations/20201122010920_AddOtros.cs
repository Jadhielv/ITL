using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITL.Infrastructure.Migrations
{
    public partial class AddOtros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Permissions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 21, 21, 9, 20, 26, DateTimeKind.Local).AddTicks(2914),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 21, 13, 4, 33, 478, DateTimeKind.Local).AddTicks(7471));

            migrationBuilder.InsertData(
                table: "PermissionTypes",
                columns: new[] { "Id", "Description" },
                values: new object[] { 3, "Otros" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PermissionTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Permissions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 21, 13, 4, 33, 478, DateTimeKind.Local).AddTicks(7471),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 11, 21, 21, 9, 20, 26, DateTimeKind.Local).AddTicks(2914));
        }
    }
}
