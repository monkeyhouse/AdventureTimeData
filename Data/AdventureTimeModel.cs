using System.Data.Entity.ModelConfiguration.Conventions;
using Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using Action = Data.Models.Action;


namespace Data
{

    public class AdventureTimeModel : IdentityDbContext 
    {
       // Your context has been configured to use a 'AdventureTimeModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Data.AdventureTimeModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'AdventureTimeModel' 
        // connection string in the application configuration file.
        public AdventureTimeModel(): base("name=DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public IdentityUser userID;        
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Story> Stories { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<Action> Actions { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Add(new ForeignKeyNamingConvention());
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Story>()
                .HasMany(t => t.Genres)                
                .WithMany();

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            // fix trackable entities
            var trackables = ChangeTracker.Entries<Metadata>();

            //if (userID == null)
            //    throw new NullReferenceException("DbContext Error: UserID must be injected or set prior to saving changes.");

            if (trackables != null)
            {
                // added
                foreach (var item in trackables.Where(t => t.State == EntityState.Added))
                {
                    item.Entity.CreatedOn = DateTime.Now;
                    item.Entity.CreatedBy = userID.Id;
                    item.Entity.ModifiedOn = DateTime.Now;
                    item.Entity.ModifiedBy = userID.Id;
                }
                // modified
                foreach (var item in trackables.Where(t => t.State == EntityState.Modified))
                {
                    item.Entity.ModifiedOn = DateTime.Now;
                    item.Entity.ModifiedBy = userID.Id;
                }
            }

            return base.SaveChanges();

        }
    }
}