using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace DongHoBlazorApp.Web.Components.Pages.KhoHang
{
    public partial class IndexKhoHang
    {
        [Inject]
        public ApiClient ApiClient { get; set; }

        public List<DongHoModel> DongHoModels { get; set; } = new();
        public List<PhanLoaiModel> PhanLoaiModels { get; set; } = new();
        public List<ThuongHieuModel> ThuongHieuModels { get; set; } = new();

        public string SearchText { get; set; } = string.Empty;
        public string SelectedStatus { get; set; } = "Tất cả";

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                // Lấy danh sách đồng hồ
                var resDH = await ApiClient.GetFromJsonAsync<BaseReponseModel>("/api/DongHo");
                if (resDH != null && resDH.Success)
                    DongHoModels = JsonConvert.DeserializeObject<List<DongHoModel>>(resDH.Data.ToString());

                // Lấy danh sách phân loại
                var resPL = await ApiClient.GetFromJsonAsync<BaseReponseModel>("/api/PhanLoai");
                if (resPL != null && resPL.Success)
                    PhanLoaiModels = JsonConvert.DeserializeObject<List<PhanLoaiModel>>(resPL.Data.ToString());

                // Lấy danh sách thương hiệu
                var resTH = await ApiClient.GetFromJsonAsync<BaseReponseModel>("/api/ThuongHieu");
                if (resTH != null && resTH.Success)
                    ThuongHieuModels = JsonConvert.DeserializeObject<List<ThuongHieuModel>>(resTH.Data.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading warehouse data: " + ex.Message);
            }
        }

        public List<DongHoModel> FilteredDongHoModels
        {
            get
            {
                var query = DongHoModels.AsQueryable();

                if (!string.IsNullOrEmpty(SearchText))
                {
                    query = query.Where(w => w.TenDongHo.Contains(SearchText, StringComparison.OrdinalIgnoreCase) || 
                                           w.MaDongHo.ToString().Contains(SearchText));
                }

                if (SelectedStatus == "Còn hàng")
                    query = query.Where(w => w.SoLuongTon > 10);
                else if (SelectedStatus == "Sắp hết")
                    query = query.Where(w => w.SoLuongTon > 0 && w.SoLuongTon <= 10);
                else if (SelectedStatus == "Hết hàng")
                    query = query.Where(w => w.SoLuongTon == 0);

                return query.ToList();
            }
        }

        public int TotalItems => DongHoModels.Sum(s => s.SoLuongTon);
        public int LowStockCount => DongHoModels.Count(c => c.SoLuongTon > 0 && c.SoLuongTon <= 10);
        public int OutOfStockCount => DongHoModels.Count(c => c.SoLuongTon == 0);
        public decimal TotalValue => DongHoModels.Sum(s => (decimal)s.SoLuongTon * s.GiaBan);

        public string GetCategoryName(int id) => PhanLoaiModels.FirstOrDefault(f => f.MaPL == id)?.TenPL ?? "N/A";
        public string GetBrandName(int id) => ThuongHieuModels.FirstOrDefault(f => f.MaThuongHieu == id)?.TenThuongHieu ?? "N/A";
    }
}
