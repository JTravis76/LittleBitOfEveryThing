
namespace WebApp.Utilities.ChartJS
{
    public class PieChartOptions : GlobalChartOptions
    {
        /// <summary>
        /// The percentage of the chart that is cut out of the middle.
        /// Default: 50 - for doughnut, 0 - for pie
        /// </summary>
        public int? cutoutPercentage { get; set; }

        /// <summary>
        /// Starting angle to draw arcs from.
        /// Default: -0.5 * Math.PI
        /// </summary>
        public int? rotation { get; set; }

        /// <summary>
        /// Sweep to allow arcs to cover.
        /// Default: 2 * Math.PI
        /// </summary>
        public int? circumference { get; set; }

    }
}