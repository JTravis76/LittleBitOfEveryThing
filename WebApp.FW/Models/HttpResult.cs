namespace WebApp.FW.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Exception { get; set; } = string.Empty;



        public static explicit operator HttpResult(System.Web.Http.ModelBinding.ModelError modelError)
        {
            return new HttpResult
            {
                ErrorMessage = modelError.ErrorMessage,
                Exception = modelError.Exception?.Message
            };
        }
    }


}