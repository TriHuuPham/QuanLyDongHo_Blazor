using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Reposities.NhaCungCap
{
    public interface INhaCungCapRepository
    {
        Task<List<NhaCungCapModel>> GetNhaCungCaps();
        Task<NhaCungCapModel> GetNhaCungCapById(int maNCC);
        Task<NhaCungCapModel> CreateNhaCungCap(NhaCungCapModel nhaCungCapModel);
        Task DeleteNhaCungCap(NhaCungCapModel nhaCungCapModel);
        Task<NhaCungCapModel> UpdateNhaCungCap(NhaCungCapModel nhaCungCapModel);
    }
}
