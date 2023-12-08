using MusicPlayer.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace MusicPlayer.MVVM.Model
{
    [CollectionDataContract(Namespace = "", Name = "PlayList")]
    public class PlayListCollection : ObservableCollection<PlayItem>
    {
        public PlayListCollection() : base() { }
        public PlayListCollection(List<PlayItem> list) : base(list) { }

    }
}