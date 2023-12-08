using System;
using MusicPlayer.MVVM.Model;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Core
{
    [DataContract(Namespace = "", Name = "Setting")]
    public class SettingsModel : BindableBase
    {
        [DataMember(Order = 0, Name = "IsCompact")]
        private bool _IsCompact;
        public bool IsCompact
        {
            get => _IsCompact;
            set { if (SetProperty(ref _IsCompact, value)) OnPropertyChanged(nameof(IsNormal)); }
        }
        public bool IsNormal { get => !_IsCompact; set => IsCompact = !value; }

        [DataMember(Order = 1, Name = "HidePlayList")]
        private bool _HidePlayList;
        public bool HidePlayList { get => _HidePlayList; set => SetProperty(ref _HidePlayList, value); }

        [DataMember(Order = 2, Name = "Topmost")]
        private bool _Topmost;
        public bool Topmost { get => _Topmost; set => SetProperty(ref _Topmost, value); }

        [DataMember(Order = 5)]
        public PlayListCollection PlayList { get; private set; }

        protected override void Init()
        {
            _IsCompact = false;
            _HidePlayList = false;
            PlayList = new PlayListCollection();
        }
    }
}
