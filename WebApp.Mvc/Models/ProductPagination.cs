using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Mvc.Models
{
    public class ProductPagination
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public string Search { get; set; }


        public List<DataContext.Product> Products { get; set; }
    }
}