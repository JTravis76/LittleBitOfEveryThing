//using System;
//using System.Data.Entity;
//using System.Linq;
//using WebApp.Mvc.Entities;

//namespace WebApp.Mvc.DataContext
//{
//    public class WikiContext : DbContext
//    {
//        public WikiContext()
//            : base("name=WikiContext")
//        {
//        }

//        public virtual DbSet<WikiContent> WikiContents { get; set; }

//        public virtual DbSet<WikiMenu> WikiMenus { get; set; }
//    }
//}