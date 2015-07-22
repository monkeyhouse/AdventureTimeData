using System;
using System.Linq;
using Data;
using Data.Models;
using Action = Data.Models.Action;

namespace Business.Repositories
{
    public class SegmentRepository : RepositoryBase,  ISegmentRepository, IDisposable, ICompletable
    {

        //private ModelFactory mFactory;
        public SegmentRepository(IModelFactory modelFactory)
        {
            //mFactory = modelFactory;
        }

        public SegmentModel CreateSegment(int storyId, int parentSegmentId, SegmentModel model)
        {
            var parent = dbContext.Segments.Find(parentSegmentId);
            //var segment = new Segment(){ Text = model.Body, IsEnding = model.IsEnding };
            
            //var actions = model.Leaders.Select(t => new Action()
            //    { Text = t.Text, Parent = parent, Child = segment}).ToList();
            //segment.Leaders = actions;
            //dbContext.Actions.AddRange(actions);
            //dbContext.Segments.Add(segment);
            // // is this required?
            //action.ChildId = segment.ID;
            //return mFactory.Create(segment);
            return null;            
        }

        public void Dispose()
        {
           if (dbContext != null)
               dbContext.Dispose();
        }

        public void Complete()
        {
            if (dbContext != null)
                dbContext.SaveChanges();
        }
    }
}