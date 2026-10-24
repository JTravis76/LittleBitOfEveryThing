using System.Collections.Generic;

namespace WebApp.Utilities.ChartJS
{
    /// <summary>
    /// The chart dataset.
    /// </summary>
    public class ChartDataset
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartDataset"/> class.
        /// </summary>
        public ChartDataset()
        {
            this.Data = new List<double>();
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public List<double> Data { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public string Label { get; set; }

        /*===============================================
         * Bar/Line/Pie Chart Options
         ================================================*/

        /// <summary>
        /// (Bar/Line/Pie) Gets or sets the background color.
        /// </summary>
        /// Use dynamic to allow passing of string or string[]
        public dynamic backgroundColor { get; set; }
        
        /// <summary>
        /// (Bar/Line/Pie) Gets or sets the border color.
        /// </summary>
        public dynamic borderColor { get; set; }

        /// <summary>
        /// (Bar/Line/Pie) Gets or sets the border width.
        /// </summary>
        public dynamic borderwidth { get; set; }

        /*===============================================
         * Bar/Pie Chart Options
         ================================================*/

        /// <summary>
        /// (Bar/Pie) Gets or sets the hover background color.
        /// </summary>
        public dynamic hoverBackgroundColor { get; set; }

        /// <summary>
        /// (Bar/Pie) Gets or sets the hover border color.
        /// </summary>
        public dynamic hoverBorderColor { get; set; }  
     
        /// <summary>
        /// (Bar/Pie) Border width of arc when hovered.
        /// </summary>
        public dynamic hoverBorderWidth { get; set; }

        /*===============================================
         * Bar/Line Chart Options
         ================================================*/
        /// <summary>
        /// (Bar/Line) The ID of the x axis to plot this dataset on
        /// </summary>
        public string xAxisID { get; set; }

        /// <summary>
        /// LineChart) The ID of the y axis to plot this dataset on
        /// </summary>
        public string yAxisID { get; set; }


        /*===============================================
         * Line Chart Options
         ================================================*/
        /// <summary>
        /// (Bar/Line) If true, fill the area under the line
        /// </summary>
        public bool? fill { get; set; }

        /// <summary>
        /// (Line) Bezier curve tension of the line. Set to 0 to draw straightlines.
        /// </summary>
        public double? lineTension { get; set; }

        /// <summary>
        /// (Line) Cap style of the line
        /// </summary>
        public string borderCapStyle { get; set; }

        /// <summary>
        /// (Line) Length and spacing of dashes.
        /// </summary>
        public int?[] borderDash { get; set; }

        /// <summary>
        /// (Line) Offset for line dashes.
        /// </summary>
        public int? borderDashOffset { get; set; }

        /// <summary>
        /// (Line) Line joint style.
        /// </summary>
        public string borderJoinStyle { get; set; }

        /// <summary>
        /// (Line) The border color for points.
        /// </summary>
        public dynamic pointBorderColor { get; set; }

        /// <summary>
        /// (Line) The fill color for points
        /// </summary>
        public dynamic pointBackgroundColor { get; set; }

        /// <summary>
        /// (Line) The width of the point border in pixels
        /// </summary>
        public dynamic pointBorderWidth { get; set; }

        /// <summary>
        /// (Line) The radius of the point shape. If set to 0, nothing is rendered.
        /// </summary>
        public dynamic pointRadius { get; set; }

        /// <summary>
        /// (Line) The radius of the point when hovered
        /// </summary>
        public dynamic pointHoverRadius { get; set; }

        /// <summary>
        /// (Line) The pixel size of the non-displayed point that reacts to mouse events
        /// </summary>
        public dynamic pointHitRadius { get; set; }

        /// <summary>
        /// (Line) Point background color when hovered
        /// </summary>
        public dynamic pointHoverBackgroundColor { get; set; }

        /// <summary>
        /// (Line) Point border color when hovered
        /// </summary>
        public dynamic pointHoverBorderColor { get; set; }

        /// <summary>
        /// (Line) Border width of point when hovered
        /// </summary>
        public dynamic pointHoverBorderWidth { get; set; }

        /// <summary>
        /// (Line) The style of point. 
        /// Options are 'circle', 'triangle', 'rect', 'rectRot', 'cross', 'crossRot', 'star', 'line', and 'dash'. 
        /// If the option is an image, that image is drawn on the canvas using drawImage.
        /// </summary>
        public dynamic pointStyle { get; set; }

    }
}