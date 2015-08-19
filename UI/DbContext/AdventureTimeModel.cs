using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using UI.Models;
using Action = UI.Models.Action;
using UI.Models.Stats;

namespace UI.DbContext
{

    public class AdventureTimeDbContext : IdentityDbContext 
    {
       // Your context has been configured to use a 'AdventureTimeModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Data.AdventureTimeModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'AdventureTimeModel' 
        // connection string in the application configuration file.
        public AdventureTimeDbContext(): base("name=DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public IdentityUser userID;

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        //Core Tables
        public virtual DbSet<Story> Stories { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<Action> Actions { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        //Stats Tables
        public virtual DbSet<StoryStats> StoryStats { get; set; }
        public virtual DbSet<UserStoryStats> UserStoryStats { get; set; }
        public virtual DbSet<UserStoryEnding> UserStoryEndings { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Add(new ForeignKeyNamingConvention());
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Story>()
                .HasMany(t => t.Tags)                
                .WithMany();

            base.OnModelCreating(modelBuilder);
        }

        public bool AutoSeedMetadata { get; set; } = true;
        public override int SaveChanges()
        {
            // fix trackable entities
            var trackables = ChangeTracker.Entries<Metadata>();

            //if (userID == null)
            //    throw new NullReferenceException("DbContext Error: UserID must be injected or set prior to saving changes.");

            if (trackables != null & AutoSeedMetadata)
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

            try
            {
                return base.SaveChanges();
            } catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            

        }
    }
}