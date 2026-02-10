-------- Tạo Migration
dotnet ef migrations add AddTableNhaCungCapThuongHieu --project DongHoBlazorApp.Database --startup-project DongHoBlazorApp.ApiService
---------- Update Database
dotnet ef database update --project DongHoBlazorApp.Database --startup-project DongHoBlazorApp.ApiService