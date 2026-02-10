using DongHoBlazorApp.BL.Services.DongHo;
using DongHoBlazorApp.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DongHoBlazorApp.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DongHoController(IDongHoService dongHoService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetDongHos()
        {
            try
            {
                var dongHos = await dongHoService.GetDongHos();
                return Ok(new { Success = true, Data = dongHos, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpGet("dongHoId")]
        public async Task<ActionResult> GetDongHoById([FromQuery] int dongHoId)
        {
            try
            {
                var dongHo = await dongHoService.GetDongHoById(dongHoId);
                return Ok(new { Success = true, Data = dongHo, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateDongHo([FromBody] Model.Entities.DongHoModel dongHoModel)
        {
            try
            {
                var createdDongHo = await dongHoService.CreateDongHo(dongHoModel);
                return Ok(new { Success = true, Data = createdDongHo, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteDongHo([FromQuery] int DeleteId)
        {
            try
            {
                var dongHo = await dongHoService.GetDongHoById(DeleteId);
                if (dongHo == null)
                {
                    return Ok(new { Success = false, Data = new object(), ErrorMessage = "Đồng hồ không tồn tại" });
                }
                await dongHoService.DeleteDongHo(dongHo);
                return Ok(new { Success = true, Data = new object(), ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDongHo([FromBody] DongHoModel dongHoModel)
        {
            try
            {
                var updatedDongHo = await dongHoService.UpdateDongHo(dongHoModel);
                return Ok(new { Success = true, Data = updatedDongHo, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }
    }
}
