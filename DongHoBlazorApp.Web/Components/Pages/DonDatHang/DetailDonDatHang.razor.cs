using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace DongHoBlazorApp.Web.Components.Pages.DonDatHang
{
    public partial class DetailDonDatHang
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public ApiClient ApiClient { get; set; }

        private DonDatHangDetailDTO Detail { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadDetailData();
        }

        private async Task LoadDetailData()
        {
            var res = await ApiClient.GetFromJsonAsync<BaseReponseModel>($"/api/DonDatHang/detail/{Id}");
            if (res != null && res.Success)
            {
                Detail = JsonConvert.DeserializeObject<DonDatHangDetailDTO>(res.Data.ToString());
            }
        }
    }
}
