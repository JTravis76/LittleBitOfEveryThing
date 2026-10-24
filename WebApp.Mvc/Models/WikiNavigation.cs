using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Mvc.Models
{
    public class WikiNavigation
    {
        public IEnumerable<Entities.WikiMenu> MainMenu { get; set; }
        public bool IsContentEditor { get; set; }
    }
}