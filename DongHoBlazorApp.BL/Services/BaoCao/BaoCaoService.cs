using DongHoBlazorApp.BL.Reposities.BaoCao;
using DongHoBlazorApp.Model.Models;
using System.Threading.Tasks;

namespace DongHoBlazorApp.BL.Services.BaoCao
{
    public class BaoCaoService(IBaoCaoRepository baoCaoRepository) : IBaoCaoService
    {
        public Task<DashboardReportDto> GetDashboardReport(string filter)
        {
            return baoCaoRepository.GetDashboardReport(filter);
        }
    }
}
