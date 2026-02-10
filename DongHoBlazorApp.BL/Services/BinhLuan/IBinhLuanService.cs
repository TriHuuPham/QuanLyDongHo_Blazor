using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Services.BinhLuan
{
    public interface IBinhLuanService
    {
        Task<List<BinhLuanModel>> GetBinhLuans();
        Task<BinhLuanModel> GetBinhLuanById(int maBL);
        Task<BinhLuanModel> CreateBinhLuan(BinhLuanModel binhLuanModel);
        Task DeleteBinhLuan(BinhLuanModel binhLuanModel);
        Task<BinhLuanModel> UpdateBinhLuan(BinhLuanModel binhLuanModel);
    }
}
