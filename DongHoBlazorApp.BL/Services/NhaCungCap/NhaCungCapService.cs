using DongHoBlazorApp.BL.Reposities.NhaCungCap;
using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Services.NhaCungCap
{
    public class NhaCungCapService(INhaCungCapRepository nhaCungCapRepository) : INhaCungCapService
    {
        public Task<List<NhaCungCapModel>> GetNhaCungCaps()
        {
            try
            {
                return nhaCungCapRepository.GetNhaCungCaps();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public Task<NhaCungCapModel> GetNhaCungCapById(int maNCC)
        {
            try
            {
                return nhaCungCapRepository.GetNhaCungCapById(maNCC);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public Task<NhaCungCapModel> CreateNhaCungCap(NhaCungCapModel nhaCungCapModel)
        {
            try
            {
                return nhaCungCapRepository.CreateNhaCungCap(nhaCungCapModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task DeleteNhaCungCap(NhaCungCapModel nhaCungCapModel)
        {
            try
            {
                return nhaCungCapRepository.DeleteNhaCungCap(nhaCungCapModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<NhaCungCapModel> UpdateNhaCungCap(NhaCungCapModel nhaCungCapModel)
        {
            try
            {
                return nhaCungCapRepository.UpdateNhaCungCap(nhaCungCapModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
