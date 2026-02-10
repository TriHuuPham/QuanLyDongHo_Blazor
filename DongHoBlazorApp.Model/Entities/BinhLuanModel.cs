namespace DongHoBlazorApp.Model.Entities
{
    public class BinhLuanModel
    {
        public int MaBL { get; set; }
        public string NoiDungBL { get; set; }
        public int MaThanhVien { get; set; }
        public int MaDongHo { get; set; }
        public string TenThanhVien { get; set; }
        public DateTime NgayBinhLuan { get; set; }
        public bool TrangThai { get; set; }
    }
}
