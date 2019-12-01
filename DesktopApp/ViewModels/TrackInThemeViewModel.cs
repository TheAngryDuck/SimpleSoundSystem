using System;
using System.Collections.Generic;
using System.Text;
using DesktopApp.Helpers;

namespace DesktopApp.ViewModels
{
    class TrackInThemeViewModel : ObservableObject, IPageViewModel
    {
        public string Name
        {
            get { return "Tracks in themes Page"; }
        }
    }
}
