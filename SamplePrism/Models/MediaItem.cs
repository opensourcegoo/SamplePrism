using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SamplePrism.Models
{
    public class MediaItem : BindableBase
    {

        private string _title;

        public MediaItem()
        {

        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get => _title;
            set { _title = value; RaisePropertyChanged(nameof(Title)); }

        }

        /// <summary>
        /// 子标题
        /// </summary>
        private string _subtitle;
        public string Subtitle
        {
            get => _subtitle;
            set { _subtitle = value; RaisePropertyChanged(nameof(Subtitle)); }
        }

        /// <summary>
        /// 状态标签
        /// </summary>
        private string _statusTag;
        public string StatusTag
        {
            get => _statusTag;
            set { _statusTag = value; RaisePropertyChanged(nameof(StatusTag)); }
        }

        /// <summary>
        /// 剧情采集
        /// </summary>
        private string _episodeInfo;
        public string EpisodeInfo
        {
            get => _episodeInfo;
            set { _episodeInfo = value; RaisePropertyChanged(nameof(EpisodeInfo)); }
        }

        /// <summary>
        ///  背景图片
        /// </summary>
        private Brush _backgroundBrush;
        public Brush BackgroundBrush
        {
            get => _backgroundBrush;
            set { _backgroundBrush = value; RaisePropertyChanged(nameof(BackgroundBrush)); }
        }

        /// <summary>
        /// style
        /// </summary>
        private Style _tagStyle;
        public Style TagStyle
        {
            get => _tagStyle;
            set { _tagStyle = value; RaisePropertyChanged(nameof(TagStyle)); }
        }
    }
}

