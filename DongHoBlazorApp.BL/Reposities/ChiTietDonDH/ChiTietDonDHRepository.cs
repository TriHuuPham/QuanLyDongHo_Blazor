using DongHoBlazorApp.Database.Data;
using DongHoBlazorApp.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace DongHoBlazorApp.BL.Reposities.ChiTietDonDH
{
    public class ChiTietDonDHRepository(AppDbContext dbContext) : IChiTietDonDHRepository
    {
        public Task<List<ChiTietDonDHModel>> GetChiTietDonDHs()
        {
            try
            {
                return dbContext.ChiTietDonDHs
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<ChiTietDonDHModel> GetChiTietDonDHById(int id)
        {
            try
            {
                return dbContext.ChiTietDonDHs
                    .FirstOrDefaultAsync(x => x.MaChiTietDonDH == id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ChiTietDonDHModel> CreateChiTietDonDH(ChiTietDonDHModel chiTietDonDH)
        {
            try
            {
                var entity = (await dbContext.ChiTietDonDHs.AddAsync(chiTietDonDH)).Entity;
                await dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ChiTietDonDHModel> UpdateChiTietDonDH(ChiTietDonDHModel chiTietDonDH)
        {
            try
            {
                var entity = dbContext.ChiTietDonDHs.Update(chiTietDonDH).Entity;
                await dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> DeleteChiTietDonDH(int id)
        {
            try
            {
                var entity = await dbContext.ChiTietDonDHs
                    .FirstOrDefaultAsync(x => x.MaChiTietDonDH == id);
                if (entity == null)
                {
                    return false;
                }
                dbContext.ChiTietDonDHs.Remove(entity);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
