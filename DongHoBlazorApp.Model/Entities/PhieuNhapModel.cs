namespace DongHoBlazorApp.Model.Entities
{
    public class PhieuNhapModel
    {
        public int MaPhieuNhap { get; set; }
        public int MaNCC { get; set; }
        public DateTime NgayNhap { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThaiNhap { get; set; }
        public bool TrangThai { get; set; }
    }
}
