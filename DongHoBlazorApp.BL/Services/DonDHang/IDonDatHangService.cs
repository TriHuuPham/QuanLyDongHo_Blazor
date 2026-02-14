using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;

namespace DongHoBlazorApp.BL.Services.DonDHang
{
    public interface IDonDatHangService
    {
        Task<PaginatedResult<DonDatHangModel>> GetDonDatHangs(string? searchTerm, string? status, int page, int pageSize);
        Task<DonDatHangModel> GetDonDatHangById(int maDonDH);
        Task<DonDatHangDetailDTO> GetDonDatHangDetail(int maDonDH);
        Task<DonDatHangStatsDTO> GetDonDatHangStats();
        Task<int> CreateDonDatHangComplex(CreateOrderRequestDTO request);
        Task<int> UpdateDonDatHang(DonDatHangModel donDatHangModel);
        Task<int> DeleteDonDatHang(int maDonDH);
    }
}
