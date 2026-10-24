namespace WebApp.Api.Database
{
    using Microsoft.EntityFrameworkCore;
    using WebApp.Api.Entities;

#nullable disable

    public class DataContext: DbContext
    {
        [System.ComponentModel.Description("Collection of configurations")]
        public virtual DbSet<Configuration> Configurations { get; set; }


        [EntityGraphQL.Schema.GraphQLIgnore(EntityGraphQL.Schema.GraphQLIgnoreType.All)]
        public virtual DbSet<WikiContent> WikiContents { get; set; }

        [EntityGraphQL.Schema.GraphQLIgnore(EntityGraphQL.Schema.GraphQLIgnoreType.All)]
        public virtual DbSet<WikiMenu> WikiMenus { get; set; }



        [EntityGraphQL.Schema.GraphQLIgnore(EntityGraphQL.Schema.GraphQLIgnoreType.All)]
        public virtual DbSet<Package> Packages { get; set; }



        [EntityGraphQL.Authorization.GraphQLAuthorize("Admin")]
        [System.ComponentModel.Description("Collection of users")]
        public virtual DbSet<User> Users { get; set; }

        [System.ComponentModel.Description("Collection of roles")]
        public virtual DbSet<Role> Roles { get; set; }

        [System.ComponentModel.Description("Collection of users and their roles")]
        public virtual DbSet<UserRole> UserRoles { get; set; }




        //Parameterless version for Mocking in Unit-Tests
        public DataContext()
        {
        }

        public DataContext(DbContextOptions options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(e => e.HasKey("ApplicationName", "RoleName"));
            modelBuilder.Entity<UserRole>(e => e.HasKey("ApplicationName", "UserName", "RoleName"));
        }
    }
}
