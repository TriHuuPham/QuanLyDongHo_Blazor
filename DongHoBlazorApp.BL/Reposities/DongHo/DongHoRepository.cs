using DongHoBlazorApp.Database.Data;
using DongHoBlazorApp.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace DongHoBlazorApp.BL.Reposities.DongHo
{
    public class DongHoRepository(AppDbContext dbContext) : IDongHoRepository
    {
        public Task<List<DongHoModel>> GetDongHos()
        {
            return dbContext.DongHos.ToListAsync();
        }

        public Task<List<DongHoModel>> SearchDongHos(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return dbContext.DongHos.Take(10).ToListAsync();
            }

            return dbContext.DongHos
                .Where(d => d.TenDongHo.Contains(searchTerm) || d.MaDongHo.ToString().Contains(searchTerm))
                .Take(20)
                .ToListAsync();
        }

        public Task<DongHoModel> GetDongHoById(int maDH)
        {
            return dbContext.DongHos
                .FirstOrDefaultAsync(dh => dh.MaDongHo == maDH);
        }

        public async Task<DongHoModel> CreateDongHo(DongHoModel dongHoModel)
        {
            var entry = await dbContext.DongHos.AddAsync(dongHoModel);
            await dbContext.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task DeleteDongHo(DongHoModel dongHoModel)
        {
            dbContext.DongHos.Remove(dongHoModel);
            await dbContext.SaveChangesAsync();
        }

        public async Task<DongHoModel> UpdateDongHo(DongHoModel dongHoModel)
        {
            var entry = dbContext.DongHos.Update(dongHoModel);
            await dbContext.SaveChangesAsync();
            return entry.Entity;
        }
    }
}
