namespace Client_ui.Domain.DTO
{
    public class ChartData
    {
        public string Month { get; set; } = string.Empty;
        public int Count { get; set; }
    }
    public class ChartDataWithVolume
    {
        public string Month { get; set; } = string.Empty;
        public int Count { get; set; }
        public decimal Volume { get; set; }
    }
}
