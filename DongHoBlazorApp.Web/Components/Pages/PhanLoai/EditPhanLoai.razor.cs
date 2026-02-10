using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace DongHoBlazorApp.Web.Components.Pages.PhanLoai
{
    public partial class EditPhanLoai
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

        [Parameter]
        public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (Id > 0)
                {
                    var res = await ApiClient.GetFromJsonAsync<BaseReponseModel>($"/api/PhanLoai/phanLoaiId?phanLoaiId={Id}");
                    if (res != null && res.Success)
                    {
                        PhanLoaiModel = JsonConvert.DeserializeObject<PhanLoaiModel>(res.Data.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task ConfirmEditPhanLoai()
        {
            IsSuccess = false;
            IsError = false;
            ErrorMessage = string.Empty;

            try
            {
                PhanLoaiModel.MaPL = Id; // Ensure ID is set correctly
                var res = await ApiClient.PutAsJsonAsync<BaseReponseModel, PhanLoaiModel>("/api/PhanLoai", PhanLoaiModel);
                if (res != null && res.Success)
                {
                    IsSuccess = true;
                    StateHasChanged();
                    await Task.Delay(2000); // Wait for 2 seconds to show success message
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
