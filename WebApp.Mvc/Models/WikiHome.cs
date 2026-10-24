using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Mvc.Models
{
    public class WikiHome
    {
        public bool InEditMode { get; set; }
        public bool IsContentEditor { get; set; }
        public string Markdown { get; set; } = string.Empty;

        public Entities.WikiContent WikiContent { get; set; }
    }
}