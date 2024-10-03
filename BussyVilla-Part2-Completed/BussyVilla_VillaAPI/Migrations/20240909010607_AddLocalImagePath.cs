using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BussyVilla_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddLocalImagePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageLocalPath",
                table: "Villas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ImageLocalPath" },
                values: new object[] { new DateTime(2024, 9, 9, 2, 6, 6, 788, DateTimeKind.Local).AddTicks(5831), null });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ImageLocalPath" },
                values: new object[] { new DateTime(2024, 9, 9, 2, 6, 6, 788, DateTimeKind.Local).AddTicks(5886), null });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ImageLocalPath" },
                values: new object[] { new DateTime(2024, 9, 9, 2, 6, 6, 788, DateTimeKind.Local).AddTicks(5888), null });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ImageLocalPath" },
                values: new object[] { new DateTime(2024, 9, 9, 2, 6, 6, 788, DateTimeKind.Local).AddTicks(5891), null });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ImageLocalPath" },
                values: new object[] { new DateTime(2024, 9, 9, 2, 6, 6, 788, DateTimeKind.Local).AddTicks(5893), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageLocalPath",
                table: "Villas");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 9, 8, 23, 32, 20, 910, DateTimeKind.Local).AddTicks(1695));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 9, 8, 23, 32, 20, 910, DateTimeKind.Local).AddTicks(1744));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 9, 8, 23, 32, 20, 910, DateTimeKind.Local).AddTicks(1748));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 9, 8, 23, 32, 20, 910, DateTimeKind.Local).AddTicks(1750));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 9, 8, 23, 32, 20, 910, DateTimeKind.Local).AddTicks(1753));
        }
    }
}
