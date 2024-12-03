using Rss_Tracking_Lib.Helpers;
using System.ServiceModel.Syndication;

namespace Rss_Tracking_Lib.Models
{
    public class MediaFeed
    {
        public readonly SyndicationFeed Feed;
        public MediaFeed(SyndicationFeed feed)
        {
            Feed = feed;
        }
        public const string NAMESPACE = "http://www.itunes.com/dtds/podcast-1.0.dtd";
        public new IEnumerable<MediaItem> Items { get => Feed.Items.Select(x => new MediaItem(x)); }
    }
    public class MediaItem
    {
        public readonly SyndicationItem Item;
        public MediaItem(SyndicationItem item)
        {
            Item = item;
        }
        public const string NAMESPACE = "http://www.itunes.com/dtds/podcast-1.0.dtd";
        public List<Media_RSS.MediaGroup>? MediaGroup { get => SyndicationHelper.GetExtensionElementValue<List<Media_RSS.MediaGroup>>(Item, NAMESPACE, "group"); }
        public List<Media_RSS.MediaContent>? MediaContent { get => SyndicationHelper.GetExtensionElementValue<List<Media_RSS.MediaContent>>(Item, NAMESPACE, "content"); }
    }
}
