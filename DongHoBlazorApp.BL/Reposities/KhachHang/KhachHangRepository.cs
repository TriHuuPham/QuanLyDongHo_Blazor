using DongHoBlazorApp.Database.Data;
using DongHoBlazorApp.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace DongHoBlazorApp.BL.Reposities.KhachHang
{
    public class KhachHangRepository (AppDbContext dbContext) : IKhachHangRepository
    {
        public Task<List<KhachHangModel>> GetKhachHangs()
        {
            try
            {
                return dbContext.KhachHangs
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<KhachHangModel> GetKhachHangById(int maKH)
        {
            try
            {
                return dbContext.KhachHangs
                    .FirstOrDefaultAsync(kh => kh.MaKH == maKH);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<KhachHangModel> CreateKhachHang(KhachHangModel khachHangModel)
        {
            try
            {
                var entry = await dbContext.KhachHangs.AddAsync(khachHangModel);
                await dbContext.SaveChangesAsync();
                return entry.Entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteKhachHang(KhachHangModel khachHangModel)
        {
            try
            {
                dbContext.KhachHangs.Remove(khachHangModel);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<KhachHangModel> UpdateKhachHang(KhachHangModel khachHangModel)
        {
            try
            {
                var entry = dbContext.KhachHangs.Update(khachHangModel);
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
