using Microsoft.EntityFrameworkCore;
using WebAppFunction.Api.Entities;

namespace WebAppFunction.Api.Database
{
  public class DataContext : DbContext
  {
    public virtual DbSet<DynamicConfigurations> Configuration { get; set; }
    public virtual DbSet<GrantorType> GrantorTypes { get; set; }
    public virtual DbSet<Document> Documents { get; set; }


    public ROWContext(DbContextOptions options) : base(options)
    {
      this.ChangeTracker.LazyLoadingEnabled = false;
    }

    // Used for Mocking and Unit Testing
    public DataContext() : base()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies(false);
      base.OnConfiguring(optionsBuilder);
    }
  }
}
