using DongHoBlazorApp.Database.Data;
using DongHoBlazorApp.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace DongHoBlazorApp.BL.Reposities.NhaCungCap
{
    public class NhaCungCapRepository(AppDbContext DbContext) : INhaCungCapRepository
    {
        public Task<List<NhaCungCapModel>> GetNhaCungCaps()
        {
            try
            {
                return DbContext.NhaCungCaps
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<NhaCungCapModel> GetNhaCungCapById(int maNCC)
        {
            try
            {
                return DbContext.NhaCungCaps
                    .FirstOrDefaultAsync(pl => pl.MaNCC == maNCC);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<NhaCungCapModel> CreateNhaCungCap(NhaCungCapModel nhaCungCapModel)
        {
            try
            {
                var entry = await DbContext.NhaCungCaps.AddAsync(nhaCungCapModel);
                await DbContext.SaveChangesAsync();
                return entry.Entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task DeleteNhaCungCap(NhaCungCapModel nhaCungCapModel)
        {
            try
            {
                DbContext.NhaCungCaps.Remove(nhaCungCapModel);
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<NhaCungCapModel> UpdateNhaCungCap(NhaCungCapModel nhaCungCapModel)
        {
            try
            {
                var entry = DbContext.NhaCungCaps.Update(nhaCungCapModel);
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
