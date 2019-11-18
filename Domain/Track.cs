using System.Collections.Generic;

namespace Domain
{
    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public List<TrackInTheme> TrackInThemes { get; set; } = new List<TrackInTheme>();
    }
}