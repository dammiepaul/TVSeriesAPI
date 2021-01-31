using Microsoft.EntityFrameworkCore;
using TVSeriesAPI.Models;

namespace TVSeriesAPI
{
    public class MovieSeriesContext : DbContext
    {
        public MovieSeriesContext(DbContextOptions<MovieSeriesContext> options) : base(options)
        {
            
        }

        //DbSets
        public DbSet<Character> Characters { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
