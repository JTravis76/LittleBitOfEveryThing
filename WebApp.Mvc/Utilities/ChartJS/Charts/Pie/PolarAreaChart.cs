using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Utilities.ChartJS
{
    /// <summary>
    /// The Polar Area Chart
    /// </summary>
    public class PolarAreaChart : ChartBase<PieChartOptions>
    {
        private readonly PieChartOptions chartOptions = new PieChartOptions();

        /// <summary>
        /// Gets the chart type.
        /// </summary>
        public override ChartType ChartType
        {
            get
            {
                return ChartType.polarArea;
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