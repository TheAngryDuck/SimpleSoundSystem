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

        public List<TrackInTheme> GetAllTrackInThemes()
        {
            using (var ctx = new AppDbContext())
            {
                List<TrackInTheme> trackInThemes = new List<TrackInTheme>();
                foreach (var trackInTheme in ctx.TrackInThemes)
                {
                    foreach (var track in ctx.Tracks)
                    {
                        if (track.Id == trackInTheme.TrackId)
                        {
                            trackInTheme.Track = track;
                        }
                    }

                    foreach (var theme in ctx.Themes)
                    {
                        if (theme.Id == trackInTheme.ThemeId)
                        {
                            trackInTheme.Theme = theme;
                        }
                    }
                    trackInThemes.Add(trackInTheme);
                }

                return trackInThemes;
            }

        }

        public List<Track> GetAllRelatedTracks(int targetId)
        {
            using (var ctx = new AppDbContext())
            {
                List<Track> relatedTracks = new List<Track>();
                foreach (var trackInTheme in ctx.TrackInThemes)
                {
                    if (trackInTheme.ThemeId == targetId)
                    {
                        foreach (var track in ctx.Tracks)
                        {
                            if (track.Id == trackInTheme.TrackId)
                            {
                                relatedTracks.Add(track);
                            }
                        }
                    }
                }

                return relatedTracks;
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

        public void DeleteTrackInTheme(int targetId)
        {
            using (var ctx = new AppDbContext())
            {
                var itemToRemove = ctx.TrackInThemes.SingleOrDefault(x => x.Id == targetId);

                if (itemToRemove != null)
                {
                    ctx.TrackInThemes.Remove(itemToRemove);
                    ctx.SaveChanges();
                }
            }
        }
    }
}