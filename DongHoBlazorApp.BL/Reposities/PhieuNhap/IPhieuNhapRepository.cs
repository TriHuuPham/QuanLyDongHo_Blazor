using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Reposities.PhieuNhap
{
    public interface IPhieuNhapRepository
    {
        Task<List<PhieuNhapModel>> GetPhieuNhaps();
        Task<PhieuNhapModel> GetPhieuNhapById(int maPN);
        Task<PhieuNhapModel> CreatePhieuNhap(PhieuNhapModel phieuNhapModel);
        Task DeletePhieuNhap(PhieuNhapModel phieuNhapModel);
        Task<PhieuNhapModel> UpdatePhieuNhap(PhieuNhapModel phieuNhapModel);
    }
}
