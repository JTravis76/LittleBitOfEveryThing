using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ManyToMany
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=DB")
        {
            //disable initializer
            Database.SetInitializer<DataContext>(null);

            //Disable Proxy Creation by default
            this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<SectionPage> SectionPages { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //== example how to set the float precision
            //modelBuilder.Entity<MyEntityClass>()
            //    .Property(e => e.ActualWsVolume)
            //    .HasPrecision(18, 2);

        }
    }
}
