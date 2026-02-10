using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Services.PhieuNhap
{
    public interface IPhieuNhapService
    {
        Task<List<PhieuNhapModel>> GetPhieuNhaps();
        Task<PhieuNhapModel> GetPhieuNhapById(int maPN);
        Task<PhieuNhapModel> CreatePhieuNhap(PhieuNhapModel phieuNhapModel);
        Task DeletePhieuNhap(PhieuNhapModel phieuNhapModel);
        Task<PhieuNhapModel> UpdatePhieuNhap(PhieuNhapModel phieuNhapModel);
    }
}
