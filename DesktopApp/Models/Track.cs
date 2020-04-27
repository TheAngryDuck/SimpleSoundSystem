using System.Collections.Generic;

namespace DesktopApp.Models
{
    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public List<TrackInTheme> TrackInThemes { get; set; } = new List<TrackInTheme>();
    }
}