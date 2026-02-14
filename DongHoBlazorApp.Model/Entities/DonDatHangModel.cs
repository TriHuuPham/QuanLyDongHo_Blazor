namespace DongHoBlazorApp.Model.Entities
{
    public class DonDatHangModel
    {
        public int MaDonDH { get; set; }
        public int MaKH { get; set; }
        public DateTime NgayDat { get; set; }
        public string PhuongThucThanhToan { get; set; }
        public string PhuongThucGiaoHang { get; set; }
        public string TinhTrang { get; set; }
        public bool TrangThai { get; set; }
    }
}
