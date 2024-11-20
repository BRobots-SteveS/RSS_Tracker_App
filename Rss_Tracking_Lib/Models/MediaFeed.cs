using Rss_Tracking_Lib.Helpers;
using System.ServiceModel.Syndication;

namespace Rss_Tracking_Lib.Models
{
    public class MediaFeed : SyndicationFeed
    {
        public new IEnumerable<MediaItem> Items { get => base.Items.Select(x => (MediaItem)x); }
    }
    public class MediaItem : SyndicationItem
    {
        private const string NAMESPACE = "media";
        public List<Media_RSS.MediaGroup>? MediaGroup { get => SyndicationHelper.GetExtensionElementValue<List<Media_RSS.MediaGroup>>(this, NAMESPACE, "group"); }
        public List<Media_RSS.MediaContent>? MediaContent { get => SyndicationHelper.GetExtensionElementValue<List<Media_RSS.MediaContent>>(this, NAMESPACE, "content"); }
    }
}
