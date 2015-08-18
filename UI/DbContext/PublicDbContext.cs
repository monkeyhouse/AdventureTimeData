using System.Data.Entity;
using UI.Models;

namespace UI.DbContext
{
    public class PublicDbContext :AdventureTimeModel
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tag>().Ignore(t => t.CreatedByUser);
            modelBuilder.Entity<Tag>().Ignore(t => t.ModifiedByUser);
            modelBuilder.Entity<Tag>().Ignore(t => t.CreatedBy);
            modelBuilder.Entity<Tag>().Ignore(t => t.ModifiedBy);
            modelBuilder.Entity<Tag>().Ignore(t => t.CreatedOn);
            modelBuilder.Entity<Tag>().Ignore(t => t.ModifiedOn);
        }
    }
}