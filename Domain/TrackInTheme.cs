namespace Domain
{
    public class TrackInTheme
    {
        public int Id { get; set; }
        public Track Track { get; set; }
        public int TrackId { get; set; }

        public Theme Theme { get; set; }
        public int ThemeId { get; set; }
    }
}