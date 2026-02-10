using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace DongHoBlazorApp.Web.Components.Pages.PhieuNhap
{
    public partial class CreatePhieuNhap
    {
        [Inject]
        public ApiClient ApiClient { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public PhieuNhapModel NewPhieuNhap { get; set; } = new () { NgayNhap = DateTime.Now, TrangThaiNhap = "Chờ duyệt", TrangThai = true };
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
                var taskNhaCungCap = ApiClient.GetFromJsonAsync<BaseReponseModel>("/api/NhaCungCap");
                var taskDongHo = ApiClient.GetFromJsonAsync<BaseReponseModel>("/api/DongHo");

                await Task.WhenAll(taskNhaCungCap, taskDongHo);

                var nhaCungCapRes = await taskNhaCungCap;
                var dongHoRes = await taskDongHo;

                if (nhaCungCapRes != null && nhaCungCapRes.Success)
                {
                    NhaCungCapModels = JsonConvert.DeserializeObject<List<NhaCungCapModel>>(nhaCungCapRes.Data.ToString()) ?? new();
                }

                if (dongHoRes != null && dongHoRes.Success)
                {
                    DongHoModels = JsonConvert.DeserializeObject<List<DongHoModel>>(dongHoRes.Data.ToString()) ?? new();
                }

                // Add initial row
                AddRow();
            }
            catch (Exception ex)
            {
                IsError = true;
                ErrorMessage = ex.Message;
            }
        }

        public void AddRow()
        {
            ChiTietItems.Add(new ChiTietPhieuNhapModel { SoLuongNhap = 1, DonGiaNhap = 0 });
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
        public decimal GrandTotal => ChiTietItems.Sum(i => i.SoLuongNhap * i.DonGiaNhap);

        public async Task HandleSubmit()
        {
            try
            {
                IsError = false;
                IsSuccess = false;

                if (NewPhieuNhap.MaNCC == 0)
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

                NewPhieuNhap.TongTien = GrandTotal;

                // Create PhieuNhap
                var phieuNhapRes = await ApiClient.PostAsJsonAsync<BaseReponseModel, PhieuNhapModel>("/api/PhieuNhap", NewPhieuNhap);

                if (phieuNhapRes != null && phieuNhapRes.Success)
                {
                    // Get the ID of the created PhieuNhap. Assuming the API returns it in Data.
                    int newMaPhieuNhap = int.Parse(phieuNhapRes.Data.ToString());

                    // Create ChiTietPhieuNhap items
                    foreach (var item in ChiTietItems)
                    {
                        item.MaPhieuNhap = newMaPhieuNhap;
                        await ApiClient.PostAsJsonAsync<BaseReponseModel, ChiTietPhieuNhapModel>("/api/ChiTietPhieuNhap", item);
                    }

                    IsSuccess = true;
                    await Task.Delay(2000);
                    NavigationManager.NavigateTo("/PhieuNhap");
                }
                else
                {
                    IsError = true;
                    ErrorMessage = phieuNhapRes?.ErrorMessage ?? "Không thể tạo phiếu nhập.";
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                ErrorMessage = ex.Message;
            }
        }

        public void ResetForm()
        {
            NewPhieuNhap = new PhieuNhapModel { NgayNhap = DateTime.Now, TrangThaiNhap = "Chờ duyệt", TrangThai = true };
            ChiTietItems = new List<ChiTietPhieuNhapModel>();
            AddRow();
            IsError = false;
            IsSuccess = false;
        }
    }
}

