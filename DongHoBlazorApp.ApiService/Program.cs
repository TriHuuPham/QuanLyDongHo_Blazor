using DongHoBlazorApp.BL.Reposities.BinhLuan;
using DongHoBlazorApp.BL.Reposities.ChiTietPhieuNhap;
using DongHoBlazorApp.BL.Reposities.DongHo;
using DongHoBlazorApp.BL.Reposities.KhachHang;
using DongHoBlazorApp.BL.Reposities.LoaiThanhVien;
using DongHoBlazorApp.BL.Reposities.NhaCungCap;
using DongHoBlazorApp.BL.Reposities.PhanLoai;
using DongHoBlazorApp.BL.Reposities.PhieuNhap;
using DongHoBlazorApp.BL.Reposities.ThuongHieu;
using DongHoBlazorApp.BL.Services.BinhLuan;
using DongHoBlazorApp.BL.Services.ChiTietPhieuNhap;
using DongHoBlazorApp.BL.Services.DongHo;
using DongHoBlazorApp.BL.Services.KhachHang;
using DongHoBlazorApp.BL.Services.LoaiThanhVien;
using DongHoBlazorApp.BL.Services.NhaCungCap;
using DongHoBlazorApp.BL.Services.PhanLoai;
using DongHoBlazorApp.BL.Services.PhieuNhap;
using DongHoBlazorApp.BL.Services.ThuongHieu;
using DongHoBlazorApp.Database.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// --------------- Phần đăng ký dịch vụ ở đây ----------------
builder.Services.AddScoped<IPhanLoaiService, PhanLoaiService>();
builder.Services.AddScoped<IPhanLoaiRepository, PhanLoaiRepository>();
builder.Services.AddScoped<IThuongHieuService, ThuongHieuService>();
builder.Services.AddScoped<IThuongHieuRepository, ThuongHieuRepository>();
builder.Services.AddScoped<INhaCungCapService, NhaCungCapService>();
builder.Services.AddScoped<INhaCungCapRepository, NhaCungCapRepository>();
builder.Services.AddScoped<IDongHoService, DongHoService>();
builder.Services.AddScoped<IDongHoRepository, DongHoRepository>();
builder.Services.AddScoped<IBinhLuanService, BinhLuanService>();
builder.Services.AddScoped<IBinhLuanRepository, BinhLuanRepository>();
builder.Services.AddScoped<IPhieuNhapService, PhieuNhapService>();
builder.Services.AddScoped<IPhieuNhapRepository, PhieuNhapRepository>();
builder.Services.AddScoped<IChiTietPhieuNhapService, ChiTietPhieuNhapService>();
builder.Services.AddScoped<IChiTietPhieuNhapRepository, ChiTietPhieuNhapRepository>();
builder.Services.AddScoped<ILoaiThanhVienService, LoaiThanhVienService>();
builder.Services.AddScoped<ILoaiThanhVienRepository, LoaiThanhVienRepository>();
builder.Services.AddScoped<IKhachHangService, KhachHangService>();
builder.Services.AddScoped<IKhachHangRepository, KhachHangRepository>();
// --------------- Kết thúc phần đăng ký dịch vụ ở đây ----------------



// Add services to the container.
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapDefaultEndpoints();

app.Run();
