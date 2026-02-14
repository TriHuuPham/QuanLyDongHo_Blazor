using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace DongHoBlazorApp.Web.Components.Pages.DonDatHang
{
    public partial class IndexDonDatHang
    {
        [Inject]
        public ApiClient ApiClient { get; set; }

        private List<DonDatHangModel> L_DonDatHang { get; set; } = new();

        private string SearchTerm { get; set; } = "";
        private string SelectedStatus { get; set; } = "Tất cả trạng thái";
        private int CurrentPage { get; set; } = 1;
        private int PageSize { get; set; } = 5;
        private int TotalItems { get; set; } = 0;
        private int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
        private DonDatHangStatsDTO Stats { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            // Fetch Stats
            var statsRes = await ApiClient.GetFromJsonAsync<BaseReponseModel>("/api/DonDatHang/stats");
            if (statsRes != null && statsRes.Success)
            {
                Stats = JsonConvert.DeserializeObject<DonDatHangStatsDTO>(statsRes.Data.ToString()) ?? new();
            }

            var url = $"/api/DonDatHang?searchTerm={Uri.EscapeDataString(SearchTerm)}&status={Uri.EscapeDataString(SelectedStatus)}&page={CurrentPage}&pageSize={PageSize}";
            var res = await ApiClient.GetFromJsonAsync<BaseReponseModel>(url);
            if (res != null && res.Success)
            {
                var paginatedResult = JsonConvert.DeserializeObject<PaginatedResult<DonDatHangModel>>(res.Data.ToString());
                if (paginatedResult != null)
                {
                    L_DonDatHang = paginatedResult.Items;
                    TotalItems = paginatedResult.TotalCount;
                }
            }
        }

        private async Task HandleSearch(ChangeEventArgs e)
        {
            SearchTerm = e.Value?.ToString() ?? "";
            CurrentPage = 1;
            await LoadData();
        }

        private async Task HandleStatusChange(ChangeEventArgs e)
        {
            SelectedStatus = e.Value?.ToString() ?? "Tất cả trạng thái";
            CurrentPage = 1;
            await LoadData();
        }

        private async Task ChangePage(int page)
        {
            if (page >= 1 && page <= TotalPages)
            {
                CurrentPage = page;
                await LoadData();
            }
        }

        private async Task UpdateStatus(DonDatHangModel item, string newStatus)
        {
            item.TinhTrang = newStatus;
            var res = await ApiClient.PutAsJsonAsync<BaseReponseModel, DonDatHangModel>("/api/DonDatHang", item);
            if (res != null && res.Success)
            {
                await LoadData(); // Refresh list
            }
        }
    }
}
