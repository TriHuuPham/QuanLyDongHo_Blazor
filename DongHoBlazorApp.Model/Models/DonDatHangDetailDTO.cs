using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.Model.Models
{
    public class DonDatHangDetailDTO
    {
        public DonDatHangModel Order { get; set; }
        public KhachHangModel Customer { get; set; }
        public List<ChiTietDonDHModel> Items { get; set; }
    }
}
