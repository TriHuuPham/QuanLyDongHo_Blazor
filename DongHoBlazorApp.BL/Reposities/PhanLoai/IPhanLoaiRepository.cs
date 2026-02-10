using DongHoBlazorApp.Model.Entities;

namespace DongHoBlazorApp.BL.Reposities.PhanLoai
{
    public interface IPhanLoaiRepository
    {
        Task<List<PhanLoaiModel>> GetPhanLoais();

        Task<PhanLoaiModel> GetPhanLoaiById(int id);

        Task<PhanLoaiModel> CreatePhanLoai(PhanLoaiModel phanLoaiModel);

        Task DeletePhanLoai(PhanLoaiModel phanLoaiModel);

        Task<PhanLoaiModel> UpdatePhanLoai(PhanLoaiModel phanLoaiModel);
    }
}
