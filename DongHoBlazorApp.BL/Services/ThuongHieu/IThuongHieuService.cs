using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Services.ThuongHieu
{
    public interface IThuongHieuService
    {
        Task<List<ThuongHieuModel>> GetThuongHieus();
        Task<ThuongHieuModel> GetThuongHieuById(int maTH);
        Task<ThuongHieuModel> CreateThuongHieu(ThuongHieuModel thuongHieuModel);
        Task DeleteThuongHieu(ThuongHieuModel thuongHieuModel);
        Task<ThuongHieuModel> UpdateThuongHieu(ThuongHieuModel thuongHieuModel);
    }
}
