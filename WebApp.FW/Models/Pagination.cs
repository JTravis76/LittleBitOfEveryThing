using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.FW.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Pagination<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<T> Data { get; set; }
    }
}