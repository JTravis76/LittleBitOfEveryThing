using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Mvc.Models
{
    public class SaleOrderDetail
    {
        public int Year { get; set; }
        public decimal SumOfLineTotal { get; set; } = 0;
    }
}