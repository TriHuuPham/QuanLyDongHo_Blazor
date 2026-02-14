using System;
using System.Collections.Generic;

namespace DongHoBlazorApp.Model.Models
{
    public class RevenueByMonthDto
    {
        public string Month { get; set; } = string.Empty;
        public decimal Revenue { get; set; }
        public decimal Profit { get; set; }
    }

    public class SalesByBrandDto
    {
        public string BrandName { get; set; } = string.Empty;
        public int SalesCount { get; set; }
        public double Percentage { get; set; }
    }

    public class TopCustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int OrderCount { get; set; }
        public decimal TotalSpent { get; set; }
    }

    public class PerformanceMetricsDto
    {
        public double ConversionRate { get; set; }
        public decimal AverageOrderValue { get; set; }
        public double CompletionRate { get; set; }
        public double AverageProcessingTime { get; set; }
    }

    public class TopProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int SalesCount { get; set; }
        public decimal Price { get; set; }
    }

    public class RecentOrderDto
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class DashboardReportDto
    {
        public int TotalProducts { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalCustomers { get; set; }
        public List<RevenueByMonthDto> RevenueByMonth { get; set; } = new();
        public List<SalesByBrandDto> SalesByBrand { get; set; } = new();
        public List<TopCustomerDto> TopCustomers { get; set; } = new();
        public List<TopProductDto> TopSellingProducts { get; set; } = new();
        public List<RecentOrderDto> RecentOrders { get; set; } = new();
        public PerformanceMetricsDto PerformanceMetrics { get; set; } = new();
    }
}
