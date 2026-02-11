using DongHoBlazorApp.Database.Data;
using DongHoBlazorApp.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace DongHoBlazorApp.BL.Reposities.LoaiThanhVien
{
    public class LoaiThanhVienRepository(AppDbContext dbContext) : ILoaiThanhVienRepository
    {
        public Task<List<LoaiThanhVienModel>> GetLoaiThanhViens()
        {
            try
            {
                return dbContext.LoaiThanhViens
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<LoaiThanhVienModel> GetLoaiThanhVienById(int maLoaiTV)
        {
            try
            {
                return dbContext.LoaiThanhViens
                    .FirstOrDefaultAsync(ltv => ltv.MaLoaiTV == maLoaiTV);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<LoaiThanhVienModel> CreateLoaiThanhVien(LoaiThanhVienModel loaiThanhVienModel)
        {
            try
            {
                var entry = await dbContext.LoaiThanhViens.AddAsync(loaiThanhVienModel);
                await dbContext.SaveChangesAsync();
                return entry.Entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteLoaiThanhVien(LoaiThanhVienModel loaiThanhVienModel)
        {
            try
            {
                dbContext.LoaiThanhViens.Remove(loaiThanhVienModel);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<LoaiThanhVienModel> UpdateLoaiThanhVien(LoaiThanhVienModel loaiThanhVienModel)
        {
            try
            {
                var entry = dbContext.LoaiThanhViens.Update(loaiThanhVienModel);
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
