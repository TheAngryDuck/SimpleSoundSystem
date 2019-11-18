using System;
using System.Collections.Generic;

namespace Domain
{
    public class Theme
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TrackInTheme> TrackInThemes { get; set; } = new List<TrackInTheme>();
    }
}