using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace DongHoBlazorApp.Web.Components.Pages.LoaiThanhVien
{
    public partial class IndexLoaiThanhVien
    {
        [Inject]
        public ApiClient ApiClient { get; set; }
        public List<LoaiThanhVienModel> LoaiThanhVienModels { get; set; } = new();
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

        private string _selectedStatus = "all";
        public string SelectedStatus
        {
            get => _selectedStatus;
            set { if (_selectedStatus != value) { _selectedStatus = value; CurrentPage = 1; } }
        }

        // Phân trang
        public int CurrentPage { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 3;

        public List<LoaiThanhVienModel> FilteredLoaiThanhVienModels
        {
            get
            {
                var query = LoaiThanhVienModels.AsQueryable();

                if (!string.IsNullOrEmpty(SearchText))
                {
                    query = query.Where(dh => (dh.TenLoaiTV != null && dh.TenLoaiTV.Contains(SearchText, StringComparison.OrdinalIgnoreCase)) ||
                                            dh.MaLoaiTV.ToString().Contains(SearchText));
                }

                if (SelectedStatus != "all")
                {
                    bool status = SelectedStatus == "true";
                    query = query.Where(dh => dh.TrangThai == status);
                }

                return query.ToList();
            }
        }

        public List<LoaiThanhVienModel> PagedLoaiThanhVienModels
        {
            get
            {
                return FilteredLoaiThanhVienModels
                    .Skip((CurrentPage - 1) * ItemsPerPage)
                    .Take(ItemsPerPage)
                    .ToList();
            }
        }

        public int TotalPages => (int)Math.Ceiling((double)FilteredLoaiThanhVienModels.Count / ItemsPerPage);

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
                var res = await ApiClient.GetFromJsonAsync<BaseReponseModel>("/api/LoaiThanhVien");
                if (res != null && res.Success)
                {
                    LoaiThanhVienModels = JsonConvert.DeserializeObject<List<LoaiThanhVienModel>>(res.Data.ToString());
                }
                await base.OnInitializedAsync();
            }
            catch (Exception)
            {
                throw;
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
                var res = await ApiClient.DeleteAsync<BaseReponseModel>($"/api/LoaiThanhVien?DeleteId={DeleteId}");
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
