namespace WebApp.Utilities.ChartJS
{
    /// <summary>
    /// The bar chart.
    /// </summary>
    public class BarChart : ChartBase<BarChartOptions>
    {
        private readonly BarChartOptions chartOptions = new BarChartOptions();

        /// <summary>
        /// Gets the chart type.
        /// </summary>
        public override ChartType ChartType
        {
            get
            {
                return ChartType.bar;
            }
        }

        /// <summary>
        /// Gets the chart configuration.
        /// </summary>
        public override BarChartOptions GlobalConfiguration
        {
            get
            {
                return chartOptions;
            }
        }
    }
}
