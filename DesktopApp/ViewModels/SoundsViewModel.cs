using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using DesktopApp.Helpers;
using DesktopApp.Models;
using DesktopApp.Views;

namespace DesktopApp.ViewModels
{

    class SoundsViewModel : ObservableObject, IPageViewModel
    {
        public string Name {
            get { return "Related tracks Page"; }
        }
        public Track SelectedTrack { get; set; }
        public Theme SelectedTheme { get; set; }
        private IList<Track> _tracks;
        private Dao data;
        private ICommand _goSoundsCommand;
        private ICommand _goAllCommand;

        public Track Tmp { get; set; }
        

        public SoundsViewModel(Theme theme)
        {
            data = new Dao();
            SelectedTheme = theme;
            _tracks = data.GetAllRelatedTracks(theme.Id);
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
        
        public ICommand GoSoundsCommand
        {
            get
            {
                if (_goSoundsCommand == null)
                {
                    _goSoundsCommand = new RelayCommand(
                        param => SoundsViewGo(),
                        param => (_goSoundsCommand != null)
                    );
                }
                return _goSoundsCommand;
            }
        }
        public ICommand GoAllCommand
        {
            get
            {
                if (_goAllCommand == null)
                {
                    _goAllCommand = new RelayCommand(
                        param => GoAll(),
                        param => (_goAllCommand != null)
                    );
                }
                return _goAllCommand;
            }
        }
        private void SoundsViewGo()
        {
            PlayerView app = new PlayerView();  
            PlayerViewModel context = new PlayerViewModel(SelectedTrack);
            app.DataContext = context;
            app.Show();
            
        }

        private void GoAll()
        {
            foreach (var track in Tracks)
            {
                PlayerView app = new PlayerView();
                PlayerViewModel context = new PlayerViewModel(track);
                app.DataContext = context;
                app.Show();
            }
        }


    }
}
