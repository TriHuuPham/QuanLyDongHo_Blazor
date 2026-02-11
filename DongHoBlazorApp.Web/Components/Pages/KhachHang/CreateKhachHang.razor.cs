using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace DongHoBlazorApp.Web.Components.Pages.KhachHang
{
    public partial class CreateKhachHang
    {
        [Inject]
        public ApiClient ApiClient { get; set; }

        [SupplyParameterFromForm]
        public KhachHangModel KhachHangModel { get; set; } = new();
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public List<LoaiThanhVienModel> LoaiThanhVienModels { get; set; } = new();

        public bool IsSuccess { get; set; } = false;
        public bool IsError { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var res = await ApiClient.GetFromJsonAsync<BaseReponseModel>("/api/LoaiThanhVien");
                if (res != null && res.Success)
                {
                    LoaiThanhVienModels = JsonConvert.DeserializeObject<List<LoaiThanhVienModel>>(res.Data.ToString());
                    // Filter to only active ones if needed, or just let user choose
                    LoaiThanhVienModels = LoaiThanhVienModels.Where(w => w.TrangThai).ToList();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Không thể tải danh sách loại thành viên: " + ex.Message;
                IsError = true;
            }
        }

        public async Task ConfirmCreateKhachHang()
        {
            IsSuccess = false;
            IsError = false;
            ErrorMessage = string.Empty;

            try
            {
                var res = await ApiClient.PostAsJsonAsync<BaseReponseModel, KhachHangModel>("/api/KhachHang", KhachHangModel);
                if (res != null && res.Success)
                {
                    IsSuccess = true;
                    StateHasChanged();
                    await Task.Delay(2000); // Wait for 1 second to show success message
                    NavigationManager.NavigateTo("/KhachHang");
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
