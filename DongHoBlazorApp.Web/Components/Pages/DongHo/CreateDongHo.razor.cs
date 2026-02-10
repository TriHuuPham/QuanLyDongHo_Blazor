using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;

namespace DongHoBlazorApp.Web.Components.Pages.DongHo
{
    public partial class CreateDongHo
    {
        [Inject]
        public ApiClient ApiClient { get; set; }

        [SupplyParameterFromForm]
        public DongHoModel DongHoModel { get; set; } = new();
        public List<PhanLoaiModel> PhanLoaiModels { get; set; } = new();
        public List<ThuongHieuModel> ThuongHieuModels { get; set; } = new();
        public List<NhaCungCapModel> NhaCungCapModels { get; set; } = new();

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        public bool IsSuccess { get; set; } = false;
        public bool IsError { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;

        private bool isImageUpload = false;
        private string uploadedFileName = string.Empty;
        private string uploadedFileSize = string.Empty;
        private string previewImageUrl = string.Empty;
        private IBrowserFile selectedFile = null;
        private const long MaxFileSize = 5 * 1024 * 1024; // 5MB


        protected override async Task OnInitializedAsync()
        {
            try
            {
                // Bắn tất cả các request cùng lúc để tối ưu thời gian phản hồi
                var taskPhanLoai = ApiClient.GetFromJsonAsync<BaseReponseModel>("/api/PhanLoai");
                var taskThuongHieu = ApiClient.GetFromJsonAsync<BaseReponseModel>("/api/ThuongHieu");
                var taskNhaCungCap = ApiClient.GetFromJsonAsync<BaseReponseModel>("/api/NhaCungCap");

                await Task.WhenAll(taskPhanLoai, taskThuongHieu, taskNhaCungCap);

                var phanLoai = await taskPhanLoai;
                var thuongHieu = await taskThuongHieu;
                var nhaCungCap = await taskNhaCungCap;

                if (phanLoai != null && phanLoai.Success)
                {
                    PhanLoaiModels = JsonConvert.DeserializeObject<List<PhanLoaiModel>>(phanLoai.Data.ToString()) ?? new();
                }

                if (thuongHieu != null && thuongHieu.Success)
                {
                    ThuongHieuModels = JsonConvert.DeserializeObject<List<ThuongHieuModel>>(thuongHieu.Data.ToString()) ?? new();
                }

                if (nhaCungCap != null && nhaCungCap.Success)
                {
                    NhaCungCapModels = JsonConvert.DeserializeObject<List<NhaCungCapModel>>(nhaCungCap.Data.ToString()) ?? new();
                }
                DongHoModel.TrangThai = true; // Default to active
                await base.OnInitializedAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task ConfirmCreateDongHo()
        {
            IsSuccess = false;
            IsError = false;
            ErrorMessage = string.Empty;

            try
            {
                // Save uploaded file if exists
                if (isImageUpload && selectedFile != null)
                {
                    var savedPath = await SaveUploadedFile();
                    if (!string.IsNullOrEmpty(savedPath))
                    {
                        DongHoModel.HinhAnh = savedPath;
                    }
                    else
                    {
                        // Error occurred during file save
                        return;
                    }
                }

                var res = await ApiClient.PostAsJsonAsync<BaseReponseModel, DongHoModel>("/api/DongHo", DongHoModel);
                if (res != null && res.Success)
                {
                    IsSuccess = true;
                    StateHasChanged();
                    await Task.Delay(2000); // Wait for 2 seconds to show success message
                    NavigationManager.NavigateTo("/DongHo");
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
        private async Task HandleFileSelected(InputFileChangeEventArgs e)
        {
            selectedFile = e.File;

            if (selectedFile != null)
            {
                // Validate file size
                if (selectedFile.Size > MaxFileSize)
                {
                    IsError = true;
                    ErrorMessage = "Kích thước file không được vượt quá 5MB.";
                    return;
                }

                // Validate file type
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(selectedFile.Name).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                {
                    IsError = true;
                    ErrorMessage = "Chỉ chấp nhận file JPG hoặc PNG.";
                    return;
                }

                uploadedFileName = selectedFile.Name;
                uploadedFileSize = $"{selectedFile.Size / 1024.0:F2} KB";

                // Generate preview
                try
                {
                    var buffer = new byte[selectedFile.Size];
                    await selectedFile.OpenReadStream(MaxFileSize).ReadAsync(buffer);
                    var base64 = Convert.ToBase64String(buffer);
                    previewImageUrl = $"data:{selectedFile.ContentType};base64,{base64}";
                }
                catch (Exception ex)
                {
                    IsError = true;
                    ErrorMessage = $"Lỗi khi tải file: {ex.Message}";
                }
                isImageUpload = true;
            }
        }

        private void RemoveUploadedFile()
        {
            selectedFile = null;
            uploadedFileName = string.Empty;
            uploadedFileSize = string.Empty;
            previewImageUrl = string.Empty;
        }

        private async Task<string> SaveUploadedFile()
        {
            if (selectedFile == null) return null;

            try
            {
                // Create directory if not exists
                var uploadsFolder = Path.Combine(WebHostEnvironment.WebRootPath, "images", "watches");
                Directory.CreateDirectory(uploadsFolder);

                // Generate unique filename
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(selectedFile.Name)}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                // Save file
                await using FileStream fs = new(filePath, FileMode.Create);
                await selectedFile.OpenReadStream(MaxFileSize).CopyToAsync(fs);

                // Return relative path
                return $"/images/watches/{fileName}";
            }
            catch (Exception ex)
            {
                IsError = true;
                ErrorMessage = $"Lỗi khi lưu file: {ex.Message}";
                return null;
            }
        }
    }
}
