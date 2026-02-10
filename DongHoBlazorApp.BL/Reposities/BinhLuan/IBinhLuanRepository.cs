using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Reposities.BinhLuan
{
    public interface IBinhLuanRepository
    {
        Task<List<BinhLuanModel>> GetBinhLuans();
        Task<BinhLuanModel> GetBinhLuanById(int maBL);
        Task<BinhLuanModel> CreateBinhLuan(BinhLuanModel binhLuanModel);
        Task DeleteBinhLuan(BinhLuanModel binhLuanModel);
        Task<BinhLuanModel> UpdateBinhLuan(BinhLuanModel binhLuanModel);
    }
}
