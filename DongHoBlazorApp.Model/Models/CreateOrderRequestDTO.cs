using System.Collections.Generic;
using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.Model.Models
{
    public class CreateOrderRequestDTO
    {
        public DonDatHangModel Order { get; set; } = new();
        public List<ChiTietDonDHModel> Details { get; set; } = new();
    }
}
