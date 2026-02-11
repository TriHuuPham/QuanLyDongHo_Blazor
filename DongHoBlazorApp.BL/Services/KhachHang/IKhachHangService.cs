using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Services.KhachHang
{
    public interface IKhachHangService
    {
        Task<List<KhachHangModel>> GetKhachHangs();
        Task<KhachHangModel> GetKhachHangById(int maKH);
        Task<KhachHangModel> CreateKhachHang(KhachHangModel khachHangModel);
        Task DeleteKhachHang(KhachHangModel khachHangModel);
        Task<KhachHangModel> UpdateKhachHang(KhachHangModel khachHangModel);
    }
}
