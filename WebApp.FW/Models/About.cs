using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.FW.Models
{
    public class About
    {
        public string Version { get; set; } = String.Empty;

        public DateTime VersionDate { get; set; } = DateTime.UtcNow;
    }
}