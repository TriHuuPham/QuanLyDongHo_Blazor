using DongHoBlazorApp.BL.Reposities.LoaiThanhVien;
using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Services.LoaiThanhVien
{
    public class LoaiThanhVienService(ILoaiThanhVienRepository loaiThanhVienRepository) : ILoaiThanhVienService
    {
        public Task<List<LoaiThanhVienModel>> GetLoaiThanhViens()
        {
            try
            {
                return loaiThanhVienRepository.GetLoaiThanhViens();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<LoaiThanhVienModel> GetLoaiThanhVienById(int maLoaiTV)
        {
            try
            {
                return loaiThanhVienRepository.GetLoaiThanhVienById(maLoaiTV);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<LoaiThanhVienModel> CreateLoaiThanhVien(LoaiThanhVienModel loaiThanhVienModel)
        {
            try
            {
                return loaiThanhVienRepository.CreateLoaiThanhVien(loaiThanhVienModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task DeleteLoaiThanhVien(LoaiThanhVienModel loaiThanhVienModel)
        {
            try
            {
                return loaiThanhVienRepository.DeleteLoaiThanhVien(loaiThanhVienModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<LoaiThanhVienModel> UpdateLoaiThanhVien(LoaiThanhVienModel loaiThanhVienModel)
        {
            try
            {
                return loaiThanhVienRepository.UpdateLoaiThanhVien(loaiThanhVienModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
