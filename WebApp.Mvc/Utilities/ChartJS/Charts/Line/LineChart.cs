namespace WebApp.Utilities.ChartJS
{
    public class LineChart : ChartBase<LineChartOptions>
    {
        private readonly LineChartOptions chartOptions = new LineChartOptions();

        /// <summary>
        /// Gets the chart type.
        /// </summary>
        public override ChartType ChartType
        {
            get
            {
                return ChartType.line;
            }
        }

        /// <summary>
        /// Gets the chart configuration.
        /// </summary>
        public override LineChartOptions GlobalConfiguration
        {
            get
            {
                return chartOptions;
            }
        }
    }
}