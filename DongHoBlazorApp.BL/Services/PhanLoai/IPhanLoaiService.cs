using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Services.PhanLoai
{
    public interface IPhanLoaiService
    {
        Task<List<PhanLoaiModel>> GetPhanLoais();

        Task<PhanLoaiModel> GetPhanLoaiById(int maPL);

        Task<PhanLoaiModel> CreatePhanLoai(PhanLoaiModel phanLoaiModel);

        Task DeletePhanLoai(PhanLoaiModel phanLoaiModel);

        Task<PhanLoaiModel> UpdatePhanLoai(PhanLoaiModel phanLoaiModel);
    }
}
