using Rss_Tracking_App.Models.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rss_Mobile_App.Converters
{
    public class ImageSourceConverter : IValueConverter
    {
        private const string RSS = "https://rss.com/blog/wp-content/uploads/2019/10/social_style_3_rss-512-1.png";
        private const string ITUNES = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/df/ITunes_logo.svg/1200px-ITunes_logo.svg.png";
        private const string YOUTUBE = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/09/YouTube_full-color_icon_%282017%29.svg/512px-YouTube_full-color_icon_%282017%29.svg.png";
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if(value == null || value.GetType() != typeof(FeedDto)) return RSS;
            FeedDto feed = (FeedDto)value;
            if(feed == null) return RSS;
            if (!string.IsNullOrWhiteSpace(feed.ThumbnailUri)) return feed.ThumbnailUri;
            return feed.Platform switch
            {
                "Youtube" => YOUTUBE,
                "Youtube Channel" => YOUTUBE,
                "Youtube Playlist" => YOUTUBE,
                "iTunes" => ITUNES,
                _ => RSS,
            };
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
