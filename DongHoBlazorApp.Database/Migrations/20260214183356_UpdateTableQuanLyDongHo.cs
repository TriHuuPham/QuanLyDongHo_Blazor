using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DongHoBlazorApp.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableQuanLyDongHo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DaThanhToan",
                table: "DonDatHangs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DiaChiGiaoHang",
                table: "DonDatHangs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayGiao",
                table: "DonDatHangs",
                type: "datetime",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaThanhToan",
                table: "DonDatHangs");

            migrationBuilder.DropColumn(
                name: "DiaChiGiaoHang",
                table: "DonDatHangs");

            migrationBuilder.DropColumn(
                name: "NgayGiao",
                table: "DonDatHangs");
        }
    }
}
