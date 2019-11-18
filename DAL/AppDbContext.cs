using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL
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
                .UseSqlite("Data Source=/Users/marti/RiderProjects/SimpleSoundSystem/sound.sqlite");
        }
    }
}