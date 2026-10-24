using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApp.FW.DataContext
{
    public class WebAppContext : DbContext
    {
        public WebAppContext() : base("name=WISE")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<WebAppContext,
            // Infrastructure.Migrations.Configuration>("WISE"));

            //disable initializer
            Database.SetInitializer<WebAppContext>(null);

            //Disable Proxy Creation by default
            this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Entities.Configuration> Configurations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //== example how to set the float precision
            //modelBuilder.Entity<MyEntityClass>()
            //    .Property(e => e.ActualWsVolume)
            //    .HasPrecision(18, 2);

        }
    }
}