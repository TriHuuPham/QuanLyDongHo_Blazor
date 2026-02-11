using DongHoBlazorApp.BL.Services.LoaiThanhVien;
using DongHoBlazorApp.BL.Services.PhanLoai;
using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace DongHoBlazorApp.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiThanhVienController(ILoaiThanhVienService loaiThanhVienService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<BaseReponseModel>> GetLoaiThanhViens()
        {
            try
            {
                var loaiThanhViens = await loaiThanhVienService.GetLoaiThanhViens();
                return Ok(new BaseReponseModel { Success = true, Data = loaiThanhViens, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpGet("loaiThanhVienId")]
        public async Task<ActionResult<BaseReponseModel>> GetLoaiThanhVienById([FromQuery] int loaiThanhVienId)
        {
            try
            {
                var loaiThanhVien = await loaiThanhVienService.GetLoaiThanhVienById(loaiThanhVienId);
                return Ok(new BaseReponseModel { Success = true, Data = loaiThanhVien, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<BaseReponseModel>> CreateLoaiThanhVien([FromBody] LoaiThanhVienModel loaiThanhVienModel)
        {
            try
            {
                var createLoaiThanhVien = await loaiThanhVienService.CreateLoaiThanhVien(loaiThanhVienModel);
                return Ok(new BaseReponseModel { Success = true, Data = createLoaiThanhVien, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<ActionResult<BaseReponseModel>> DeleteLoaiThanhVien([FromQuery] int DeleteId)
        {
            try
            {
                var phanLoai = await loaiThanhVienService.GetLoaiThanhVienById(DeleteId);
                await loaiThanhVienService.DeleteLoaiThanhVien(phanLoai);
                return Ok(new BaseReponseModel { Success = true, Data = new object(), ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult<BaseReponseModel>> UpdatePhanLoai([FromBody] LoaiThanhVienModel loaiThanhVienModel)
        {
            try
            {
                var updatedPhanLoai = await loaiThanhVienService.UpdateLoaiThanhVien(loaiThanhVienModel);
                return Ok(new BaseReponseModel { Success = true, Data = updatedPhanLoai, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }
    }
}
