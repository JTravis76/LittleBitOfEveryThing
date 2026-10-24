namespace WebApp.Api.Models
{
#nullable disable

    public class Pagination<T>
    {
        /// <summary>
        /// current selected page
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// number of results to return for the page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// total count of pages
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// data results for the page
        /// </summary>
        public T Data { get; set; }
    }
}
