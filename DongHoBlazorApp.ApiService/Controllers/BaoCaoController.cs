using DongHoBlazorApp.BL.Services.BaoCao;
using DongHoBlazorApp.Model.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DongHoBlazorApp.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaoCaoController(IBaoCaoService baoCaoService) : ControllerBase
    {
        [HttpGet("Dashboard")]
        public async Task<ActionResult<DashboardReportDto>> GetDashboardReport([FromQuery] string filter = "7 ngày qua")
        {
            var report = await baoCaoService.GetDashboardReport(filter);
            return Ok(report);
        }
    }
}
