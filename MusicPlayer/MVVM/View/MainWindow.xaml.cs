using MusicPlayer.MVVM.ViewModel;
using System.ComponentModel;
using MusicPlayer.Core;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MusicPlayer.MVVM.Model;

namespace MusicPlayer
{
    public partial class MainWindow : Window
    {
        private MainViewModel vm => DataContext as MainViewModel;
        public MainWindow()
        {
            InitializeComponent();
        }
        protected void ListViewItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            PlayItem item = listView.SelectedItem as PlayItem;
            vm.Player.PlayItemCommand.Execute(item);
        }

        private void Window_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop, true)) return;

            e.Effects = DragDropEffects.Copy;
            e.Handled = true;
        }
        private void Window_Drop(object sender, DragEventArgs e)
        {
            var list = vm.Setting.PlayList;
            var files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (files == null) return;

            foreach (var s in files)
                list.Add(new PlayItem(s));
        }
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = sender as Slider;
            vm.PlayerValueChanger(slider.Value);
        }


    }
}