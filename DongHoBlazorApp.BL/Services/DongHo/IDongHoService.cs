using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Services.DongHo
{
    public interface IDongHoService
    {
        Task<List<DongHoModel>> GetDongHos();
        Task<List<DongHoModel>> SearchDongHos(string searchTerm);
        Task<DongHoModel> GetDongHoById(int maDH);
        Task<DongHoModel> CreateDongHo(DongHoModel dongHoModel);
        Task DeleteDongHo(DongHoModel dongHoModel);
        Task<DongHoModel> UpdateDongHo(DongHoModel dongHoModel);
    }
}
