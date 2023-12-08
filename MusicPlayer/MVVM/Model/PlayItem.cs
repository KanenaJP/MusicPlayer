using MusicPlayer.Core;
using Shell32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.MVVM.Model
{
    [DataContract(Namespace = "")]

    public class PlayItem : BindableBase
    {
        [DataMember(Order = 0)]
        public string FilePath { get; set; }

        private string _Title;
        public string Title { get => _Title; set => SetProperty(ref _Title, value); }
        private TimeSpan _Length;
        public TimeSpan Length { get => _Length; set => SetProperty(ref _Length, value); }
        private bool _IsSelected;
        public bool IsSelected { get => _IsSelected; set => SetProperty(ref _IsSelected, value); }

        private static Shell _Shell;
        private Shell Shell
        {
            get
            {
                if (_Shell == null) _Shell = new Shell();
                return _Shell;
            }
        }

        public PlayItem(string filePath)
        {
            FilePath = filePath;
            Init();
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context) => Init();
        private new void Init()
        {
            var dir = Path.GetDirectoryName(FilePath);
            var name = Path.GetFileName(FilePath);
            var folder = Shell.NameSpace(dir);
            var folderItem = folder.ParseName(name);

            var title = folder.GetDetailsOf(folderItem, 21);
            Title = (title.Length > 0 && title.Length < 60)
                  ? title : Path.GetFileNameWithoutExtension(FilePath);

            if (TimeSpan.TryParse(folder.GetDetailsOf(folderItem, 27), out var timeSpan))
                Length = timeSpan;
        }
        public override string ToString() => $"{Title}";

    }
}
