using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Components;

namespace DongHoBlazorApp.Web.Components.Pages.LoaiThanhVien
{
    public partial class CreateLoaiThanhVien
    {
        [Inject]
        public ApiClient ApiClient { get; set; }

        [SupplyParameterFromForm]
        public LoaiThanhVienModel LoaiThanhVienModel { get; set; } = new();
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public bool IsSuccess { get; set; } = false;
        public bool IsError { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;

        public async Task ConfirmCreateLoaiThanhVien()
        {
            IsSuccess = false;
            IsError = false;
            ErrorMessage = string.Empty;

            try
            {
                var res = await ApiClient.PostAsJsonAsync<BaseReponseModel, LoaiThanhVienModel>("/api/LoaiThanhVien", LoaiThanhVienModel);
                if (res != null && res.Success)
                {
                    IsSuccess = true;
                    StateHasChanged();
                    await Task.Delay(2000); // Wait for 1 second to show success message
                    NavigationManager.NavigateTo("/LoaiThanhVien");
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
