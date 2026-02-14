using DongHoBlazorApp.Database.Data;
using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace DongHoBlazorApp.BL.Reposities.DonDHang
{
    public class DonDatHangRepository(AppDbContext dbContext) : IDonDatHangRepository
    {
        public async Task<PaginatedResult<DonDatHangModel>> GetDonDatHangs(string? searchTerm, string? status, int page, int pageSize)
        {
            try
            {
                var query = dbContext.DonDatHangs.AsQueryable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    // Assuming searching by customer info or order ID? 
                    // Let's search by status or customer ID for now as placeholders if MaKH is searchable
                    // Or if there's a name field in a related table, but let's keep it simple.
                    query = query.Where(d => d.MaDonDH.ToString().Contains(searchTerm) || d.MaKH.ToString().Contains(searchTerm));
                }

                if (!string.IsNullOrEmpty(status) && status != "Tất cả trạng thái")
                {
                    query = query.Where(d => d.TinhTrang == status);
                }

                var totalCount = await query.CountAsync();
                var items = await query
                    .OrderByDescending(d => d.NgayDat)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new PaginatedResult<DonDatHangModel>
                {
                    Items = items,
                    TotalCount = totalCount
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<DonDatHangModel> GetDonDatHangById(int maDonDH)
        {
            try
            {
                return dbContext.DonDatHangs
                    .FirstOrDefaultAsync(ddh => ddh.MaDonDH == maDonDH);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<DonDatHangDetailDTO> GetDonDatHangDetail(int maDonDH)
        {
            try
            {
                var order = await dbContext.DonDatHangs
                    .FirstOrDefaultAsync(d => d.MaDonDH == maDonDH);
                if (order == null) return null;

                var customer = await dbContext.KhachHangs
                    .FirstOrDefaultAsync(k => k.MaKH == order.MaKH);

                var items = await dbContext.ChiTietDonDHs
                    .Where(i => i.MaDonDH == maDonDH)
                    .ToListAsync();

                return new DonDatHangDetailDTO
                {
                    Order = order,
                    Customer = customer,
                    Items = items
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<DonDatHangStatsDTO> GetDonDatHangStats()
        {
            try
            {
                var stats = new DonDatHangStatsDTO
                {
                    TotalOrders = await dbContext.DonDatHangs.CountAsync(),
                    ProcessingOrders = await dbContext.DonDatHangs.CountAsync(d => d.TinhTrang == "Đang xử lý"),
                    CompletedOrders = await dbContext.DonDatHangs.CountAsync(d => d.TinhTrang == "Hoàn thành"),
                    CancelledOrders = await dbContext.DonDatHangs.CountAsync(d => d.TinhTrang == "Đã hủy")
                };
                return stats;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> CreateDonDatHang(DonDatHangModel donDatHangModel)
        {
            try
            {
                var entry = await dbContext.DonDatHangs.AddAsync(donDatHangModel);
                await dbContext.SaveChangesAsync();
                return entry.Entity.MaDonDH;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> UpdateDonDatHang(DonDatHangModel donDatHangModel)
        {
            try
            {
                var existingOrder = await dbContext.DonDatHangs
                    .FirstOrDefaultAsync(ddh => ddh.MaDonDH == donDatHangModel.MaDonDH);
                if (existingOrder == null)
                {
                    throw new Exception("Order not found");
                }
                dbContext.Entry(existingOrder).CurrentValues.SetValues(donDatHangModel);
                await dbContext.SaveChangesAsync();
                return existingOrder.MaDonDH;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> DeleteDonDatHang(int maDonDH)
        {
            try
            {
                var existingOrder = await dbContext.DonDatHangs
                    .FirstOrDefaultAsync(ddh => ddh.MaDonDH == maDonDH);
                if (existingOrder == null)
                {
                    throw new Exception("Order not found");
                }
                dbContext.DonDatHangs.Remove(existingOrder);
                await dbContext.SaveChangesAsync();
                return maDonDH;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
