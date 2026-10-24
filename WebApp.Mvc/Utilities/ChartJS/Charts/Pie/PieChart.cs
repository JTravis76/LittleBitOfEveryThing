namespace WebApp.Utilities.ChartJS
{
    /// <summary>
    /// The pie chart.
    /// </summary>
    public class PieChart : ChartBase<PieChartOptions>
    {
        private readonly PieChartOptions chartOptions = new PieChartOptions();

        /// <summary>
        /// Gets the chart type.
        /// </summary>
        public override ChartType ChartType
        {
            get
            {
                return ChartType.pie;
            }
        }

        /// <summary>
        /// Gets the chart configuration.
        /// </summary>
        public override PieChartOptions GlobalConfiguration
        {
            get
            {
                return chartOptions;
            }
        }
    }
}