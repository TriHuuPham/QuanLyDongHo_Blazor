using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Reposities.ChiTietPhieuNhap
{
    public interface IChiTietPhieuNhapRepository
    {
        Task<List<ChiTietPhieuNhapModel>> GetChiTietPhieuNhaps();
        Task<ChiTietPhieuNhapModel> GetChiTietPhieuNhapById(int maCTPN);
        Task<ChiTietPhieuNhapModel> CreateChiTietPhieuNhap(ChiTietPhieuNhapModel chiTietPhieuNhapModel);
        Task DeleteChiTietPhieuNhap(ChiTietPhieuNhapModel chiTietPhieuNhapModel);
        Task<ChiTietPhieuNhapModel> UpdateChiTietPhieuNhap(ChiTietPhieuNhapModel chiTietPhieuNhapModel);
    }
}
