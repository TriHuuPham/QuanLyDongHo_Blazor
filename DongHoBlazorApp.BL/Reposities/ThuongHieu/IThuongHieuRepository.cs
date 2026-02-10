using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Reposities.ThuongHieu
{
    public interface IThuongHieuRepository
    {
        Task<List<ThuongHieuModel>> GetThuongHieus();
        Task<ThuongHieuModel> GetThuongHieuById(int id);
        Task<ThuongHieuModel> CreateThuongHieu(ThuongHieuModel thuongHieuModel);
        Task DeleteThuongHieu(ThuongHieuModel thuongHieuModel);
        Task<ThuongHieuModel> UpdateThuongHieu(ThuongHieuModel thuongHieuModel);
    }
}
