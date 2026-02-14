using DongHoBlazorApp.BL.Reposities.DonDHang;
using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;

namespace DongHoBlazorApp.BL.Services.DonDHang
{
    public class DonDatHangService(IDonDatHangRepository donDatHangRepository) : IDonDatHangService
    {
        public Task<PaginatedResult<DonDatHangModel>> GetDonDatHangs(string? searchTerm, string? status, int page, int pageSize)
        {
            try
            {
                return donDatHangRepository.GetDonDatHangs(searchTerm, status, page, pageSize);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<DonDatHangModel> GetDonDatHangById(int maDonDH)
        {
            try
            {
                return donDatHangRepository.GetDonDatHangById(maDonDH);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<DonDatHangDetailDTO> GetDonDatHangDetail(int maDonDH)
        {
            try
            {
                return donDatHangRepository.GetDonDatHangDetail(maDonDH);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<DonDatHangStatsDTO> GetDonDatHangStats()
        {
            try
            {
                return donDatHangRepository.GetDonDatHangStats();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<int> CreateDonDatHangComplex(CreateOrderRequestDTO request)
        {
            try
            {
                return donDatHangRepository.CreateDonDatHangComplex(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public Task<int> UpdateDonDatHang(DonDatHangModel donDatHangModel)
        {
            try
            {
                return donDatHangRepository.UpdateDonDatHang(donDatHangModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<int> DeleteDonDatHang(int maDonDH)
        {
            try
            {
                return donDatHangRepository.DeleteDonDatHang(maDonDH);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
