using DongHoBlazorApp.BL.Reposities.ChiTietDonDH;
using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Services.ChiTietDonDH
{
    public class ChiTietDonDHService(IChiTietDonDHRepository chiTietDonDHRepository) : IChiTietDonDHService
    {
        public Task<List<ChiTietDonDHModel>> GetChiTietDonDHs()
        {
            try
            {
                return chiTietDonDHRepository.GetChiTietDonDHs();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<ChiTietDonDHModel> GetChiTietDonDHById(int id)
        {
            try
            {
                return chiTietDonDHRepository.GetChiTietDonDHById(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<ChiTietDonDHModel> CreateChiTietDonDH(ChiTietDonDHModel chiTietDonDH)
        {
            try
            {
                return chiTietDonDHRepository.CreateChiTietDonDH(chiTietDonDH);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<ChiTietDonDHModel> UpdateChiTietDonDH(ChiTietDonDHModel chiTietDonDH)
        {
            try
            {
                return chiTietDonDHRepository.UpdateChiTietDonDH(chiTietDonDH);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<bool> DeleteChiTietDonDH(int id)
        {
            try
            {
                return chiTietDonDHRepository.DeleteChiTietDonDH(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
