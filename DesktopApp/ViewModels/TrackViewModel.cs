using System.Collections.Generic;
using System.Windows.Input;
using DAL;
using DesktopApp.Helpers;
using Domain;
using Microsoft.Win32;

namespace DesktopApp.ViewModels
{
    class TrackViewModel : ObservableObject, IPageViewModel
    {
        private IList<Track> _tracks;
        private Dao data;
        private ICommand _addTrackCommand;
        private ICommand _deleteTrackCommand;
        private string _trackNameInner;
        private string _trackPathInner;
        public Track SelectedTrack { get; set; }
        public string TrackNameOuter
        {
            get { return this._trackNameInner; }
            set
            {
                if (!string.Equals(this._trackNameInner, value))
                {
                    this._trackNameInner = value;
                    this.RaisePropertyChanged("trackName");
                }
            }
        }
        public string TrackPathOuter
        {
            get { return this._trackPathInner; }
            set
            {
                if (!string.Equals(this._trackPathInner, value))
                {
                    this._trackPathInner = value;
                    this.RaisePropertyChanged("trackPath");
                }
            }
        }
        public string Name
        {
            get { return "Tracks page"; }
        }
        public TrackViewModel()
        {
            data = new Dao();

            _tracks = data.GetAllTracks();
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
        public ICommand SaveTrackCommand
        {
            get
            {
                if (_addTrackCommand == null)
                {
                    _addTrackCommand = new RelayCommand(
                        param => Button_Click(),
                        param => (TrackNameOuter != null)
                    );
                }
                return _addTrackCommand;
            }
        }
        public ICommand DeleteTrackCommand
        {
            get
            {
                if (_deleteTrackCommand == null)
                {
                    _deleteTrackCommand = new RelayCommand(
                        param => Delete_track(SelectedTrack),
                        param => (_deleteTrackCommand != null)
                    );
                }
                return _deleteTrackCommand;
            }
        }
        private void Button_Click()
        {
            if (TrackNameOuter != null || TrackNameOuter != "")
            {
                var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                TrackPathOuter = dialog.FileName;
            }

            bool isNew = true;
            var tmp = new Track {Name = _trackNameInner, FilePath = _trackPathInner};
            foreach (var track in Tracks)
            {
                if (track.Name.Equals(tmp.Name))
                {
                    isNew = false;
                }
            }
            if (isNew)
            {
                data.AddTrack(tmp);
            Tracks = data.GetAllTracks();
            }
            }
        }

        private void Delete_track(Track item)
        {
            if (item != null)
            {
                data.DeleteTrack(item.Id);
            Tracks = data.GetAllTracks();
            }
        }


    }
}
