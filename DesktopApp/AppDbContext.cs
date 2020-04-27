using DesktopApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DesktopApp
{
    public class AppDbContext: DbContext
    {
        
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<TrackInTheme> TrackInThemes { get; set; }
        
        
        private static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(MyLoggerFactory)
                .UseSqlite("Data Source=./sound.sqlite");
        }
    }
}