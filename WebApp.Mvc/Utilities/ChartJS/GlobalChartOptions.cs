namespace WebApp.Utilities.ChartJS
{
    /// <summary>
    /// The global chart options.
    /// </summary>
    public abstract class GlobalChartOptions 
    {
        public GlobalChartOptions()
        {
            this.Title = new Title();
            this.Legend = new Legend();
            //this.Scales = new Scales();
            this.Animation = new Animation();
        }

        public Title Title { get; set; }
        public Legend Legend { get; set; }
        //public Scales Scales { get; set; }
        public Animation Animation { get; set; }

        /// <summary>
        /// Default font color for all text.
        /// Default: #666
        /// </summary>
        public System.Drawing.Color? defaultFontColor { get; set; }

        /// <summary>
        /// Default font family for all text.
        /// Default: "'Helvetica Neue', 'Helvetica', 'Arial', sans-serif"
        /// </summary>
        public string defaultFontFamily { get; set; }

        /// <summary>
        /// Default font size (in px) for text. Does not apply to radialLinear scale point labels.
        /// Default: 12
        /// </summary>
        public int? defaultFontSize { get; set; }

        /// <summary>
        /// Default font style. Does not apply to tooltip title or footer. Does not apply to chart title.
        /// Default: normal
        /// </summary>
        public string defaultFontStyle { get; set; }

        /// <summary>
        /// Resizes when the canvas container does.
        /// Default: true
        /// </summary>
        public bool? responsive { get; set; }

        /// <summary>
        /// Duration in milliseconds it takes to animate to new size after a resize event.
        /// Default: 0
        /// </summary>
        public int? responsiveAnimationDuration { get; set; }

        /// <summary>
        /// Maintain the original canvas aspect ratio (width / height) when resizing.
        /// Default: true
        /// </summary>
        public bool? maintainAspectRatio { get; set; }
    }

    //Title Class
    public class Title
    {
        public bool? Display { get; set; }
        public string Position { get; set; }
        public bool? fullWidth { get; set; }
        public int? fontSize { get; set; }
        public string fontFamily { get; set; }
        public System.Drawing.Color? fontColor { get; set; }
        public string fontStyle { get; set; }
        public int? Padding { get; set; }
        public string Text { get; set; }
    }

    //Legend Class
    public class Legend
    {
        public bool? Display { get; set; }
        public string Position { get; set; }
        public bool? fullWidth { get; set; }
    }

    //Scales Class
    public class Scales
    {
        public Scales()
        {
            this.xAxes = new xAxes();
            this.yAxes = new yAxes();
        }
        public xAxes xAxes { get; set; }
        public yAxes yAxes { get; set; }
    }

    public class xAxes
    {
        public bool? Stacked { get; set; }
    }

    public class yAxes
    {
        public bool? Stacked { get; set; }
    }

    public class Animation
    {
        /// <summary>
        /// The number of milliseconds an animation takes.
        /// Default: 1000
        /// </summary>
        public int? duration { get; set; }

        /// <summary>
        /// Easing function to use.
        /// Default: "easeOutQuart"
        /// </summary>
        public string easing { get; set; }

        /*======================================
         * Pie/Doughnut
         =======================================*/
        /// <summary>
        /// (Pie/Doughnut) Will animate the rotation of the chart.
        /// Default: true
        /// </summary>
        public bool? animateRotate { get; set; }

        /// <summary>
        /// (Pie/Doughnut) Will animate scaling the Doughnut from the centre.
        /// Default: false
        /// </summary>
        public bool? animateScale { get; set; }
    }
}
