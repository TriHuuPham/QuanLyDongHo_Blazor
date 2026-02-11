using DongHoBlazorApp.BL.Services.KhachHang;
using DongHoBlazorApp.BL.Services.LoaiThanhVien;
using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace DongHoBlazorApp.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController(IKhachHangService khachHangService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<BaseReponseModel>> GetKhachHangs()
        {
            try
            {
                var khachHangs = await khachHangService.GetKhachHangs();
                return Ok(new BaseReponseModel { Success = true, Data = khachHangs, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpGet("khachHangId")]
        public async Task<ActionResult<BaseReponseModel>> GetKhachHangById([FromQuery] int khachHangId)
        {
            try
            {
                var khachHang = await khachHangService.GetKhachHangById(khachHangId);
                return Ok(new BaseReponseModel { Success = true, Data = khachHang, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<BaseReponseModel>> CreateKhachHang([FromBody] KhachHangModel khachHangModel)
        {
            try
            {
                var createKhachHang = await khachHangService.CreateKhachHang(khachHangModel);
                return Ok(new BaseReponseModel { Success = true, Data = createKhachHang, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<ActionResult<BaseReponseModel>> DeleteKhachHang([FromQuery] int DeleteId)
        {
            try
            {
                var khachHang = await khachHangService.GetKhachHangById(DeleteId);
                await khachHangService.DeleteKhachHang(khachHang);
                return Ok(new BaseReponseModel { Success = true, Data = new object(), ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult<BaseReponseModel>> UpdateKhachHang([FromBody] KhachHangModel khachHangModel)
        {
            try
            {
                var updatedKhachHang = await khachHangService.UpdateKhachHang(khachHangModel);
                return Ok(new BaseReponseModel { Success = true, Data = updatedKhachHang, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }
    }
}
