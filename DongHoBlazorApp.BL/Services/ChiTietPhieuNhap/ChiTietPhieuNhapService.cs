using DongHoBlazorApp.BL.Reposities.ChiTietPhieuNhap;
using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Services.ChiTietPhieuNhap
{
    public class ChiTietPhieuNhapService(IChiTietPhieuNhapRepository chiTietPhieuNhapRepository) : IChiTietPhieuNhapService
    {
        public Task<List<ChiTietPhieuNhapModel>> GetChiTietPhieuNhaps()
        {
            try
            {
                return chiTietPhieuNhapRepository.GetChiTietPhieuNhaps();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<ChiTietPhieuNhapModel> GetChiTietPhieuNhapById(int maCTPN)
        {
            try
            {
                return chiTietPhieuNhapRepository.GetChiTietPhieuNhapById(maCTPN);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<ChiTietPhieuNhapModel> CreateChiTietPhieuNhap(ChiTietPhieuNhapModel chiTietPhieuNhapModel)
        {
            try
            {
                return chiTietPhieuNhapRepository.CreateChiTietPhieuNhap(chiTietPhieuNhapModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task DeleteChiTietPhieuNhap(ChiTietPhieuNhapModel chiTietPhieuNhapModel)
        {
            try
            {
                return chiTietPhieuNhapRepository.DeleteChiTietPhieuNhap(chiTietPhieuNhapModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<ChiTietPhieuNhapModel> UpdateChiTietPhieuNhap(ChiTietPhieuNhapModel chiTietPhieuNhapModel)
        {
            try
            {
                return chiTietPhieuNhapRepository.UpdateChiTietPhieuNhap(chiTietPhieuNhapModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
