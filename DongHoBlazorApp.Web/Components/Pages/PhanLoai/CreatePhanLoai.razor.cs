using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace DongHoBlazorApp.Web.Components.Pages.PhanLoai
{
    public partial class CreatePhanLoai
    {
        [Inject]
        public ApiClient ApiClient { get; set; }

        [SupplyParameterFromForm]
        public PhanLoaiModel PhanLoaiModel { get; set; } = new();
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public bool IsSuccess { get; set; } = false;
        public bool IsError { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;

        public async Task ConfirmCreatePhanLoai()
        {
            IsSuccess = false;
            IsError = false;
            ErrorMessage = string.Empty;

            try
            {
                var res = await ApiClient.PostAsJsonAsync<BaseReponseModel, PhanLoaiModel>("/api/PhanLoai", PhanLoaiModel);
                if (res != null && res.Success)
                {
                    IsSuccess = true;
                    StateHasChanged();
                    await Task.Delay(2000); // Wait for 1 second to show success message
                    NavigationManager.NavigateTo("/PhanLoai");
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
