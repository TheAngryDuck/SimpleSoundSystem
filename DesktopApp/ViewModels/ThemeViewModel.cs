using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using DAL;
using DesktopApp.Helpers;
using DesktopApp.Views;
using Domain;

namespace DesktopApp.ViewModels
{
    class ThemeViewModel : ObservableObject, IPageViewModel
    {
        private IList<Theme> _themes;
        private Dao data;
        private ICommand _addThemeCommand;
        private ICommand _deleteThemeCommand;
        private ICommand _goSoundsCommand;
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
        private void Button_Click()
        {
            bool isNew = true;
            Theme tmp = new Theme {Name = _themeNameInner};
            foreach (var theme in Themes)
            {
                if (theme.Name.Equals(tmp.Name))
                {
                    isNew = false;
                }
            }
            if (isNew)
            {
                data.AddTheme(tmp);
            Themes = data.GetAllThemes();
            }
        }

        private void Delete_theme(Theme item)
        {
            if (item !=null)
            {
                data.DeleteTheme(item.Id);
            Themes = data.GetAllThemes();
            }
            
        }

        private void SoundsViewGo()
        {
            if (SelectedTheme !=null)
            {
                 SoundsView app = new SoundsView();
            SoundsViewModel context = new SoundsViewModel(SelectedTheme);
            app.DataContext = context;
            app.Show();
            }
           
        }
    }
}
