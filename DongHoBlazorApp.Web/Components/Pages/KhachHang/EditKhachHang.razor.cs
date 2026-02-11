using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace DongHoBlazorApp.Web.Components.Pages.KhachHang
{
    public partial class EditKhachHang
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public ApiClient ApiClient { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [SupplyParameterFromForm]
        public KhachHangModel KhachHangModel { get; set; } = new();

        public List<LoaiThanhVienModel> LoaiThanhVienModels { get; set; } = new();

        public bool IsSuccess { get; set; } = false;
        public bool IsError { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                // Fetch Rank list
                var resRank = await ApiClient.GetFromJsonAsync<BaseReponseModel>("/api/LoaiThanhVien");
                if (resRank != null && resRank.Success)
                {
                    LoaiThanhVienModels = JsonConvert.DeserializeObject<List<LoaiThanhVienModel>>(resRank.Data.ToString());
                    LoaiThanhVienModels = LoaiThanhVienModels.Where(w => w.TrangThai).ToList();
                }

                if (Id > 0)
                {
                    // Use consistent endpoint if possible, assuming /api/KhachHang/{id} works
                    var resKH = await ApiClient.GetFromJsonAsync<BaseReponseModel>($"/api/KhachHang/khachHangId?khachHangId={Id}");
                    if (resKH != null && resKH.Success)
                    {
                        KhachHangModel = JsonConvert.DeserializeObject<KhachHangModel>(resKH.Data.ToString());
                    }
                    else
                    {
                        IsError = true;
                        ErrorMessage = resKH?.ErrorMessage ?? "Không tìm thấy khách hàng.";
                    }
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                ErrorMessage = "Lỗi khi tải dữ liệu: " + ex.Message;
            }
        }


        public async Task ConfirmEditKhachHang()
        {
            IsSuccess = false;
            IsError = false;
            ErrorMessage = string.Empty;

            try
            {
                KhachHangModel.MaKH = Id; // Ensure ID is set correctly
                var res = await ApiClient.PutAsJsonAsync<BaseReponseModel, KhachHangModel>("/api/KhachHang", KhachHangModel);
                if (res != null && res.Success)
                {
                    IsSuccess = true;
                    StateHasChanged();
                    await Task.Delay(2000); // Wait for 2 seconds to show success message
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
