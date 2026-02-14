using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Reposities.KhachHang
{
    public interface IKhachHangRepository
    {
        Task<List<KhachHangModel>> GetKhachHangs();
        Task<List<KhachHangModel>> SearchKhachHangs(string searchTerm);
        Task<KhachHangModel> GetKhachHangById(int id);
        Task<KhachHangModel> CreateKhachHang(KhachHangModel khachHangModel);
        Task DeleteKhachHang(KhachHangModel khachHangModel);
        Task<KhachHangModel> UpdateKhachHang(KhachHangModel khachHangModel);
    }
}
