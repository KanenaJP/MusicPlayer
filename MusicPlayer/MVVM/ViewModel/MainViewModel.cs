using Microsoft.Win32;
using MusicPlayer.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using MusicPlayer.MVVM.Model;
using MediaState = MusicPlayer.MVVM.Model.MediaState;

namespace MusicPlayer.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public RelayCommand MoveWindowCommand { get; set; }
        public RelayCommand ShutdownCommand { get; set; }
        public RelayCommand MinimizeWindowCommand { get; set; }
        public RelayCommand OpenFileCommand { get; set; }
        public RelayCommand TogglePlayPauseCommand { get; set; }
        public RelayCommand DragOverCommand { get; set; }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }
        public SettingsModel Setting { get; }
        public PlayerModel Player { get; }
        private bool _IsPlaying;
        public bool IsPlaying { get => _IsPlaying; set => SetProperty(ref _IsPlaying, value); }


        public MainViewModel()
        {
            var serializer = new SerializeHelper<SettingsModel>();
            Setting = serializer.Load();
            Player = new PlayerModel(Setting.PlayList);
            Player.PropertyChanged += Player_PropertyChanged;

            Application.Current.MainWindow.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            //Application Commands
            MoveWindowCommand = new RelayCommand(o => { Application.Current.MainWindow.DragMove(); });
            ShutdownCommand = new RelayCommand(o => { Application.Current.Shutdown(); });
            MinimizeWindowCommand = new RelayCommand(o => { Application.Current.MainWindow.WindowState = WindowState.Minimized; });
        }
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        private void Player_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Player.MediaState))
                IsPlaying = Player.MediaState == MediaState.Playing;
        }
        public void PlayerValueChanger(double value)
        {
            TimeSpan time = TimeSpan.FromSeconds(value);
            Player.Position = time;
        }

    }
}
