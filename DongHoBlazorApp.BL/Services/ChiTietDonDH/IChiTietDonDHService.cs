using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Services.ChiTietDonDH
{
    public interface IChiTietDonDHService
    {
        Task<List<ChiTietDonDHModel>> GetChiTietDonDHs();
        Task<ChiTietDonDHModel> GetChiTietDonDHById(int id);
        Task<ChiTietDonDHModel> CreateChiTietDonDH(ChiTietDonDHModel chiTietDonDH);
        Task<ChiTietDonDHModel> UpdateChiTietDonDH(ChiTietDonDHModel chiTietDonDH);
        Task<bool> DeleteChiTietDonDH(int id);
    }
}
