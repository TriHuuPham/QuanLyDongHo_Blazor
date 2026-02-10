using DongHoBlazorApp.BL.Services.BinhLuan;
using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace DongHoBlazorApp.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BinhLuanController(IBinhLuanService binhLuanService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<BaseReponseModel>> GetBinhLuans()
        {
            try
            {
                var binhLuans = await binhLuanService.GetBinhLuans();
                return Ok(new BaseReponseModel { Success = true, Data = binhLuans, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpGet("binhLuanId")]
        public async Task<ActionResult<BaseReponseModel>> GetBinhLuanById([FromQuery] int binhLuanId)
        {
            try
            {
                var binhLuan = await binhLuanService.GetBinhLuanById(binhLuanId);
                return Ok(new BaseReponseModel { Success = true, Data = binhLuan, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<BaseReponseModel>> CreateBinhLuan([FromBody] BinhLuanModel binhLuanModel)
        {
            try
            {
                var createdBinhLuan = await binhLuanService.CreateBinhLuan(binhLuanModel);
                return Ok(new BaseReponseModel { Success = true, Data = createdBinhLuan, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<ActionResult<BaseReponseModel>> DeleteBinhLuan([FromQuery] int DeleteId)
        {
            try
            {
                var binhLuan = await binhLuanService.GetBinhLuanById(DeleteId);
                await binhLuanService.DeleteBinhLuan(binhLuan);
                return Ok(new BaseReponseModel { Success = true, Data = new object(), ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult<BaseReponseModel>> UpdateBinhLuan([FromBody] BinhLuanModel binhLuanModel)
        {
            try
            {
                var updatedBinhLuan = await binhLuanService.UpdateBinhLuan(binhLuanModel);
                return Ok(new BaseReponseModel { Success = true, Data = updatedBinhLuan, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }
    }
}
