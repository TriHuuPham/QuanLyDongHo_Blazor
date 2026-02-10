using DongHoBlazorApp.BL.Services.PhanLoai;
using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace DongHoBlazorApp.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhanLoaiController(IPhanLoaiService phanLoaiService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<BaseReponseModel>> GetPhanLoais()
        {
            try
            {
                var phanLoais = await phanLoaiService.GetPhanLoais();
                return Ok(new BaseReponseModel { Success = true, Data = phanLoais, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpGet("phanLoaiId")]
        public async Task<ActionResult<BaseReponseModel>> GetPhanLoaiById([FromQuery] int phanLoaiId)
        {
            try
            {
                var phanLoai = await phanLoaiService.GetPhanLoaiById(phanLoaiId);
                return Ok(new BaseReponseModel { Success = true, Data = phanLoai, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<BaseReponseModel>> CreatePhanLoai([FromBody] PhanLoaiModel phanLoaiModel)
        {
            try
            {
                var createdPhanLoai = await phanLoaiService.CreatePhanLoai(phanLoaiModel);
                return Ok(new BaseReponseModel { Success = true, Data = createdPhanLoai, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<ActionResult<BaseReponseModel>> DeletePhanLoai([FromQuery] int DeleteId)
        {
            try
            {
                var phanLoai = await phanLoaiService.GetPhanLoaiById(DeleteId);
                await phanLoaiService.DeletePhanLoai(phanLoai);
                return Ok(new BaseReponseModel { Success = true, Data = new object(), ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult<BaseReponseModel>> UpdatePhanLoai([FromBody] PhanLoaiModel phanLoaiModel)
        {
            try
            {
                var updatedPhanLoai = await phanLoaiService.UpdatePhanLoai(phanLoaiModel);
                return Ok(new BaseReponseModel { Success = true, Data = updatedPhanLoai, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new BaseReponseModel { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }
    }
}
