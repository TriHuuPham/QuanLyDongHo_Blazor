using DongHoBlazorApp.Database.Data;
using DongHoBlazorApp.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace DongHoBlazorApp.BL.Reposities.ChiTietPhieuNhap
{
    public class ChiTietPhieuNhapRepository(AppDbContext dbContext) : IChiTietPhieuNhapRepository
    {
        public Task<List<ChiTietPhieuNhapModel>> GetChiTietPhieuNhaps()
        {
            try
            {
                var chiTietPhieuNhaps = dbContext.ChiTietPhieuNhaps.ToListAsync();
                return chiTietPhieuNhaps;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<ChiTietPhieuNhapModel> GetChiTietPhieuNhapById(int maCTPN)
        {
            try
            {
                var chiTietPhieuNhap = dbContext.ChiTietPhieuNhaps.FirstOrDefaultAsync(ctpn => ctpn.MaChiTietPhieuNhap == maCTPN);
                return chiTietPhieuNhap;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ChiTietPhieuNhapModel> CreateChiTietPhieuNhap(ChiTietPhieuNhapModel chiTietPhieuNhapModel)
        {
            try
            {
                await dbContext.ChiTietPhieuNhaps.AddAsync(chiTietPhieuNhapModel);
                await dbContext.SaveChangesAsync();
                return chiTietPhieuNhapModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteChiTietPhieuNhap(ChiTietPhieuNhapModel chiTietPhieuNhapModel)
        {
            try
            {
                dbContext.ChiTietPhieuNhaps.Remove(chiTietPhieuNhapModel);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ChiTietPhieuNhapModel> UpdateChiTietPhieuNhap(ChiTietPhieuNhapModel chiTietPhieuNhapModel)
        {
            try
            {
                dbContext.ChiTietPhieuNhaps.Update(chiTietPhieuNhapModel);
                await dbContext.SaveChangesAsync();
                return chiTietPhieuNhapModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
