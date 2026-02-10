using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Reposities.DongHo
{
    public interface IDongHoRepository
    {
        Task<List<DongHoModel>> GetDongHos();
        Task<DongHoModel> GetDongHoById(int maDH);
        Task<DongHoModel> CreateDongHo(DongHoModel dongHoModel);
        Task DeleteDongHo(DongHoModel dongHoModel);
        Task<DongHoModel> UpdateDongHo(DongHoModel dongHoModel);
    }
}
