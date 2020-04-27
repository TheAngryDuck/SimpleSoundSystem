using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using DesktopApp.Helpers;
using DesktopApp.Models;

namespace DesktopApp.ViewModels
{
    class PlayerViewModel
    {
        
        public Track SelectedTrack { get; set; }
        
        public PlayerViewModel(Track track)
        {
            SelectedTrack = track;
        }
    }
}
