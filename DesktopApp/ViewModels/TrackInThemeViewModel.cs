using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using DesktopApp.Helpers;
using DesktopApp.Models;

namespace DesktopApp.ViewModels
{
    class TrackInThemeViewModel : ObservableObject, IPageViewModel
    {
        private IList<Track> _tracks;
        private IList<Theme> _themes;
        private IList<TrackInTheme> _trackInThemes;

        private ICommand _addTrackInThemeCommand;
        private ICommand _refreshCommand;

        private ICommand _deleteTrackInThemeCommand;
        public TrackInTheme SelectedTrackInTheme { get; set; }

        private Dao data;
        public Theme SelectedTheme { get; set; }
        public Track SelectedTrack { get; set; }
        public string Name
        {
            get { return "Tracks in themes Page"; }
        }
        public TrackInThemeViewModel()
        {
            data = new Dao();

            _tracks = data.GetAllTracks();
            _themes = data.GetAllThemes();
            _trackInThemes = data.GetAllTrackInThemes();
        }
        public IList<Track> Tracks
        {
            get { return _tracks; }
            set
            {
                _tracks = value;
                this.RaisePropertyChanged("Tracks");
            }
        }
        public IList<TrackInTheme> TracksInThemes
        {
            get { return _trackInThemes; }
            set
            {
                _trackInThemes = value;
                this.RaisePropertyChanged("TracksInThemes");
            }
        }
        public IList<Theme> Themes
        {
            get { return _themes; }
            set
            {
                _themes = value;
                this.RaisePropertyChanged("Themes");
            }
        }
        public ICommand SaveTrackInThemeCommand
        {
            get
            {
                if (_addTrackInThemeCommand == null)
                {
                    _addTrackInThemeCommand = new RelayCommand(
                        param => Button_Click(),
                        param => (_addTrackInThemeCommand != null)
                    );
                }
                return _addTrackInThemeCommand;
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                if (_refreshCommand == null)
                {
                    _refreshCommand = new RelayCommand(
                        param => Refresh(),
                        param => (_refreshCommand != null)
                    );
                }
                return _refreshCommand;
            }
        }
        public ICommand DeleteTrackInThemeCommand
        {
            get
            {
                if (_deleteTrackInThemeCommand == null)
                {
                    _deleteTrackInThemeCommand = new RelayCommand(
                        param => Delete_trackInTheme(SelectedTrackInTheme),
                        param => (_deleteTrackInThemeCommand != null)
                    );
                }
                return _deleteTrackInThemeCommand;
            }
        }
        private void Button_Click()
        {
            if (SelectedTheme != null && SelectedTrack != null)
            {
                var tmp = new TrackInTheme();
            tmp.Theme = SelectedTheme;
            tmp.Track = SelectedTrack;
            tmp.ThemeId = SelectedTheme.Id;
            tmp.TrackId = SelectedTrack.Id;
            data.AddTrackInTheme(tmp);
            TracksInThemes = data.GetAllTrackInThemes();
            Tracks = data.GetAllTracks();
            Themes = data.GetAllThemes();
            }
        }
        private void Delete_trackInTheme(TrackInTheme item)
        {
            if (item != null)
            {
                data.DeleteTrackInTheme(item.Id);
            TracksInThemes = data.GetAllTrackInThemes();
            Tracks = data.GetAllTracks();
            Themes = data.GetAllThemes();
            }
            
        }

        public void Refresh()
        {
            var tmp1 = new List<Track>();
            var tmp2 = new List<Theme>();
            var tmp3 = new List<TrackInTheme>();
            TracksInThemes = tmp3;
            Tracks = tmp1;
            Themes = tmp2;
            TracksInThemes = data.GetAllTrackInThemes();
            Tracks = data.GetAllTracks();
            Themes = data.GetAllThemes();
        }
    }
}
