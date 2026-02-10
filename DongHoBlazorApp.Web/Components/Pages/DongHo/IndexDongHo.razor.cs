using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace DongHoBlazorApp.Web.Components.Pages.DongHo
{
    public partial class IndexDongHo
    {
        [Inject]
        public ApiClient ApiClient { get; set; }
        public List<DongHoModel> DongHoModels { get; set; } = new();
        public List<PhanLoaiModel> PhanLoaiModels { get; set; } = new();
        public List<ThuongHieuModel> ThuongHieuModels { get; set; } = new();
        public int DeleteId { get; set; }
        public bool IsShowDeleteModal { get; set; } = false;
        public string DeleteErrorMessage { get; set; } = string.Empty;

        // Lọc và Tìm kiếm
        private string _searchText = string.Empty;
        public string SearchText 
        { 
            get => _searchText; 
            set { if (_searchText != value) { _searchText = value; CurrentPage = 1; } } 
        }

        private int _selectedBrandId = 0;
        public int SelectedBrandId 
        { 
            get => _selectedBrandId; 
            set { if (_selectedBrandId != value) { _selectedBrandId = value; CurrentPage = 1; } } 
        }

        private int _selectedCategoryId = 0;
        public int SelectedCategoryId 
        { 
            get => _selectedCategoryId; 
            set { if (_selectedCategoryId != value) { _selectedCategoryId = value; CurrentPage = 1; } } 
        }

        private string _selectedStatus = "all";
        public string SelectedStatus 
        { 
            get => _selectedStatus; 
            set { if (_selectedStatus != value) { _selectedStatus = value; CurrentPage = 1; } } 
        }

        // Phân trang
        public int CurrentPage { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 3;

        public List<DongHoModel> FilteredDongHoModels
        {
            get
            {
                var query = DongHoModels.AsQueryable();

                if (!string.IsNullOrEmpty(SearchText))
                {
                    query = query.Where(dh => (dh.TenDongHo != null && dh.TenDongHo.Contains(SearchText, StringComparison.OrdinalIgnoreCase)) || 
                                            dh.MaDongHo.ToString().Contains(SearchText));
                }

                if (SelectedBrandId != 0)
                {
                    query = query.Where(dh => dh.MaThuongHieu == SelectedBrandId);
                }

                if (SelectedCategoryId != 0)
                {
                    query = query.Where(dh => dh.MaPL == SelectedCategoryId);
                }

                if (SelectedStatus != "all")
                {
                    bool status = SelectedStatus == "active";
                    query = query.Where(dh => dh.TrangThai == status);
                }

                return query.ToList();
            }
        }

        public int TotalPages => (int)Math.Ceiling((double)FilteredDongHoModels.Count / ItemsPerPage);

        public void ChangePage(int page)
        {
            if (page >= 1 && page <= TotalPages)
            {
                CurrentPage = page;
                StateHasChanged();
            }
        }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                // Bắn tất cả các request cùng lúc để tối ưu thời gian phản hồi
                var taskDongHo = ApiClient.GetFromJsonAsync<BaseReponseModel>("/api/DongHo");
                var taskPhanLoai = ApiClient.GetFromJsonAsync<BaseReponseModel>("/api/PhanLoai");
                var taskThuongHieu = ApiClient.GetFromJsonAsync<BaseReponseModel>("/api/ThuongHieu");

                await Task.WhenAll(taskDongHo, taskPhanLoai, taskThuongHieu);

                var dongHoRes = await taskDongHo;
                var phanLoaiRes = await taskPhanLoai;
                var thuongHieuRes = await taskThuongHieu;

                if (dongHoRes != null && dongHoRes.Success)
                {
                    DongHoModels = JsonConvert.DeserializeObject<List<DongHoModel>>(dongHoRes.Data.ToString()) ?? new();
                }

                if (phanLoaiRes != null && phanLoaiRes.Success)
                {
                    PhanLoaiModels = JsonConvert.DeserializeObject<List<PhanLoaiModel>>(phanLoaiRes.Data.ToString()) ?? new();
                }

                if (thuongHieuRes != null && thuongHieuRes.Success)
                {
                    ThuongHieuModels = JsonConvert.DeserializeObject<List<ThuongHieuModel>>(thuongHieuRes.Data.ToString()) ?? new();
                }

                await base.OnInitializedAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task ToggleTrangThai(DongHoModel item)
        {
            item.TrangThai = !item.TrangThai;
            try
            {
                var res = await ApiClient.PutAsJsonAsync<BaseReponseModel, DongHoModel>("/api/DongHo", item);
                if (res != null && res.Success)
                {
                    StateHasChanged();
                }
                else
                {
                    item.TrangThai = !item.TrangThai; // Rollback
                    StateHasChanged();
                }
            }
            catch (Exception)
            {
                item.TrangThai = !item.TrangThai; // Rollback
                StateHasChanged();
            }
        }

        public void ShowDeleteModal(int id)
        {
            DeleteId = id;
            IsShowDeleteModal = true;
            DeleteErrorMessage = string.Empty;
        }

        public void CloseDeleteModal()
        {
            IsShowDeleteModal = false;
            DeleteErrorMessage = string.Empty;
        }
        public async Task ConfirmDelete()
        {
            try
            {
                var res = await ApiClient.DeleteAsync<BaseReponseModel>($"/api/DongHo?DeleteId={DeleteId}");
                if (res != null && res.Success)
                {
                    await OnInitializedAsync();
                    CloseDeleteModal();
                }
                else
                {
                    DeleteErrorMessage = res?.ErrorMessage ?? "Xóa thất bại.";
                }
            }
            catch (Exception ex)
            {
                DeleteErrorMessage = ex.Message;
            }
        }
    }
}
