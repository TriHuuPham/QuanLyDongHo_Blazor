using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Services.ChiTietPhieuNhap
{
    public interface IChiTietPhieuNhapService
    {
        Task<List<ChiTietPhieuNhapModel>> GetChiTietPhieuNhaps();
        Task<ChiTietPhieuNhapModel> GetChiTietPhieuNhapById(int maCTPN);
        Task<ChiTietPhieuNhapModel> CreateChiTietPhieuNhap(ChiTietPhieuNhapModel chiTietPhieuNhapModel);
        Task DeleteChiTietPhieuNhap(ChiTietPhieuNhapModel chiTietPhieuNhapModel);
        Task<ChiTietPhieuNhapModel> UpdateChiTietPhieuNhap(ChiTietPhieuNhapModel chiTietPhieuNhapModel);
    }
}
