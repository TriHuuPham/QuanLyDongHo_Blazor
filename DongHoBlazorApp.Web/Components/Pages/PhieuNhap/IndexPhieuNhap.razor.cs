using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace DongHoBlazorApp.Web.Components.Pages.PhieuNhap
{
    public partial class IndexPhieuNhap
    {
        [Inject]
        public ApiClient ApiClient { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public List<PhieuNhapModel> PhieuNhaps { get; set; } = new();
        public List<ChiTietPhieuNhapModel> ChiTietPhieuNhapModels { get; set; } = new();
        public List<NhaCungCapModel> NhaCungCapModels { get; set; } = new();
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

        private int _selectedSuplierId = 0;
        public int SelectedSuplierId
        {
            get => _selectedSuplierId;
            set { if (_selectedSuplierId != value) { _selectedSuplierId = value; CurrentPage = 1; } }
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

        public List<PhieuNhapModel> FilteredPhieuNhapModels
        {
            get
            {
                var query = PhieuNhaps.AsQueryable();

                if (!string.IsNullOrEmpty(SearchText))
                {
                    query = query.Where(pn => pn.MaPhieuNhap.ToString().Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                            NhaCungCapModels.Any(ncc => ncc.MaNCC == pn.MaNCC && ncc.TenNCC.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));
                }

                if (SelectedSuplierId != 0)
                {
                    query = query.Where(pn => pn.MaNCC == SelectedSuplierId);
                }

                if (SelectedStatus != "all")
                {
                    string statusText = SelectedStatus switch
                    {
                        "pending" => "Chờ duyệt",
                        "approved" => "Đã duyệt",
                        "rejected" => "Từ chối",
                        _ => SelectedStatus
                    };
                    query = query.Where(pn => pn.TrangThaiNhap == statusText);
                }

                return query.ToList();
            }
        }

        public List<PhieuNhapModel> PaginatedPhieuNhaps => FilteredPhieuNhapModels
            .Skip((CurrentPage - 1) * ItemsPerPage)
            .Take(ItemsPerPage)
            .ToList();

        public int TotalPages => (int)Math.Ceiling((double)FilteredPhieuNhapModels.Count / ItemsPerPage);

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
                var taskPhieuNhap = ApiClient.GetFromJsonAsync<BaseReponseModel>("/api/PhieuNhap");
                var taskChiTietPhieuNhap = ApiClient.GetFromJsonAsync<BaseReponseModel>("/api/ChiTietPhieuNhap");
                var taskNhaCungCap = ApiClient.GetFromJsonAsync<BaseReponseModel>("/api/NhaCungCap");

                await Task.WhenAll(taskPhieuNhap, taskChiTietPhieuNhap, taskNhaCungCap);

                var phieuNhapRes = await taskPhieuNhap;
                var chiTietPhieuNhapRes = await taskChiTietPhieuNhap;
                var nhaCungCapRes = await taskNhaCungCap;

                if (phieuNhapRes != null && phieuNhapRes.Success)
                {
                    PhieuNhaps = JsonConvert.DeserializeObject<List<PhieuNhapModel>>(phieuNhapRes.Data.ToString()) ?? new();
                }

                if (chiTietPhieuNhapRes != null && chiTietPhieuNhapRes.Success)
                {
                    ChiTietPhieuNhapModels = JsonConvert.DeserializeObject<List<ChiTietPhieuNhapModel>>(chiTietPhieuNhapRes.Data.ToString()) ?? new();
                }

                if (nhaCungCapRes != null && nhaCungCapRes.Success)
                {
                    NhaCungCapModels = JsonConvert.DeserializeObject<List<NhaCungCapModel>>(nhaCungCapRes.Data.ToString()) ?? new();
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
                var phieuNhapRes = await ApiClient.DeleteAsync<BaseReponseModel>($"/api/PhieuNhap?DeleteId={DeleteId}");
                var chiTietPhieuNhapRes = await ApiClient.DeleteAsync<BaseReponseModel>($"/api/ChiTietPhieuNhap?DeleteId={DeleteId}");
                if (phieuNhapRes != null && phieuNhapRes.Success)
                {
                    await OnInitializedAsync();
                    //CloseDeleteModal();
                }
                else
                {
                    DeleteErrorMessage = phieuNhapRes?.ErrorMessage ?? "Xóa thất bại.";
                }

                if (chiTietPhieuNhapRes != null && chiTietPhieuNhapRes.Success)
                {
                    await OnInitializedAsync();
                    CloseDeleteModal();
                }
                else
                {
                    DeleteErrorMessage = chiTietPhieuNhapRes?.ErrorMessage ?? "Xóa thất bại.";
                }

            }
            catch (Exception ex)
            {
                DeleteErrorMessage = ex.Message;
            }
        }
    }
}
