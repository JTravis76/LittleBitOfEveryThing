using System.Collections.Generic;

namespace WebApp.Utilities.ChartJS
{
    /// <summary>
    /// The chart data.
    /// </summary>
    public class ChartData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartData"/> class.
        /// </summary>
        public ChartData()
        {
            this.Labels = new List<string>();
            this.Datasets = new List<ChartDataset>();
        }

        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        public List<string> Labels { get; set; }

        /// <summary>
        /// Gets or sets the datasets.
        /// </summary>
        public List<ChartDataset> Datasets { get; set; }

    }
}