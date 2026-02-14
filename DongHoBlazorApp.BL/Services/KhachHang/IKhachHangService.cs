using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Services.KhachHang
{
    public interface IKhachHangService
    {
        Task<List<KhachHangModel>> GetKhachHangs();
        Task<List<KhachHangModel>> SearchKhachHangs(string searchTerm);
        Task<KhachHangModel> GetKhachHangById(int maKH);
        Task<KhachHangModel> CreateKhachHang(KhachHangModel khachHangModel);
        Task DeleteKhachHang(KhachHangModel khachHangModel);
        Task<KhachHangModel> UpdateKhachHang(KhachHangModel khachHangModel);
    }
}
