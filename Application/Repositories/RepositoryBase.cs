using System;
using Data;
using Microsoft.Practices.Unity;

namespace Business.Repositories
{
    public class RepositoryBase : ICompletable, IDisposable
    {
        protected AdventureTimeModel dbContext;

        [Dependency]
        public AdventureTimeModel DbContext {
            get { return dbContext; }
            set { dbContext = value; }
        }

        public void Complete()
        {
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            if (dbContext != null)
                dbContext.Dispose();

            dbContext = null;
        }
    }
}