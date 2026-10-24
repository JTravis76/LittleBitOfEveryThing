using System;

namespace WebApp.WkHtmlToPdf
{
    /// <summary>
    /// The exception that is thrown when WkHtmlToPdf process retruns non-zero error exit code
    /// </summary>
    public class WkHtmlToPdfException : Exception
    {
        /// <summary>Get WkHtmlToPdf process error code</summary>
        public int ErrorCode { get; private set; }

        public WkHtmlToPdfException(int errCode, string message)
          : base(string.Format("{0} (exit code: {1})", (object)message, (object)errCode))
        {
            this.ErrorCode = errCode;
        }
    }
}
