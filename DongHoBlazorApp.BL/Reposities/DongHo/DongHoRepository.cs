using DongHoBlazorApp.Database.Data;
using DongHoBlazorApp.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace DongHoBlazorApp.BL.Reposities.DongHo
{
    public class DongHoRepository(AppDbContext DbContext) : IDongHoRepository
    {
        public Task<List<DongHoModel>> GetDongHos()
        {
            try
            {
                return DbContext.DongHos
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<DongHoModel> GetDongHoById(int maDH)
        {
            try
            {
                return DbContext.DongHos
                    .FirstOrDefaultAsync(dh => dh.MaDongHo == maDH);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<DongHoModel> CreateDongHo(DongHoModel dongHoModel)
        {
            try
            {
                var entry = await DbContext.DongHos.AddAsync(dongHoModel);
                await DbContext.SaveChangesAsync();
                return entry.Entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteDongHo(DongHoModel dongHoModel)
        {
            try
            {
                DbContext.DongHos.Remove(dongHoModel);
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<DongHoModel> UpdateDongHo(DongHoModel dongHoModel)
        {
            try
            {
                var entry = DbContext.DongHos.Update(dongHoModel);
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
