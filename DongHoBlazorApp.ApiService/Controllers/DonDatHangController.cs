using DongHoBlazorApp.BL.Services.DonDHang;
using DongHoBlazorApp.Model.Entities;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace DongHoBlazorApp.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonDatHangController(IDonDatHangService donDatHangService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetDonDatHangs([FromQuery] string? searchTerm, [FromQuery] string? status, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var donDatHangs = await donDatHangService.GetDonDatHangs(searchTerm, status, page, pageSize);
                return Ok(new { Success = true, Data = donDatHangs, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpGet("maDonDH")]
        public async Task<ActionResult> GetDonDatHangById([FromQuery] int maDonDH)
        {
            try
            {
                var donDatHang = await donDatHangService.GetDonDatHangById(maDonDH);
                return Ok(new { Success = true, Data = donDatHang, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpGet("detail/{maDonDH}")]
        public async Task<ActionResult> GetDonDatHangDetail(int maDonDH)
        {
            try
            {
                var detail = await donDatHangService.GetDonDatHangDetail(maDonDH);
                return Ok(new { Success = true, Data = detail, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpGet("stats")]
        public async Task<ActionResult> GetDonDatHangStats()
        {
            try
            {
                var stats = await donDatHangService.GetDonDatHangStats();
                return Ok(new { Success = true, Data = stats, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new { Success = false, Data = new object(), ErrorMessage = ex.Message });
            }
        }

        [HttpPost("complex")]
        public async Task<ActionResult> CreateDonDatHangComplex([FromBody] CreateOrderRequestDTO request)
        {
            try
            {
                var result = await donDatHangService.CreateDonDatHangComplex(request);
                return Ok(new { Success = true, Data = result, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new { Success = false, Data = 0, ErrorMessage = ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDonDatHang([FromBody] DonDatHangModel donDatHangModel)
        {
            try
            {
                var result = await donDatHangService.UpdateDonDatHang(donDatHangModel);
                return Ok(new { Success = true, Data = result, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new { Success = false, Data = 0, ErrorMessage = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteDonDatHang([FromQuery] int maDonDH)
        {
            try
            {
                var result = await donDatHangService.DeleteDonDatHang(maDonDH);
                return Ok(new { Success = true, Data = result, ErrorMessage = "" });
            }
            catch (Exception ex)
            {
                return Ok(new { Success = false, Data = 0, ErrorMessage = ex.Message });
            }
        }
    }
}
