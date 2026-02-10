using DongHoBlazorApp.BL.Services.ThuongHieu;
using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace DongHoBlazorApp.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThuongHieuController(IThuongHieuService thuongHieuService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<BaseReponseModel>> GetThuongHieus()
        {
            try
            {
                var thuongHieus = await thuongHieuService.GetThuongHieus();
                return Ok(new BaseReponseModel { Success = true, Data = thuongHieus, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpGet("thuongHieuId")]
        public async Task<ActionResult<BaseReponseModel>> GetThuongHieuById([FromQuery] int thuongHieuId)
        {
            try
            {
                var thuongHieu = await thuongHieuService.GetThuongHieuById(thuongHieuId);
                return Ok(new BaseReponseModel { Success = true, Data = thuongHieu, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<BaseReponseModel>> CreateThuongHieu([FromBody] ThuongHieuModel thuongHieuModel)
        {
            try
            {
                var createdThuongHieu = await thuongHieuService.CreateThuongHieu(thuongHieuModel);
                return Ok(new BaseReponseModel { Success = true, Data = createdThuongHieu, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }


        [HttpDelete]
        public async Task<ActionResult<BaseReponseModel>> DeleteThuongHieu([FromQuery] int DeleteId)
        {
            try
            {
                var thuongHieu = await thuongHieuService.GetThuongHieuById(DeleteId);
                await thuongHieuService.DeleteThuongHieu(thuongHieu);
                return Ok(new BaseReponseModel { Success = true, Data = new object(), ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }


        [HttpPut]
        public async Task<ActionResult<BaseReponseModel>> UpdateThuongHieu([FromBody] ThuongHieuModel thuongHieuModel)
        {
            try
            {
                var updatedThuongHieu = await thuongHieuService.UpdateThuongHieu(thuongHieuModel);
                return Ok(new BaseReponseModel { Success = true, Data = updatedThuongHieu, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }
    }
}
