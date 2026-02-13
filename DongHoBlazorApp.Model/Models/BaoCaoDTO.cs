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

    public class DashboardReportDto
    {
        public List<RevenueByMonthDto> RevenueByMonth { get; set; } = new();
        public List<SalesByBrandDto> SalesByBrand { get; set; } = new();
        public List<TopCustomerDto> TopCustomers { get; set; } = new();
        public PerformanceMetricsDto PerformanceMetrics { get; set; } = new();
    }
}
