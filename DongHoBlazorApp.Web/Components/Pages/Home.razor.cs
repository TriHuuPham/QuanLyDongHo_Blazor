using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace DongHoBlazorApp.Web.Components.Pages
{
    public partial class Home
    {
        [Inject]
        public HttpClient ApiClient { get; set; } = default!;

        private DashboardReportDto Report { get; set; } = new();
        private bool IsLoading { get; set; } = true;
        private string SelectedFilter { get; set; } = "7 ngày qua";

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                IsLoading = true;
                var res = await ApiClient.GetFromJsonAsync<DashboardReportDto>($"/api/BaoCao/Dashboard?filter={Uri.EscapeDataString(SelectedFilter)}");
                if (res != null)
                {
                    Report = res;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading dashboard: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task OnFilterChanged(ChangeEventArgs e)
        {
            SelectedFilter = e.Value?.ToString() ?? "7 ngày qua";
            await LoadData();
        }
    }
}
