using DongHoBlazorApp.BL.Services.PhieuNhap;
using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace DongHoBlazorApp.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuNhapController(IPhieuNhapService phieuNhapService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<BaseReponseModel>> GetPhieuNhaps()
        {
            try
            {
                var phieuNhaps = await phieuNhapService.GetPhieuNhaps();
                return Ok(new BaseReponseModel { Success = true, Data = phieuNhaps, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpGet("phieuNhapId")]
        public async Task<ActionResult<BaseReponseModel>> GetPhieuNhapById([FromQuery] int phieuNhapId)
        {
            try
            {
                var phieuNhap = await phieuNhapService.GetPhieuNhapById(phieuNhapId);
                return Ok(new BaseReponseModel { Success = true, Data = phieuNhap, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<BaseReponseModel>> CreatePhieuNhap([FromBody] PhieuNhapModel phieuNhapModel)
        {
            try
            {
                var createdPhieuNhap = await phieuNhapService.CreatePhieuNhap(phieuNhapModel);
                return Ok(new BaseReponseModel { Success = true, Data = createdPhieuNhap, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<ActionResult<BaseReponseModel>> DeletePhieuNhap([FromQuery] int DeleteId)
        {
            try
            {
                var phieuNhap = await phieuNhapService.GetPhieuNhapById(DeleteId);
                await phieuNhapService.DeletePhieuNhap(phieuNhap);
                return Ok(new BaseReponseModel { Success = true, Data = new object(), ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult<BaseReponseModel>> UpdatePhieuNhap([FromBody] PhieuNhapModel phieuNhapModel)
        {
            try
            {
                var updatedPhieuNhap = await phieuNhapService.UpdatePhieuNhap(phieuNhapModel);
                return Ok(new BaseReponseModel { Success = true, Data = updatedPhieuNhap, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }
    }
}
