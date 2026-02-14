using DongHoBlazorApp.Database.Data;
using DongHoBlazorApp.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace DongHoBlazorApp.BL.Reposities.BaoCao
{
    public class BaoCaoRepository(AppDbContext dbContext) : IBaoCaoRepository
    {
        public async Task<DashboardReportDto> GetDashboardReport(string filter)
        {
            var report = new DashboardReportDto();

            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.Now;

            switch (filter)
            {
                case "7 ngày qua":
                    startDate = DateTime.Now.AddDays(-7);
                    break;
                case "30 ngày qua":
                    startDate = DateTime.Now.AddDays(-30);
                    break;
                case "Tháng này":
                    startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    break;
                case "Quý này":
                    int currentQuarter = (DateTime.Now.Month - 1) / 3 + 1;
                    startDate = new DateTime(DateTime.Now.Year, (currentQuarter - 1) * 3 + 1, 1);
                    break;
                case "Năm nay":
                    startDate = new DateTime(DateTime.Now.Year, 1, 1);
                    break;
            }

            // 0. Overview Metrics
            report.TotalProducts = await dbContext.DongHos.CountAsync();
            report.TotalOrders = await dbContext.DonDatHangs.CountAsync();
            report.TotalCustomers = await dbContext.KhachHangs.CountAsync();
            report.TotalRevenue = await dbContext.ChiTietDonDHs
                .SumAsync(d => (decimal?)(d.DonGia * d.SoLuong)) ?? 0m;

            // 1. Revenue By Month
            for (int i = 5; i >= 0; i--)
            {
                var targetMonth = DateTime.Now.AddMonths(-i);
                var monthLabel = $"T{targetMonth.Month}";
                
                var revenue = await dbContext.DonDatHangs
                    .Where(o => o.NgayDat.Month == targetMonth.Month && o.NgayDat.Year == targetMonth.Year)
                    .Join(dbContext.ChiTietDonDHs, o => o.MaDonDH, d => d.MaDonDH, (o, d) => new { d.DonGia, d.SoLuong })
                    .SumAsync(x => (decimal?)(x.DonGia * x.SoLuong)) ?? 0m;

                report.RevenueByMonth.Add(new RevenueByMonthDto
                {
                    Month = monthLabel,
                    Revenue = revenue,
                    Profit = revenue * 0.3m
                });
            }

            // 2. Sales By Brand
            var salesByBrand = await dbContext.ChiTietDonDHs
                .Join(dbContext.DongHos, d => d.MaDongHo, h => h.MaDongHo, (d, h) => new { d, h })
                .Join(dbContext.ThuongHieus, combined => combined.h.MaThuongHieu, b => b.MaThuongHieu, (combined, b) => new { b.TenThuongHieu, combined.d.SoLuong })
                .GroupBy(x => x.TenThuongHieu)
                .Select(g => new SalesByBrandDto
                {
                    BrandName = g.Key,
                    SalesCount = g.Sum(x => x.SoLuong)
                })
                .OrderByDescending(x => x.SalesCount)
                .Take(5)
                .ToListAsync();

            int totalSalesCount = salesByBrand.Sum(x => x.SalesCount);
            if (totalSalesCount > 0)
            {
                foreach (var item in salesByBrand)
                {
                    item.Percentage = Math.Round((double)item.SalesCount / totalSalesCount * 100, 1);
                }
            }
            report.SalesByBrand = salesByBrand;

            // 3. Top Customers
            report.TopCustomers = await dbContext.DonDatHangs
                .Join(dbContext.KhachHangs, o => o.MaKH, k => k.MaKH, (o, k) => new { o, k })
                .GroupBy(x => new { x.k.MaKH, x.k.TenKH })
                .Select(g => new TopCustomerDto
                {
                    CustomerId = g.Key.MaKH,
                    CustomerName = g.Key.TenKH,
                    OrderCount = g.Count(),
                    TotalSpent = 0 
                })
                .OrderByDescending(x => x.OrderCount)
                .Take(4)
                .ToListAsync();

            foreach (var customer in report.TopCustomers)
            {
                customer.TotalSpent = await dbContext.DonDatHangs
                    .Where(o => o.MaKH == customer.CustomerId)
                    .Join(dbContext.ChiTietDonDHs, o => o.MaDonDH, d => d.MaDonDH, (o, d) => (decimal?)(d.DonGia * d.SoLuong))
                    .SumAsync() ?? 0m;
            }

            // 4. Top Selling Products
            var topIds = await dbContext.ChiTietDonDHs
                .GroupBy(d => d.MaDongHo)
                .Select(g => new { MaDH = g.Key, Count = g.Sum(x => x.SoLuong) })
                .OrderByDescending(x => x.Count)
                .Take(4)
                .ToListAsync();
            
            report.TopSellingProducts = new List<TopProductDto>();
            foreach(var tid in topIds)
            {
                var h = await dbContext.DongHos.FirstOrDefaultAsync(dh => dh.MaDongHo == tid.MaDH);
                report.TopSellingProducts.Add(new TopProductDto {
                    ProductId = tid.MaDH,
                    ProductName = h?.TenDongHo ?? "Unknown",
                    SalesCount = tid.Count,
                    Price = h?.GiaBan ?? 0m
                });
            }

            // 5. Recent Orders
            report.RecentOrders = await dbContext.DonDatHangs
                .OrderByDescending(o => o.NgayDat)
                .Take(5)
                .Join(dbContext.KhachHangs, o => o.MaKH, k => k.MaKH, (o, k) => new { o, k })
                .Select(combined => new RecentOrderDto
                {
                    OrderId = combined.o.MaDonDH,
                    CustomerName = combined.k.TenKH,
                    OrderDate = combined.o.NgayDat,
                    Status = combined.o.TinhTrang,
                    TotalAmount = 0 
                })
                .ToListAsync();

            foreach (var order in report.RecentOrders)
            {
                order.TotalAmount = await dbContext.ChiTietDonDHs
                    .Where(d => d.MaDonDH == order.OrderId)
                    .SumAsync(d => (decimal?)(d.DonGia * d.SoLuong)) ?? 0m;
                
                var firstProduct = await dbContext.ChiTietDonDHs
                    .Where(d => d.MaDonDH == order.OrderId)
                    .Join(dbContext.DongHos, d => d.MaDongHo, h => h.MaDongHo, (d, h) => h.TenDongHo)
                    .FirstOrDefaultAsync();
                order.ProductName = firstProduct ?? "N/A";
            }

            // 6. Performance Metrics
            report.PerformanceMetrics = new PerformanceMetricsDto
            {
                ConversionRate = 3.2,
                AverageOrderValue = (await dbContext.ChiTietDonDHs.AnyAsync()) ? await dbContext.ChiTietDonDHs.AverageAsync(d => (decimal?)(d.DonGia * d.SoLuong)) ?? 0m : 0m,
                CompletionRate = 90.8,
                AverageProcessingTime = 2.4
            };

            return report;
        }
    }
}
