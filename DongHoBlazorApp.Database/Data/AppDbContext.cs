using DongHoBlazorApp.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace DongHoBlazorApp.Database.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<PhanLoaiModel> PhanLoais { get; set; } = null!;
        public DbSet<ThuongHieuModel> ThuongHieus { get; set; } = null!;
        public DbSet<NhaCungCapModel> NhaCungCaps { get; set; } = null!;
        public DbSet<DongHoModel> DongHos { get; set; } = null!;
        public DbSet<PhieuNhapModel> PhieuNhaps { get; set; } = null!;
        public DbSet<ChiTietPhieuNhapModel> ChiTietPhieuNhaps { get; set; } = null!;
        public DbSet<BinhLuanModel> BinhLuans { get; set; } = null!;
        public DbSet<DonDatHangModel> DonDatHangs { get; set; } = null!;
        public DbSet<ChiTietDonDHModel> ChiTietDonDHs { get; set; } = null!;
        public DbSet<KhachHangModel> KhachHangs { get; set; } = null!;
        public DbSet<LoaiThanhVienModel> LoaiThanhViens { get; set; } = null!;
        public DbSet<ThanhVienModel> ThanhViens { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PhanLoaiModel>(eb =>
            {
                eb.HasKey(x => x.MaPL);
                eb.Property(x => x.MaPL).ValueGeneratedOnAdd(); // identity
                eb.Property(x => x.TenPL).HasMaxLength(500);
            });

            modelBuilder.Entity<ThuongHieuModel>(eb =>
            {
                eb.HasKey(x => x.MaThuongHieu);
                eb.Property(x => x.MaThuongHieu).ValueGeneratedOnAdd(); // identity
                eb.Property(x => x.TenThuongHieu).HasMaxLength(1000);
                eb.Property(x => x.Logo).HasMaxLength(1000);
                eb.Property(x => x.MoTa).HasColumnType("nvarchar(max)");
            });

            modelBuilder.Entity<NhaCungCapModel>(eb =>
            {
                eb.HasKey(x => x.MaNCC);
                eb.Property(x => x.MaNCC).ValueGeneratedOnAdd(); // identity
                eb.Property(x => x.TenNCC).HasMaxLength(1000);
                eb.Property(x => x.DiaChi).HasMaxLength(2000);
                eb.Property(x => x.Email).HasMaxLength(500);
                eb.Property(x => x.SoDienThoai).HasMaxLength(10);
            });

            // --------------- Phần xử lý cho đồng hồ -----------------

            modelBuilder.Entity<DongHoModel>(eb =>
            {
                eb.HasKey(x => x.MaDongHo);
                eb.Property(x => x.MaDongHo).ValueGeneratedOnAdd();
                eb.Property(x => x.TenDongHo).HasMaxLength(1000);
                eb.Property(x => x.HinhAnh).HasMaxLength(500);
                eb.Property(x => x.MoTa).HasColumnType("nvarchar(max)");
                eb.Property(x => x.GiaBan).HasColumnType("decimal(18,2)");
                eb.Property(x => x.NgayCapNhat).HasDefaultValueSql("GETDATE()");

                eb.HasOne<PhanLoaiModel>()
                  .WithMany()
                  .HasForeignKey(x => x.MaPL)
                  .OnDelete(DeleteBehavior.Cascade);

                eb.HasOne<ThuongHieuModel>()
                  .WithMany()
                  .HasForeignKey(x => x.MaThuongHieu)
                  .OnDelete(DeleteBehavior.Cascade);

                eb.HasOne<NhaCungCapModel>()
                  .WithMany()
                  .HasForeignKey(x => x.MaNCC)
                  .OnDelete(DeleteBehavior.Cascade);


            });

            // --------------- Kết thúc phần xử lý cho đồng hồ -----------------

            modelBuilder.Entity<PhieuNhapModel>(eb =>
            {
                eb.HasKey(x => x.MaPhieuNhap);
                eb.Property(x => x.TongTien).HasColumnType("decimal(18,2)");
                eb.Property(x => x.TrangThaiNhap).HasMaxLength(250);
                eb.Property(x => x.NgayNhap).HasDefaultValueSql("GETDATE()");
                eb.HasOne<NhaCungCapModel>()
                  .WithMany()
                  .HasForeignKey(x => x.MaNCC)
                  .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ChiTietPhieuNhapModel>(eb =>
            {
                eb.HasKey(x => x.MaChiTietPhieuNhap);
                eb.Property(x => x.DonGiaNhap).HasColumnType("decimal(18,2)");

                eb.HasOne<PhieuNhapModel>()
                  .WithMany()
                  .HasForeignKey(x => x.MaPhieuNhap)
                  .OnDelete(DeleteBehavior.Cascade);

                eb.HasOne<DongHoModel>()
                  .WithMany()
                  .HasForeignKey(x => x.MaDongHo)
                  .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<BinhLuanModel>(eb =>
            {
                eb.HasKey(x => x.MaBL);
                eb.Property(x => x.MaBL).ValueGeneratedOnAdd();
                eb.Property(x => x.NoiDungBL).HasColumnType("nvarchar(max)");
                eb.Property(x => x.TenThanhVien).HasMaxLength(500);
                eb.Property(x => x.NgayBinhLuan).HasDefaultValueSql("GETDATE()");

                eb.HasOne<DongHoModel>()
                  .WithMany()
                  .HasForeignKey(x => x.MaDongHo)
                  .OnDelete(DeleteBehavior.Cascade);

                eb.HasOne<ThanhVienModel>()
                  .WithMany()
                  .HasForeignKey(x => x.MaThanhVien)
                  .OnDelete(DeleteBehavior.Cascade);
            });

            // --------------- Phần xử lý cho thành viên -----------------

            modelBuilder.Entity<ThanhVienModel>(eb =>
            {
                eb.HasKey(x => x.MaThanhVien);
                eb.Property(x => x.MaThanhVien).ValueGeneratedOnAdd();
                eb.Property(x => x.TenDangNhap).HasMaxLength(250);
                eb.Property(x => x.MatKhau).HasMaxLength(1000);
                eb.Property(x => x.HoTen).HasMaxLength(500);
                eb.Property(x => x.DiaChi).HasColumnType("nvarchar(max)");
                eb.Property(x => x.SoDienThoai).HasMaxLength(10);
                eb.Property(x => x.Email).HasMaxLength(250);
                eb.Property(x => x.NgayTao).HasDefaultValueSql("GETDATE()");
                eb.Property(x => x.HinhAnh).HasMaxLength(500);

                eb.HasOne<LoaiThanhVienModel>()
                  .WithMany()
                  .HasForeignKey(x => x.MaLoaiTV)
                  .OnDelete(DeleteBehavior.Cascade);
            });
            // --------------- Kết thúc phần xử lý cho thành viên -----------------


            // --------------- Phần xử lý cho loại thành viên -----------------
            modelBuilder.Entity<LoaiThanhVienModel>(eb =>
            {
                eb.HasKey(x => x.MaLoaiTV);
                eb.Property(x => x.MaLoaiTV).ValueGeneratedOnAdd();
                eb.Property(x => x.TenLoaiTV).HasMaxLength(500);
            });
            // --------------- Kết thúc phần xử lý cho loại thành viên -----------------


            // --------------- Phần xử lý cho khách hàng -----------------
            modelBuilder.Entity<KhachHangModel>(eb =>
            {
                eb.HasKey(x => x.MaKH);
                eb.Property(x => x.MaKH).ValueGeneratedOnAdd();
                eb.Property(x => x.TenKH).HasMaxLength(500);
                eb.Property(x => x.DiaChi).HasColumnType("nvarchar(max)");
                eb.Property(x => x.SoDienThoai).HasMaxLength(10);
                eb.Property(x => x.Email).HasMaxLength(250);

                eb.HasOne<LoaiThanhVienModel>()
                  .WithMany()
                  .HasForeignKey(x => x.MaLoaiTV)
                  .OnDelete(DeleteBehavior.Cascade);
            });
            // --------------- Kết thúc phần xử lý cho khách hàng -----------------


            // -------------- Phần xử lý cho đơn đặt hàng -----------------
            modelBuilder.Entity<DonDatHangModel>(eb =>
            {
                eb.HasKey(x => x.MaDonDH);
                eb.Property(x => x.MaDonDH).ValueGeneratedOnAdd();
                eb.Property(x => x.NgayDat).HasDefaultValueSql("GETDATE()");
                eb.Property(x => x.NgayGiao).HasColumnType("datetime");
                eb.Property(x => x.DiaChiGiaoHang).HasMaxLength(2000);
                eb.Property(x => x.PhuongThucThanhToan).HasMaxLength(500);
                eb.Property(x => x.PhuongThucGiaoHang).HasMaxLength(500);
                eb.HasOne<KhachHangModel>()
                  .WithMany()
                  .HasForeignKey(x => x.MaKH)
                  .OnDelete(DeleteBehavior.Cascade);
            });
            // --------------- Kết thúc phần xử lý cho đơn đặt hàng -----------------


            // --------------- Phần xử lý cho chi tiết đơn đặt hàng -----------------
            modelBuilder.Entity<ChiTietDonDHModel>(eb =>
            {
                eb.HasKey(x => x.MaChiTietDonDH);
                eb.Property(x => x.MaChiTietDonDH).ValueGeneratedOnAdd();
                eb.Property(x => x.DonGia).HasColumnType("decimal(18,2)");
                eb.Property(x => x.TenDongHo).HasMaxLength(500);
                eb.HasOne<DonDatHangModel>()
                  .WithMany()
                  .HasForeignKey(x => x.MaDonDH)
                  .OnDelete(DeleteBehavior.Cascade);
                eb.HasOne<DongHoModel>()
                  .WithMany()
                  .HasForeignKey(x => x.MaDongHo)
                  .OnDelete(DeleteBehavior.Restrict);
            });
            // --------------- Kết thúc phần xử lý cho chi tiết đơn đặt hàng -----------------

        }
    }
}
