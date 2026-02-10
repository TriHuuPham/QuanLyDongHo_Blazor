using DongHoBlazorApp.Database.Data;
using DongHoBlazorApp.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace DongHoBlazorApp.BL.Reposities.PhieuNhap
{
    public class PhieuNhapRepository(AppDbContext dbContext) : IPhieuNhapRepository
    {
        public Task<List<PhieuNhapModel>> GetPhieuNhaps()
        {
            try
            {
                return dbContext.PhieuNhaps
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<PhieuNhapModel> GetPhieuNhapById(int maPN)
        {
            try
            {
                return dbContext.PhieuNhaps
                    .FirstOrDefaultAsync(pn => pn.MaPhieuNhap == maPN);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<PhieuNhapModel> CreatePhieuNhap(PhieuNhapModel phieuNhapModel)
        {
            try
            {
                var entry = await dbContext.PhieuNhaps.AddAsync(phieuNhapModel);
                await dbContext.SaveChangesAsync();
                return entry.Entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeletePhieuNhap(PhieuNhapModel phieuNhapModel)
        {
            try
            {
                dbContext.PhieuNhaps.Remove(phieuNhapModel);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<PhieuNhapModel> UpdatePhieuNhap(PhieuNhapModel phieuNhapModel)
        {
            try
            {
                var entry = dbContext.PhieuNhaps.Update(phieuNhapModel);
                await dbContext.SaveChangesAsync();
                return entry.Entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
