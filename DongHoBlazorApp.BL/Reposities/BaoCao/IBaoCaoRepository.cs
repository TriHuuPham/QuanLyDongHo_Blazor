using DongHoBlazorApp.Model.Models;
using System.Threading.Tasks;

namespace DongHoBlazorApp.BL.Reposities.BaoCao
{
    public interface IBaoCaoRepository
    {
        Task<DashboardReportDto> GetDashboardReport(string filter);
    }
}
