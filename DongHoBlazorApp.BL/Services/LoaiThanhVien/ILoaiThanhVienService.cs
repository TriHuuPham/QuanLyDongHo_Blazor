using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Services.LoaiThanhVien
{
    public interface ILoaiThanhVienService
    {
        Task<List<LoaiThanhVienModel>> GetLoaiThanhViens();
        Task<LoaiThanhVienModel> GetLoaiThanhVienById(int maLoaiTV);
        Task<LoaiThanhVienModel> CreateLoaiThanhVien(LoaiThanhVienModel loaiThanhVienModel);
        Task DeleteLoaiThanhVien(LoaiThanhVienModel loaiThanhVienModel);
        Task<LoaiThanhVienModel> UpdateLoaiThanhVien(LoaiThanhVienModel loaiThanhVienModel);
    }
}
