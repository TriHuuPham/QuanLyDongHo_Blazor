using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DongHoBlazorApp.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddTableFullDH : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietPhieuNhapModels_DongHos_MaDongHo",
                table: "ChiTietPhieuNhapModels");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietPhieuNhapModels_PhieuNhaps_MaPhieuNhap",
                table: "ChiTietPhieuNhapModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietPhieuNhapModels",
                table: "ChiTietPhieuNhapModels");

            migrationBuilder.RenameTable(
                name: "ChiTietPhieuNhapModels",
                newName: "ChiTietPhieuNhaps");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietPhieuNhapModels_MaPhieuNhap",
                table: "ChiTietPhieuNhaps",
                newName: "IX_ChiTietPhieuNhaps_MaPhieuNhap");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietPhieuNhapModels_MaDongHo",
                table: "ChiTietPhieuNhaps",
                newName: "IX_ChiTietPhieuNhaps_MaDongHo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayNhap",
                table: "PhieuNhaps",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiNhap",
                table: "PhieuNhaps",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCapNhat",
                table: "DongHos",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayBinhLuan",
                table: "BinhLuans",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietPhieuNhaps",
                table: "ChiTietPhieuNhaps",
                column: "MaChiTietPhieuNhap");

            migrationBuilder.CreateTable(
                name: "LoaiThanhViens",
                columns: table => new
                {
                    MaLoaiTV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiTV = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UuDai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiThanhViens", x => x.MaLoaiTV);
                });

            migrationBuilder.CreateTable(
                name: "KhachHangs",
                columns: table => new
                {
                    MaKH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKH = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    MaLoaiTV = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHangs", x => x.MaKH);
                    table.ForeignKey(
                        name: "FK_KhachHangs_LoaiThanhViens_MaLoaiTV",
                        column: x => x.MaLoaiTV,
                        principalTable: "LoaiThanhViens",
                        principalColumn: "MaLoaiTV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThanhViens",
                columns: table => new
                {
                    MaThanhVien = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDangNhap = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    MatKhau = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    HoTen = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    MaLoaiTV = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    HinhAnh = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanhViens", x => x.MaThanhVien);
                    table.ForeignKey(
                        name: "FK_ThanhViens_LoaiThanhViens_MaLoaiTV",
                        column: x => x.MaLoaiTV,
                        principalTable: "LoaiThanhViens",
                        principalColumn: "MaLoaiTV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonDatHangs",
                columns: table => new
                {
                    MaDonDH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKH = table.Column<int>(type: "int", nullable: false),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    PhuongThucThanhToan = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PhuongThucGiaoHang = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TinhTrang = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonDatHangs", x => x.MaDonDH);
                    table.ForeignKey(
                        name: "FK_DonDatHangs_KhachHangs_MaKH",
                        column: x => x.MaKH,
                        principalTable: "KhachHangs",
                        principalColumn: "MaKH",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonDHs",
                columns: table => new
                {
                    MaChiTietDonDH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDonDH = table.Column<int>(type: "int", nullable: false),
                    MaDongHo = table.Column<int>(type: "int", nullable: false),
                    TenDongHo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDonDHs", x => x.MaChiTietDonDH);
                    table.ForeignKey(
                        name: "FK_ChiTietDonDHs_DonDatHangs_MaDonDH",
                        column: x => x.MaDonDH,
                        principalTable: "DonDatHangs",
                        principalColumn: "MaDonDH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietDonDHs_DongHos_MaDongHo",
                        column: x => x.MaDongHo,
                        principalTable: "DongHos",
                        principalColumn: "MaDongHo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuans_MaThanhVien",
                table: "BinhLuans",
                column: "MaThanhVien");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonDHs_MaDonDH",
                table: "ChiTietDonDHs",
                column: "MaDonDH");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonDHs_MaDongHo",
                table: "ChiTietDonDHs",
                column: "MaDongHo");

            migrationBuilder.CreateIndex(
                name: "IX_DonDatHangs_MaKH",
                table: "DonDatHangs",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHangs_MaLoaiTV",
                table: "KhachHangs",
                column: "MaLoaiTV");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhViens_MaLoaiTV",
                table: "ThanhViens",
                column: "MaLoaiTV");

            migrationBuilder.AddForeignKey(
                name: "FK_BinhLuans_ThanhViens_MaThanhVien",
                table: "BinhLuans",
                column: "MaThanhVien",
                principalTable: "ThanhViens",
                principalColumn: "MaThanhVien",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietPhieuNhaps_DongHos_MaDongHo",
                table: "ChiTietPhieuNhaps",
                column: "MaDongHo",
                principalTable: "DongHos",
                principalColumn: "MaDongHo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietPhieuNhaps_PhieuNhaps_MaPhieuNhap",
                table: "ChiTietPhieuNhaps",
                column: "MaPhieuNhap",
                principalTable: "PhieuNhaps",
                principalColumn: "MaPhieuNhap",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BinhLuans_ThanhViens_MaThanhVien",
                table: "BinhLuans");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietPhieuNhaps_DongHos_MaDongHo",
                table: "ChiTietPhieuNhaps");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietPhieuNhaps_PhieuNhaps_MaPhieuNhap",
                table: "ChiTietPhieuNhaps");

            migrationBuilder.DropTable(
                name: "ChiTietDonDHs");

            migrationBuilder.DropTable(
                name: "ThanhViens");

            migrationBuilder.DropTable(
                name: "DonDatHangs");

            migrationBuilder.DropTable(
                name: "KhachHangs");

            migrationBuilder.DropTable(
                name: "LoaiThanhViens");

            migrationBuilder.DropIndex(
                name: "IX_BinhLuans_MaThanhVien",
                table: "BinhLuans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietPhieuNhaps",
                table: "ChiTietPhieuNhaps");

            migrationBuilder.DropColumn(
                name: "TrangThaiNhap",
                table: "PhieuNhaps");

            migrationBuilder.RenameTable(
                name: "ChiTietPhieuNhaps",
                newName: "ChiTietPhieuNhapModels");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietPhieuNhaps_MaPhieuNhap",
                table: "ChiTietPhieuNhapModels",
                newName: "IX_ChiTietPhieuNhapModels_MaPhieuNhap");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietPhieuNhaps_MaDongHo",
                table: "ChiTietPhieuNhapModels",
                newName: "IX_ChiTietPhieuNhapModels_MaDongHo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayNhap",
                table: "PhieuNhaps",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayCapNhat",
                table: "DongHos",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayBinhLuan",
                table: "BinhLuans",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietPhieuNhapModels",
                table: "ChiTietPhieuNhapModels",
                column: "MaChiTietPhieuNhap");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietPhieuNhapModels_DongHos_MaDongHo",
                table: "ChiTietPhieuNhapModels",
                column: "MaDongHo",
                principalTable: "DongHos",
                principalColumn: "MaDongHo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietPhieuNhapModels_PhieuNhaps_MaPhieuNhap",
                table: "ChiTietPhieuNhapModels",
                column: "MaPhieuNhap",
                principalTable: "PhieuNhaps",
                principalColumn: "MaPhieuNhap",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
