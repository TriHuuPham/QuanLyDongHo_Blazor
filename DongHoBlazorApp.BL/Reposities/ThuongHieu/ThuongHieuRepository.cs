using DongHoBlazorApp.Database.Data;
using DongHoBlazorApp.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace DongHoBlazorApp.BL.Reposities.ThuongHieu
{
    public class ThuongHieuRepository(AppDbContext DbContext) : IThuongHieuRepository
    {
        public Task<List<ThuongHieuModel>> GetThuongHieus()
        {
            try
            {
                return DbContext.ThuongHieus
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<ThuongHieuModel> GetThuongHieuById(int maTH)
        {
            try
            {
                return DbContext.ThuongHieus
                    .FirstOrDefaultAsync(th => th.MaThuongHieu == maTH);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ThuongHieuModel> CreateThuongHieu(ThuongHieuModel thuongHieuModel)
        {
            try
            {
                var entry = await DbContext.ThuongHieus.AddAsync(thuongHieuModel);
                await DbContext.SaveChangesAsync();
                return entry.Entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteThuongHieu(ThuongHieuModel thuongHieuModel)
        {
            try
            {
                DbContext.ThuongHieus.Remove(thuongHieuModel);
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ThuongHieuModel> UpdateThuongHieu(ThuongHieuModel thuongHieuModel)
        {
            try
            {
                var entry = DbContext.ThuongHieus.Update(thuongHieuModel);
                await DbContext.SaveChangesAsync();
                return entry.Entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
