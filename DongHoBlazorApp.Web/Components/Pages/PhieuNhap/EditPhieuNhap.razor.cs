using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace DongHoBlazorApp.Web.Components.Pages.PhieuNhap
{
    public partial class EditPhieuNhap
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public ApiClient ApiClient { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public PhieuNhapModel CurrentPhieuNhap { get; set; } = new();
        public List<ChiTietPhieuNhapModel> ChiTietItems { get; set; } = new();
        public List<NhaCungCapModel> NhaCungCapModels { get; set; } = new();
        public List<DongHoModel> DongHoModels { get; set; } = new();

        public bool IsSuccess { get; set; } = false;
        public bool IsError { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var taskPhieuNhap = ApiClient.GetFromJsonAsync<BaseReponseModel>($"/api/PhieuNhap/phieuNhapId?phieuNhapId={Id}");
                var taskChiTiet = ApiClient.GetFromJsonAsync<BaseReponseModel>($"/api/ChiTietPhieuNhap"); // Fetching all for now, will filter
                var taskNhaCungCap = ApiClient.GetFromJsonAsync<BaseReponseModel>("/api/NhaCungCap");
                var taskDongHo = ApiClient.GetFromJsonAsync<BaseReponseModel>("/api/DongHo");

                await Task.WhenAll(taskPhieuNhap, taskChiTiet, taskNhaCungCap, taskDongHo);

                var phieuNhapRes = await taskPhieuNhap;
                var chiTietRes = await taskChiTiet;
                var nhaCungCapRes = await taskNhaCungCap;
                var dongHoRes = await taskDongHo;

                if (phieuNhapRes != null && phieuNhapRes.Success)
                {
                    CurrentPhieuNhap = JsonConvert.DeserializeObject<PhieuNhapModel>(phieuNhapRes.Data.ToString()) ?? new();
                }

                if (chiTietRes != null && chiTietRes.Success)
                {
                    var allChiTiet = JsonConvert.DeserializeObject<List<ChiTietPhieuNhapModel>>(chiTietRes.Data.ToString()) ?? new();
                    ChiTietItems = allChiTiet.Where(ct => ct.MaPhieuNhap == Id).ToList();
                }

                if (nhaCungCapRes != null && nhaCungCapRes.Success)
                {
                    NhaCungCapModels = JsonConvert.DeserializeObject<List<NhaCungCapModel>>(nhaCungCapRes.Data.ToString()) ?? new();
                }

                if (dongHoRes != null && dongHoRes.Success)
                {
                    DongHoModels = JsonConvert.DeserializeObject<List<DongHoModel>>(dongHoRes.Data.ToString()) ?? new();
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                ErrorMessage = ex.Message;
            }
        }

        public void AddRow()
        {
            ChiTietItems.Add(new ChiTietPhieuNhapModel { MaPhieuNhap = Id, SoLuongNhap = 1, DonGiaNhap = 0 });
        }

        public void RemoveRow(ChiTietPhieuNhapModel item)
        {
            if (ChiTietItems.Count > 1)
            {
                ChiTietItems.Remove(item);
            }
        }

        public decimal TotalQuantity => ChiTietItems.Sum(i => i.SoLuongNhap);
        public int TotalItems => ChiTietItems.Count;
        public decimal GrandTotal => ChiTietItems.Sum(i => (decimal)i.SoLuongNhap * i.DonGiaNhap);

        public async Task HandleSubmit()
        {
            try
            {
                IsError = false;
                IsSuccess = false;

                if (CurrentPhieuNhap.MaNCC == 0)
                {
                    IsError = true;
                    ErrorMessage = "Vui lòng chọn nhà cung cấp.";
                    return;
                }

                if (ChiTietItems.Any(i => i.MaDongHo == 0))
                {
                    IsError = true;
                    ErrorMessage = "Vui lòng chọn sản phẩm cho tất cả các hàng.";
                    return;
                }

                CurrentPhieuNhap.TongTien = GrandTotal;

                // 1. Update PhieuNhap
                var phieuNhapRes = await ApiClient.PutAsJsonAsync<BaseReponseModel, PhieuNhapModel>("/api/PhieuNhap", CurrentPhieuNhap);

                if (phieuNhapRes != null && phieuNhapRes.Success)
                {
                    // 2. Sync ChiTietPhieuNhap: Simplest way is to delete old ones and insert new ones
                    // Assuming delete by MaPhieuNhap endpoint exists or we use the one from IndexPhieuNhap
                    await ApiClient.DeleteAsync<BaseReponseModel>($"/api/ChiTietPhieuNhap?DeleteId={Id}");

                    foreach (var item in ChiTietItems)
                    {
                        item.MaPhieuNhap = Id;
                        await ApiClient.PostAsJsonAsync<BaseReponseModel, ChiTietPhieuNhapModel>("/api/ChiTietPhieuNhap", item);
                    }

                    IsSuccess = true;
                    await Task.Delay(2000);
                    NavigationManager.NavigateTo("/PhieuNhap");
                }
                else
                {
                    IsError = true;
                    ErrorMessage = phieuNhapRes?.ErrorMessage ?? "Không thể cập nhật phiếu nhập.";
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                ErrorMessage = ex.Message;
            }
        }

        public async Task ConfirmDelete()
        {
            try
            {
                // Similar to IndexPhieuNhap delete logic
                var phieuNhapRes = await ApiClient.DeleteAsync<BaseReponseModel>($"/api/PhieuNhap?DeleteId={Id}");
                var chiTietPhieuNhapRes = await ApiClient.DeleteAsync<BaseReponseModel>($"/api/ChiTietPhieuNhap?DeleteId={Id}");

                if (phieuNhapRes != null && phieuNhapRes.Success && chiTietPhieuNhapRes != null && chiTietPhieuNhapRes.Success)
                {
                    NavigationManager.NavigateTo("/PhieuNhap");
                }
                else
                {
                    IsError = true;
                    ErrorMessage = phieuNhapRes?.ErrorMessage ?? chiTietPhieuNhapRes?.ErrorMessage ?? "Xóa thất bại.";
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

