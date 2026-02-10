using DongHoBlazorApp.BL.Reposities.DongHo;
using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Services.DongHo
{
    public class DongHoService(IDongHoRepository dongHoRepository) : IDongHoService
    {
        public Task<List<DongHoModel>> GetDongHos()
        {
            try
            {
                return dongHoRepository.GetDongHos();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<DongHoModel> GetDongHoById(int maDH)
        {
            try
            {
                return dongHoRepository.GetDongHoById(maDH);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<DongHoModel> CreateDongHo(DongHoModel dongHoModel)
        {
            try
            {
                return dongHoRepository.CreateDongHo(dongHoModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task DeleteDongHo(DongHoModel dongHoModel)
        {
            try
            {
                return dongHoRepository.DeleteDongHo(dongHoModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<DongHoModel> UpdateDongHo(DongHoModel dongHoModel)
        {
            try
            {
                return dongHoRepository.UpdateDongHo(dongHoModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
