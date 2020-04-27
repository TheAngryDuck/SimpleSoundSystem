using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Win32;

namespace DesktopApp.Views
{
    /// <summary>
    /// Interaction logic for PlayerView.xaml
    /// </summary>
    /// The source for the code of the Player view is https://www.wpf-tutorial.com/audio-video/how-to-creating-a-complete-audio-video-player/
    public partial class PlayerView : Window
    {
        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;
        private double volume = 0.5;
        private bool canSwell = false;
        private bool canFade = false;
        private double CurrentVolume = 0.5;
        public bool CanLoop { get; set; } = false;
        private bool willLoop = false;

        public PlayerView()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            DispatcherTimer volTimer = new DispatcherTimer();
            volTimer.Interval = TimeSpan.FromMilliseconds(250);
            volTimer.Tick += timer_Tick2;
            volTimer.Start();

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if ((mePlayer.Source != null) && (mePlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                sliProgress.Minimum = 0;
                sliProgress.Maximum = mePlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = mePlayer.Position.TotalSeconds;
            }
        }
        private void timer_Tick2(object sender, EventArgs e)
        {
            if (canSwell && (mePlayer.Volume - volume) >= -0.01)
            {
                canSwell = false;
            }
            if (canSwell)
            {
                mePlayer.Volume += (CurrentVolume / 12);
            }

            if (canFade && Math.Abs(mePlayer.Volume) > 0)
            {
                mePlayer.Volume -= (CurrentVolume / 12);
            }

            

            if (canFade && Math.Abs(mePlayer.Volume) <= 0.01)
            {
                canFade = false;
                mePlayer.Stop();
                mediaPlayerIsPlaying = false;
                mePlayer.Volume = volume;
            }

        }

        private void Check(object sender, RoutedEventArgs e)
        {
            CanLoop = true;

            loopBox.Background = new SolidColorBrush(Colors.LawnGreen);
        }

        private void Uncheck(object sender, RoutedEventArgs e)
        {
            CanLoop = false;
            loopBox.Background = new SolidColorBrush(Colors.Red);
        }

        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (mePlayer != null) && (mePlayer.Source != null);
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            volume = mePlayer.Volume;
            mePlayer.Volume -= mePlayer.Volume;
            
            mePlayer.Play();
            canSwell = true;
            mediaPlayerIsPlaying = true;
            willLoop = true;
        }
        private void More_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void Less_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void More_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (mePlayer.Volume < 1)
            {
                mePlayer.Volume += 0.1;
                //volume = mePlayer.Volume;
                CurrentVolume = mePlayer.Volume;
            }
            
        }
        private void Less_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (mePlayer.Volume > 0)
            {
               mePlayer.Volume -= 0.1;
               //volume = mePlayer.Volume;
                CurrentVolume = mePlayer.Volume;

            }

        }

        private void Media_Ended(object sender, EventArgs e)
        {
            if (CanLoop && willLoop)
            {
                mePlayer.Position = TimeSpan.FromMilliseconds(1);
                mePlayer.Play();
            }
            else
            {
                mePlayer.Stop();
            }
            
        }
        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Pause();
        }

        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            canFade = true;
            willLoop = false;

        }


        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            mePlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mePlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }
    }
}
