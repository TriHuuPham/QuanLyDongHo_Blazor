namespace DongHoBlazorApp.Model.Entities
{
    public class DongHoModel
    {
        public int MaDongHo { get; set; }
        public string TenDongHo { get; set; }
        public int MaPL { get; set; }
        public decimal GiaBan { get; set; }
        public int MaNCC { get; set; }
        public int MaThuongHieu { get; set; }
        public int SoLuongTon { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayCapNhat { get; set; }
        public string HinhAnh { get; set; }
        public int LuotXem { get; set; }
        public int LuotBinhLuan { get; set; }
        public int DaBan { get; set; }
        public bool TrangThai { get; set; }
    }
}
