using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Data.Models;

namespace Business.Repositories
{
    public class GenreRepository : RepositoryBase, IGenreRepository
    {
       
        public Genre Create(GenreModel g)
        {
            var genre = new Genre() {Text = g.Text};
            dbContext.Genres.Add(genre);
            dbContext.SaveChanges();
            return genre;
        }

        public ICollection<Genre> FindorCreateGenres(IEnumerable<GenreModel> genres)
        {         
            if (genres == null || !genres.Any())
                return new List<Genre>();

            var resultGenres = new List<Genre>();

            var created = false;

            foreach (var g in genres)
            {
                Genre g1;
                if (g.ID == 0){
                    g1 = dbContext.Genres.SingleOrDefault( t => t.Text == g.Text);

                    if (g1 == default(Genre))
                        g1 = new Genre() {Text = g.Text} ;

                    created = true;
                }else{
                    g1 = dbContext.Genres.Find(g.ID);
                }
                
                
                resultGenres.Add(g1);
            }

            if (created)
                dbContext.SaveChanges();

            return resultGenres;
        }


    }
}