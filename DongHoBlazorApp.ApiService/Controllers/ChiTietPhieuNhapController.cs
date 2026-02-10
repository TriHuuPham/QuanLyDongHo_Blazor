using DongHoBlazorApp.BL.Services.ChiTietPhieuNhap;
using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace DongHoBlazorApp.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietPhieuNhapController(IChiTietPhieuNhapService chiTietPhieuNhapService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<BaseReponseModel>> GetChiTietPhieuNhaps()
        {
            try
            {
                var phieuNhaps = await chiTietPhieuNhapService.GetChiTietPhieuNhaps();
                return Ok(new BaseReponseModel { Success = true, Data = phieuNhaps, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpGet("chiTietPhieuNhapId")]
        public async Task<ActionResult<BaseReponseModel>> GetChiTietPhieuNhapById([FromQuery] int chiTietPhieuNhapId)
        {
            try
            {
                var phieuNhap = await chiTietPhieuNhapService.GetChiTietPhieuNhapById(chiTietPhieuNhapId);
                return Ok(new BaseReponseModel { Success = true, Data = phieuNhap, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<BaseReponseModel>> CreateChiTietPhieuNhap([FromBody] ChiTietPhieuNhapModel chiTietPhieuNhapModel)
        {
            try
            {
                var createdChiTietPhieuNhap = await chiTietPhieuNhapService.CreateChiTietPhieuNhap(chiTietPhieuNhapModel);
                return Ok(new BaseReponseModel { Success = true, Data = createdChiTietPhieuNhap, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<ActionResult<BaseReponseModel>> DeleteChiTietPhieuNhap([FromQuery] int DeleteId)
        {
            try
            {
                var phieuNhap = await chiTietPhieuNhapService.GetChiTietPhieuNhapById(DeleteId);
                await chiTietPhieuNhapService.DeleteChiTietPhieuNhap(phieuNhap);
                return Ok(new BaseReponseModel { Success = true, Data = new object(), ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult<BaseReponseModel>> UpdateChiTietPhieuNhap([FromBody] ChiTietPhieuNhapModel chiTietPhieuNhapModel)
        {
            try
            {
                var updatedChiTietPhieuNhap = await chiTietPhieuNhapService.UpdateChiTietPhieuNhap(chiTietPhieuNhapModel);
                return Ok(new BaseReponseModel { Success = true, Data = updatedChiTietPhieuNhap, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }
    }
}
