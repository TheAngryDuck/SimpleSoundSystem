using System.Collections.Generic;

namespace DesktopApp.Models
{
    public class Theme
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TrackInTheme> TrackInThemes { get; set; } = new List<TrackInTheme>();
    }
}