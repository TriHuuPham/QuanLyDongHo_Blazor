using DongHoBlazorApp.BL.Reposities.ThuongHieu;
using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Services.ThuongHieu
{
    public class ThuongHieuService(IThuongHieuRepository thuongHieuRepository) : IThuongHieuService
    {
        public Task<List<ThuongHieuModel>> GetThuongHieus()
        {
            try
            {
                return thuongHieuRepository.GetThuongHieus();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public Task<ThuongHieuModel> GetThuongHieuById(int maTH)
        {
            try
            {
                return thuongHieuRepository.GetThuongHieuById(maTH);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public Task<ThuongHieuModel> CreateThuongHieu(ThuongHieuModel thuongHieuModel)
        {
            try
            {
                return thuongHieuRepository.CreateThuongHieu(thuongHieuModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task DeleteThuongHieu(ThuongHieuModel thuongHieuModel)
        {
            try
            {
                return thuongHieuRepository.DeleteThuongHieu(thuongHieuModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<ThuongHieuModel> UpdateThuongHieu(ThuongHieuModel thuongHieuModel)
        {
            try
            {
                return thuongHieuRepository.UpdateThuongHieu(thuongHieuModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
