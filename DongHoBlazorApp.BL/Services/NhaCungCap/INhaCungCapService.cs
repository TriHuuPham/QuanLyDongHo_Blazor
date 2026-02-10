using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Services.NhaCungCap
{
    public interface INhaCungCapService
    {
        Task<List<NhaCungCapModel>> GetNhaCungCaps();
        Task<NhaCungCapModel> GetNhaCungCapById(int maNCC);
        Task<NhaCungCapModel> CreateNhaCungCap(NhaCungCapModel nhaCungCapModel);
        Task DeleteNhaCungCap(NhaCungCapModel nhaCungCapModel);
        Task<NhaCungCapModel> UpdateNhaCungCap(NhaCungCapModel nhaCungCapModel);
    }
}
