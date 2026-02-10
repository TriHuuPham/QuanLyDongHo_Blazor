using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace DongHoBlazorApp.Web.Components.Pages.PhanLoai
{
    public partial class IndexPhanLoai
    {
        [Inject]
        public ApiClient ApiClient { get; set; }
        public List<PhanLoaiModel> PhanLoaiModels { get; set; } = new();
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var res = await ApiClient.GetFromJsonAsync<BaseReponseModel>("/api/PhanLoai");
                if (res != null && res.Success)
                {
                    PhanLoaiModels = JsonConvert.DeserializeObject<List<PhanLoaiModel>>(res.Data.ToString());
                }
                await base.OnInitializedAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int DeleteId { get; set; }
        public bool IsShowDeleteModal { get; set; } = false;
        public string DeleteErrorMessage { get; set; } = string.Empty;

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
                var res = await ApiClient.DeleteAsync<BaseReponseModel>($"/api/PhanLoai?DeleteId={DeleteId}");
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
