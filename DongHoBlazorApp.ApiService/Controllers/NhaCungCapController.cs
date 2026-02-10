using DongHoBlazorApp.BL.Services.NhaCungCap;
using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace DongHoBlazorApp.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhaCungCapController(INhaCungCapService nhaCungCapService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<BaseReponseModel>> GetNhaCungCaps()
        {
            try
            {
                var nhaCungCaps = await nhaCungCapService.GetNhaCungCaps();
                return Ok(new BaseReponseModel { Success = true, Data = nhaCungCaps, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpGet("nhaCungCapId")]
        public async Task<ActionResult<BaseReponseModel>> GetNhaCungCapById([FromQuery] int nhaCungCapId)
        {
            try
            {
                var nhaCungCap = await nhaCungCapService.GetNhaCungCapById(nhaCungCapId);
                return Ok(new BaseReponseModel { Success = true, Data = nhaCungCap, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<BaseReponseModel>> CreateNhaCungCap([FromBody] NhaCungCapModel nhaCungCapModel)
        {
            try
            {
                var createdNhaCungCap = await nhaCungCapService.CreateNhaCungCap(nhaCungCapModel);
                return Ok(new BaseReponseModel { Success = true, Data = createdNhaCungCap, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<ActionResult<BaseReponseModel>> DeleteNhaCungCap([FromQuery] int DeleteId)
        {
            try
            {
                var nhaCungCap = await nhaCungCapService.GetNhaCungCapById(DeleteId);
                await nhaCungCapService.DeleteNhaCungCap(nhaCungCap);
                return Ok(new BaseReponseModel { Success = true, Data = new object(), ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult<BaseReponseModel>> UpdateNhaCungCap([FromBody] NhaCungCapModel nhaCungCapModel)
        {
            try
            {
                var updatedNhaCungCap = await nhaCungCapService.UpdateNhaCungCap(nhaCungCapModel);
                return Ok(new BaseReponseModel { Success = true, Data = updatedNhaCungCap, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }
    }
}
