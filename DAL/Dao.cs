using System.Collections.Generic;
using System.Linq;
using Domain;

namespace DAL
{
    public class Dao
    {
        public void AddTheme(Theme theme)
        {
            using (var ctx = new AppDbContext())
            {
                ctx.Themes.Add(new Theme(
                )
                {
                    Name = theme.Name
                });

                ctx.SaveChanges();
            }
        }

        public void AddTrack(Track track)
        {
            using (var ctx = new AppDbContext())
            {
                ctx.Tracks.Add(new Track()
                {
                    Name = track.Name,
                    FilePath = track.FilePath
                });
                ctx.SaveChanges();
            }
        }

        public void AddTrackInTheme(TrackInTheme trackInTheme)
        {
            using (var ctx = new AppDbContext())
            {
                ctx.TrackInThemes.Add(new TrackInTheme()
                {
                    TrackId = trackInTheme.TrackId,
                    ThemeId = trackInTheme.ThemeId
                });

                ctx.SaveChanges();
            }
        }

        public List<Theme> GetAllThemes()
        {

            using (var ctx = new AppDbContext())
            {
                List<Theme> themes = new List<Theme>();
                foreach (var theme in ctx.Themes)
                {
                    themes.Add(theme);
                }

                return themes;
            }
        }
        public List<Track> GetAllTracks()
        {

            using (var ctx = new AppDbContext())
            {
                List<Track> tracks = new List<Track>();
                foreach (var theme in ctx.Tracks)
                {
                    tracks.Add(theme);
                }

                return tracks;
            }
        }

        public void DeleteTheme(int targetId)
        {
            using (var ctx = new AppDbContext())
            {
                var itemToRemove = ctx.Themes.SingleOrDefault(x => x.Id == targetId);

                if (itemToRemove != null)
                {
                    ctx.Themes.Remove(itemToRemove);
                    ctx.SaveChanges();
                }
            }
        }
        public void DeleteTrack(int targetId)
        {
            using (var ctx = new AppDbContext())
            {
                var itemToRemove = ctx.Tracks.SingleOrDefault(x => x.Id == targetId);

                if (itemToRemove != null)
                {
                    ctx.Tracks.Remove(itemToRemove);
                    ctx.SaveChanges();
                }
            }
        }
    }
}