using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Components;

namespace DongHoBlazorApp.Web.Components.Pages.BaoCao
{
    public partial class IndexBaoCao
    {
        [Inject]
        public ApiClient ApiClient { get; set; } = default!;

        public DashboardReportDto? ReportData { get; set; }
        public string SelectedFilter { get; set; } = "7 ngày qua";
        public bool IsLoading { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            await LoadReportData();
        }

        public async Task LoadReportData()
        {
            IsLoading = true;
            try
            {
                ReportData = await ApiClient.GetFromJsonAsync<DashboardReportDto>($"/api/BaoCao/Dashboard?filter={SelectedFilter}");
            }
            catch (Exception ex)
            {
                // Handle error
                Console.WriteLine($"Error loading report data: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task OnFilterChanged(ChangeEventArgs e)
        {
            SelectedFilter = e.Value?.ToString() ?? "7 ngày qua";
            await LoadReportData();
        }
    }
}
