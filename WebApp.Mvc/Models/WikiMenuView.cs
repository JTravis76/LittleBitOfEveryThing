using System.Collections.Generic;

namespace WebApp.Mvc.Models
{
    public class WikiMenuView
    {
        public List<Entities.WikiMenu> WikiMenu { get; set; }
        public List<Entities.WikiContent> WikiContent { get; set; }
    }
}