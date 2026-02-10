using DongHoBlazorApp.Database.Data;
using DongHoBlazorApp.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace DongHoBlazorApp.BL.Reposities.BinhLuan
{
    public class BinhLuanRepository(AppDbContext dbContext) : IBinhLuanRepository
    {
        public Task<List<BinhLuanModel>> GetBinhLuans()
        {
            try
            {
                return dbContext.BinhLuans
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<BinhLuanModel> GetBinhLuanById(int maBL)
        {
            try
            {
                return dbContext.BinhLuans
                    .FirstOrDefaultAsync(bl => bl.MaBL == maBL);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<BinhLuanModel> CreateBinhLuan(BinhLuanModel binhLuanModel)
        {
            try
            {
                var entry = await dbContext.BinhLuans.AddAsync(binhLuanModel);
                await dbContext.SaveChangesAsync();
                return entry.Entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteBinhLuan(BinhLuanModel binhLuanModel)
        {
            try
            {
                dbContext.BinhLuans.Remove(binhLuanModel);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<BinhLuanModel> UpdateBinhLuan(BinhLuanModel binhLuanModel)
        {
            try
            {
                var entry = dbContext.BinhLuans.Update(binhLuanModel);
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
