namespace DongHoBlazorApp.Model.Entities
{
    public class ThanhVienModel
    {
        public int MaThanhVien { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public int MaLoaiTV { get; set; }
        public DateTime NgayTao { get; set; }
        public string HinhAnh { get; set; }
        public bool TrangThai { get; set; }
    }
}
