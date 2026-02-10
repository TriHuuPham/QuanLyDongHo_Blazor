using DongHoBlazorApp.Database.Data;
using DongHoBlazorApp.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace DongHoBlazorApp.BL.Reposities.PhanLoai
{
    public class PhanLoaiRepository(AppDbContext appDbContext) : IPhanLoaiRepository
    {
        public Task<List<PhanLoaiModel>> GetPhanLoais()
        {
            try
            {
                return appDbContext.PhanLoais
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<PhanLoaiModel> GetPhanLoaiById(int maPL)
        {
            try
            {
                return appDbContext.PhanLoais
                    .FirstOrDefaultAsync(pl => pl.MaPL == maPL);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<PhanLoaiModel> CreatePhanLoai(PhanLoaiModel phanLoaiModel)
        {
            try
            {
                var entry = await appDbContext.PhanLoais.AddAsync(phanLoaiModel);
                await appDbContext.SaveChangesAsync();
                return entry.Entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeletePhanLoai(PhanLoaiModel phanLoaiModel)
        {
            try
            {
                appDbContext.PhanLoais.Remove(phanLoaiModel);
                await appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<PhanLoaiModel> UpdatePhanLoai(PhanLoaiModel phanLoaiModel)
        {
            try
            {
                var entry = appDbContext.PhanLoais.Update(phanLoaiModel);
                await appDbContext.SaveChangesAsync();
                return entry.Entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
