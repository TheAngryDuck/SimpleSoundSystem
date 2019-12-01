using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using DAL;
using DesktopApp.Helpers;
using Domain;

namespace DesktopApp.ViewModels
{
    class ThemeViewModel : ObservableObject, IPageViewModel
    {
        private IList<Theme> _themes;
        private Dao data;
        private ICommand _addThemeCommand;
        private ICommand _deleteThemeCommand;
        private string _themeNameInner;
        public Theme SelectedTheme { get; set; }
        public string ThemeNameOuter
        {
            get { return this._themeNameInner; }
            set
            {
                if (!string.Equals(this._themeNameInner, value))
                {
                    this._themeNameInner = value;
                    this.RaisePropertyChanged("themeName"); 
                }
            }
        }

        public string Name
        {
            get { return "Themes page"; }
        }

        public ThemeViewModel()
        {
            data = new Dao();
            
            _themes = data.GetAllThemes();
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

        
        public ICommand SaveThemeCommand
        {
            get
            {
                if (_addThemeCommand == null)
                {
                    _addThemeCommand = new RelayCommand(
                        param => Button_Click(),
                        param => (ThemeNameOuter != null)
                    );
                }
                return _addThemeCommand;
            }
        }
        public ICommand DeleteThemeCommand
        {
            get
            {
                if (_deleteThemeCommand == null)
                {
                    _deleteThemeCommand = new RelayCommand(
                        param => Delete_theme(SelectedTheme),
                        param => (ThemeNameOuter != null)
                    );
                }
                return _deleteThemeCommand;
            }
        }
        private void Button_Click()
        {
            Theme tmp = new Theme {Name = _themeNameInner};
            data.AddTheme(tmp);
            Themes = data.GetAllThemes();

        }

        private void Delete_theme(Theme item)
        {
            data.DeleteTheme(item.Id);
            Themes = data.GetAllThemes();
        }
    }
}
