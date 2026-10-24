using System;

namespace WebApp.WkHtmlToPdf
{
    /// <summary>Represents PDF page margins (unit size is mm)</summary>
    public class PageMargins
    {
        /// <summary>Get or set top margin (in mm)</summary>
        public float? Top { get; set; }

        /// <summary>Get or set bottom margin (in mm)</summary>
        public float? Bottom { get; set; }

        /// <summary>Get or set left margin (in mm)</summary>
        public float? Left { get; set; }

        /// <summary>Get or set right margin (in mm)</summary>
        public float? Right { get; set; }
    }
}
