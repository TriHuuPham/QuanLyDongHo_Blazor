using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Reposities.LoaiThanhVien
{
    public interface ILoaiThanhVienRepository
    {
        Task<List<LoaiThanhVienModel>> GetLoaiThanhViens();
        Task<LoaiThanhVienModel> GetLoaiThanhVienById(int id);
        Task<LoaiThanhVienModel> CreateLoaiThanhVien(LoaiThanhVienModel loaiThanhVienModel);
        Task DeleteLoaiThanhVien(LoaiThanhVienModel loaiThanhVienModel);
        Task<LoaiThanhVienModel> UpdateLoaiThanhVien(LoaiThanhVienModel loaiThanhVienModel);
    }
}
