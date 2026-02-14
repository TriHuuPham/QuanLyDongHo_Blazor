namespace DongHoBlazorApp.Model.Models
{
    public class DonDatHangStatsDTO
    {
        public int TotalOrders { get; set; }
        public int ProcessingOrders { get; set; }
        public int CompletedOrders { get; set; }
        public int CancelledOrders { get; set; }
    }
}
