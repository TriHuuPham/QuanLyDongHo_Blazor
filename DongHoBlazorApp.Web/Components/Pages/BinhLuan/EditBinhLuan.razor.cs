using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace DongHoBlazorApp.Web.Components.Pages.BinhLuan
{
    public partial class EditBinhLuan
    {
        [Inject]
        public ApiClient ApiClient { get; set; }

        [SupplyParameterFromForm]
        public BinhLuanModel BinhLuanModel { get; set; } = new();
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public bool IsSuccess { get; set; } = false;
        public bool IsError { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;

        [Parameter]
        public int Id { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (Id > 0)
                {
                    var res = await ApiClient.GetFromJsonAsync<BaseReponseModel>($"/api/BinhLuan/binhLuanId?binhLuanId={Id}");
                    if (res != null && res.Success && res.Data != null)
                    {
                        BinhLuanModel = JsonConvert.DeserializeObject<BinhLuanModel>(res.Data.ToString()) ?? new();
                    }
                    else
                    {
                        IsError = true;
                        ErrorMessage = res?.ErrorMessage ?? "Không tìm thấy thông tin bình luận.";
                    }
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                ErrorMessage = "Lỗi khi tải dữ liệu: " + ex.Message;
            }
        }
        public async Task ConfirmEditBinhLuan()
        {
            IsSuccess = false;
            IsError = false;
            ErrorMessage = string.Empty;

            try
            {
                BinhLuanModel.MaBL = Id; // Ensure ID is set correctly
                var res = await ApiClient.PutAsJsonAsync<BaseReponseModel, BinhLuanModel>("/api/BinhLuan", BinhLuanModel);
                if (res != null && res.Success)
                {
                    IsSuccess = true;
                    StateHasChanged();
                    await Task.Delay(2000); // Wait for 2 seconds to show success message
                    NavigationManager.NavigateTo("/BinhLuan");
                }
                else
                {
                    IsError = true;
                    ErrorMessage = res?.ErrorMessage ?? "Đã có lỗi xảy ra.";
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                ErrorMessage = ex.Message;
            }
        }
    }
}
