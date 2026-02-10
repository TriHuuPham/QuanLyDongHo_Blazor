using DongHoBlazorApp.BL.Reposities.PhieuNhap;
using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Services.PhieuNhap
{
    public class PhieuNhapService(IPhieuNhapRepository phieuNhapRepository) : IPhieuNhapService
    {
        public Task<List<PhieuNhapModel>> GetPhieuNhaps()
        {
            try
            {
                return phieuNhapRepository.GetPhieuNhaps();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<PhieuNhapModel> GetPhieuNhapById(int maPN)
        {
            try
            {
                return phieuNhapRepository.GetPhieuNhapById(maPN);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<PhieuNhapModel> CreatePhieuNhap(PhieuNhapModel phieuNhapModel)
        {
            try
            {
                return phieuNhapRepository.CreatePhieuNhap(phieuNhapModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task DeletePhieuNhap(PhieuNhapModel phieuNhapModel)
        {
            try
            {
                return phieuNhapRepository.DeletePhieuNhap(phieuNhapModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<PhieuNhapModel> UpdatePhieuNhap(PhieuNhapModel phieuNhapModel)
        {
            try
            {
                return phieuNhapRepository.UpdatePhieuNhap(phieuNhapModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
