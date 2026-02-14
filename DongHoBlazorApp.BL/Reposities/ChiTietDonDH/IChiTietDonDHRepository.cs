using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Reposities.ChiTietDonDH
{
    public interface IChiTietDonDHRepository
    {
        Task<List<ChiTietDonDHModel>> GetChiTietDonDHs();
        Task<ChiTietDonDHModel> GetChiTietDonDHById(int id);
        Task<ChiTietDonDHModel> CreateChiTietDonDH(ChiTietDonDHModel chiTietDonDH);
        Task<ChiTietDonDHModel> UpdateChiTietDonDH(ChiTietDonDHModel chiTietDonDH);
        Task<bool> DeleteChiTietDonDH(int id);
    }
}
