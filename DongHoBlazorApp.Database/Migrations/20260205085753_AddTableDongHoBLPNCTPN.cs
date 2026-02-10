using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DongHoBlazorApp.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddTableDongHoBLPNCTPN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DongHos",
                columns: table => new
                {
                    MaDongHo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDongHo = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    MaPL = table.Column<int>(type: "int", nullable: false),
                    GiaBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaNCC = table.Column<int>(type: "int", nullable: false),
                    MaThuongHieu = table.Column<int>(type: "int", nullable: false),
                    SoLuongTon = table.Column<int>(type: "int", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LuotXem = table.Column<int>(type: "int", nullable: false),
                    LuotBinhLuan = table.Column<int>(type: "int", nullable: false),
                    DaBan = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DongHos", x => x.MaDongHo);
                    table.ForeignKey(
                        name: "FK_DongHos_NhaCungCaps_MaNCC",
                        column: x => x.MaNCC,
                        principalTable: "NhaCungCaps",
                        principalColumn: "MaNCC",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DongHos_PhanLoais_MaPL",
                        column: x => x.MaPL,
                        principalTable: "PhanLoais",
                        principalColumn: "MaPL",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DongHos_ThuongHieus_MaThuongHieu",
                        column: x => x.MaThuongHieu,
                        principalTable: "ThuongHieus",
                        principalColumn: "MaThuongHieu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhieuNhaps",
                columns: table => new
                {
                    MaPhieuNhap = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNCC = table.Column<int>(type: "int", nullable: false),
                    NgayNhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuNhaps", x => x.MaPhieuNhap);
                    table.ForeignKey(
                        name: "FK_PhieuNhaps_NhaCungCaps_MaNCC",
                        column: x => x.MaNCC,
                        principalTable: "NhaCungCaps",
                        principalColumn: "MaNCC",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BinhLuans",
                columns: table => new
                {
                    MaBL = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoiDungBL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaThanhVien = table.Column<int>(type: "int", nullable: false),
                    MaDongHo = table.Column<int>(type: "int", nullable: false),
                    TenThanhVien = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NgayBinhLuan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinhLuans", x => x.MaBL);
                    table.ForeignKey(
                        name: "FK_BinhLuans_DongHos_MaDongHo",
                        column: x => x.MaDongHo,
                        principalTable: "DongHos",
                        principalColumn: "MaDongHo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietPhieuNhapModels",
                columns: table => new
                {
                    MaChiTietPhieuNhap = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhieuNhap = table.Column<int>(type: "int", nullable: false),
                    MaDongHo = table.Column<int>(type: "int", nullable: false),
                    SoLuongNhap = table.Column<int>(type: "int", nullable: false),
                    DonGiaNhap = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietPhieuNhapModels", x => x.MaChiTietPhieuNhap);
                    table.ForeignKey(
                        name: "FK_ChiTietPhieuNhapModels_DongHos_MaDongHo",
                        column: x => x.MaDongHo,
                        principalTable: "DongHos",
                        principalColumn: "MaDongHo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChiTietPhieuNhapModels_PhieuNhaps_MaPhieuNhap",
                        column: x => x.MaPhieuNhap,
                        principalTable: "PhieuNhaps",
                        principalColumn: "MaPhieuNhap",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuans_MaDongHo",
                table: "BinhLuans",
                column: "MaDongHo");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuNhapModels_MaDongHo",
                table: "ChiTietPhieuNhapModels",
                column: "MaDongHo");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuNhapModels_MaPhieuNhap",
                table: "ChiTietPhieuNhapModels",
                column: "MaPhieuNhap");

            migrationBuilder.CreateIndex(
                name: "IX_DongHos_MaNCC",
                table: "DongHos",
                column: "MaNCC");

            migrationBuilder.CreateIndex(
                name: "IX_DongHos_MaPL",
                table: "DongHos",
                column: "MaPL");

            migrationBuilder.CreateIndex(
                name: "IX_DongHos_MaThuongHieu",
                table: "DongHos",
                column: "MaThuongHieu");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhaps_MaNCC",
                table: "PhieuNhaps",
                column: "MaNCC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BinhLuans");

            migrationBuilder.DropTable(
                name: "ChiTietPhieuNhapModels");

            migrationBuilder.DropTable(
                name: "DongHos");

            migrationBuilder.DropTable(
                name: "PhieuNhaps");
        }
    }
}
