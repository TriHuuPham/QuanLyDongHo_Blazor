using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace DongHoBlazorApp.Web.Components.Pages.ThuongHieu
{
    public partial class IndexThuongHieu
    {
        [Inject]
        public ApiClient ApiClient { get; set; }
        public List<ThuongHieuModel> ThuongHieuModels { get; set; } = new();
        public int DeleteId { get; set; }
        public bool IsShowDeleteModal { get; set; } = false;
        public string DeleteErrorMessage { get; set; } = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var res = await ApiClient.GetFromJsonAsync<BaseReponseModel>("/api/ThuongHieu");
                if (res != null && res.Success)
                {
                    ThuongHieuModels = JsonConvert.DeserializeObject<List<ThuongHieuModel>>(res.Data.ToString());
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
                var res = await ApiClient.DeleteAsync<BaseReponseModel>($"/api/ThuongHieu?DeleteId={DeleteId}");
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
