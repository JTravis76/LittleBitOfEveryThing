namespace WebApp.Utilities.ChartJS
{
    /// <summary>
    /// The chart base.
    /// </summary>
    public abstract class ChartBase<TGlobalChartOptions>
        where TGlobalChartOptions : GlobalChartOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartBase"/> class.
        /// </summary>
        protected ChartBase()
        {
            this.DataSource = new ChartData();
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public ChartData DataSource { get; set; }

        /// <summary>
        /// Gets the chart type.
        /// </summary>
        public abstract ChartType ChartType { get; }

        /// <summary>
        /// Gets the chart configuration.
        /// </summary>
        public abstract TGlobalChartOptions GlobalConfiguration { get; }

    }
}