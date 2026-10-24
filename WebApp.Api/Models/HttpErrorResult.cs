namespace WebApp.Api.Models
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Missing XML Comment", "CS1591")]
    public class HttpErrorResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string? Exception { get; set; } = string.Empty;


        public static explicit operator HttpErrorResult(Microsoft.AspNetCore.Mvc.ModelBinding.ModelError modelError)
        {
            return new HttpErrorResult
            {
                ErrorMessage = modelError.ErrorMessage,
                Exception = modelError.Exception?.Message
            };
        }
    }
}
