using Client_ui.Domain.DTO;

namespace Client_ui.Service
{
    public interface IChartService
    {
        Task<IEnumerable<ChartData>> GetAllWorkoutsInMonth(Guid userId);
        Task<IEnumerable<ChartDataWithVolume>> GetAllWorkoutsInMonthWithVolume(Guid userId);

    }
}
