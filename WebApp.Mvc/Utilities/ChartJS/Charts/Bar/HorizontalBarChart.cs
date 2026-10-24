
namespace WebApp.Utilities.ChartJS
{
    /// <summary>
    /// The horizontal bar.
    /// </summary>
    public class HorizontalBarChart : ChartBase<BarChartOptions>
    {
        private readonly BarChartOptions chartOptions = new BarChartOptions();

        /// <summary>
        /// Gets the chart type.
        /// </summary>
        public override ChartType ChartType
        {
            get
            {
                return ChartType.horizontalBar;
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