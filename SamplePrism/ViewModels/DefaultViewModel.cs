using SamplePrism.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SamplePrism.ViewModels
{
    public class DefaultViewModel : BindableBase
    {
        private ObservableCollection<MediaItem> _mediaItems;
        public ObservableCollection<MediaItem> MediaItems
        {
            get { return _mediaItems; }
            set { _mediaItems = value; RaisePropertyChanged(nameof(MediaItems)); }
        }

        
        public DefaultViewModel()
        {
            MediaItems = new ObservableCollection<MediaItem>();
            MediaItem mediaItem = new MediaItem()
            {
                Title = "吞噬星空",
                Subtitle = "看至第189",
                StatusTag = "最近在看",
                EpisodeInfo = "更新至189集"
                //BackgroundBrush = new ImageBrush("")

            };

        }
    }
}
