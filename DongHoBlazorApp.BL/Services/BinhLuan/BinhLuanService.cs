using DongHoBlazorApp.BL.Reposities.BinhLuan;
using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Services.BinhLuan
{
    public class BinhLuanService(IBinhLuanRepository binhLuanRepository) : IBinhLuanService
    {
        public Task<List<BinhLuanModel>> GetBinhLuans()
        {
            try
            {
                return binhLuanRepository.GetBinhLuans();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<BinhLuanModel> GetBinhLuanById(int maBL)
        {
            try
            {
                return binhLuanRepository.GetBinhLuanById(maBL);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public Task<BinhLuanModel> CreateBinhLuan(BinhLuanModel binhLuanModel)
        {
            try
            {
                return binhLuanRepository.CreateBinhLuan(binhLuanModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task DeleteBinhLuan(BinhLuanModel binhLuanModel)
        {
            try
            {
                return binhLuanRepository.DeleteBinhLuan(binhLuanModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<BinhLuanModel> UpdateBinhLuan(BinhLuanModel binhLuanModel)
        {
            try
            {
                return binhLuanRepository.UpdateBinhLuan(binhLuanModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
