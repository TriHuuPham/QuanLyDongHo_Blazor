using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace DongHoBlazorApp.Web.Components.Pages.LoaiThanhVien
{
    public partial class EditLoaiThanhVien
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

        [Parameter]
        public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (Id > 0)
                {
                    var res = await ApiClient.GetFromJsonAsync<BaseReponseModel>($"/api/LoaiThanhVien/loaiThanhVienId?loaiThanhVienId={Id}");
                    if (res != null && res.Success)
                    {
                        LoaiThanhVienModel = JsonConvert.DeserializeObject<LoaiThanhVienModel>(res.Data.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task ConfirmEditLoaiThanhVien()
        {
            IsSuccess = false;
            IsError = false;
            ErrorMessage = string.Empty;

            try
            {
                LoaiThanhVienModel.MaLoaiTV = Id; // Ensure ID is set correctly
                var res = await ApiClient.PutAsJsonAsync<BaseReponseModel, LoaiThanhVienModel>("/api/LoaiThanhVien", LoaiThanhVienModel);
                if (res != null && res.Success)
                {
                    IsSuccess = true;
                    StateHasChanged();
                    await Task.Delay(2000); // Wait for 2 seconds to show success message
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
