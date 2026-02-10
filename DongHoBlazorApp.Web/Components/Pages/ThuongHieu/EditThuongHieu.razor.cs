using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;

namespace DongHoBlazorApp.Web.Components.Pages.ThuongHieu
{
    public partial class EditThuongHieu
    {
        [Inject]
        public ApiClient ApiClient { get; set; }

        [SupplyParameterFromForm]
        public ThuongHieuModel ThuongHieuModel { get; set; } = new();
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public bool IsSuccess { get; set; } = false;
        public bool IsError { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;

        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }

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
                if (Id > 0)
                {
                    var res = await ApiClient.GetFromJsonAsync<BaseReponseModel>($"/api/ThuongHieu/thuongHieuId?thuongHieuId={Id}");
                    if (res != null && res.Success)
                    {
                        ThuongHieuModel = JsonConvert.DeserializeObject<ThuongHieuModel>(res.Data.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task ConfirmEditThuongHieu()
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
                        ThuongHieuModel.Logo = savedPath;
                    }
                    else
                    {
                        // Error occurred during file save
                        return;
                    }
                }

                ThuongHieuModel.MaThuongHieu = Id; // Ensure ID is set correctly
                var res = await ApiClient.PutAsJsonAsync<BaseReponseModel, ThuongHieuModel>("/api/ThuongHieu", ThuongHieuModel);
                if (res != null && res.Success)
                {
                    IsSuccess = true;
                    StateHasChanged();
                    await Task.Delay(2000); // Wait for 2 seconds to show success message
                    NavigationManager.NavigateTo("/ThuongHieu");
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
                var uploadsFolder = Path.Combine(WebHostEnvironment.WebRootPath, "images", "brands");
                Directory.CreateDirectory(uploadsFolder);

                // Generate unique filename
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(selectedFile.Name)}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                // Save file
                await using FileStream fs = new(filePath, FileMode.Create);
                await selectedFile.OpenReadStream(MaxFileSize).CopyToAsync(fs);

                // Return relative path
                return $"/images/brands/{fileName}";
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
