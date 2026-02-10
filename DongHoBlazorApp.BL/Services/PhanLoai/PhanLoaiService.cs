using DongHoBlazorApp.BL.Reposities.PhanLoai;
using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Services.PhanLoai
{
    public class PhanLoaiService(IPhanLoaiRepository phanLoaiRepository) : IPhanLoaiService
    {
        public Task<List<PhanLoaiModel>> GetPhanLoais()
        {
            try
            {
                return phanLoaiRepository.GetPhanLoais();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public Task<PhanLoaiModel> GetPhanLoaiById(int maPL)
        {
            try
            {
                return phanLoaiRepository.GetPhanLoaiById(maPL);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public Task<PhanLoaiModel> CreatePhanLoai(PhanLoaiModel phanLoaiModel)
        {
            try
            {
                return phanLoaiRepository.CreatePhanLoai(phanLoaiModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task DeletePhanLoai(PhanLoaiModel phanLoaiModel)
        {
            try
            {
                return phanLoaiRepository.DeletePhanLoai(phanLoaiModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<PhanLoaiModel> UpdatePhanLoai(PhanLoaiModel phanLoaiModel)
        {
            try
            {
                return phanLoaiRepository.UpdatePhanLoai(phanLoaiModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
