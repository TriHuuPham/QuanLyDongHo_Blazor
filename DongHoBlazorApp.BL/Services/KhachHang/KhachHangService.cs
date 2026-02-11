using DongHoBlazorApp.BL.Reposities.KhachHang;
using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Services.KhachHang
{
    public class KhachHangService (IKhachHangRepository khachHangRepository) : IKhachHangService
    {
        public Task<List<KhachHangModel>> GetKhachHangs()
        {
            try
            {
                return khachHangRepository.GetKhachHangs();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<KhachHangModel> GetKhachHangById(int maKH)
        {
            try
            {
                return khachHangRepository.GetKhachHangById(maKH);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<KhachHangModel> CreateKhachHang(KhachHangModel khachHangModel)
        {
            try
            {
                return khachHangRepository.CreateKhachHang(khachHangModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task DeleteKhachHang(KhachHangModel khachHangModel)
        {
            try
            {
                return khachHangRepository.DeleteKhachHang(khachHangModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<KhachHangModel> UpdateKhachHang(KhachHangModel khachHangModel)
        {
            try
            {
                return khachHangRepository.UpdateKhachHang(khachHangModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
