using DongHoBlazorApp.Model.Models;
using System.Threading.Tasks;

namespace DongHoBlazorApp.BL.Services.BaoCao
{
    public interface IBaoCaoService
    {
        Task<DashboardReportDto> GetDashboardReport(string filter);
    }
}
