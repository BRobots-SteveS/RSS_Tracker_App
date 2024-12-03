using Rss_Tracking_Lib.Helpers;
using System.ServiceModel.Syndication;

namespace Rss_Tracking_Lib.Models
{
    public class YoutubeFeed : MediaFeed
    {
        public YoutubeFeed(SyndicationFeed feed) : base(feed) { }
        public const string NAMESPACE = "http://www.youtube.com/xml/schemas/2015";
        public new IEnumerable<YoutubeItem> Items { get => Feed.Items.Select(x => new YoutubeItem(x)); }
        public string? ChannelId { get => SyndicationHelper.GetExtensionElementValue<string>(Feed, NAMESPACE, "channelId"); }
        public string? PlaylistId { get => SyndicationHelper.GetExtensionElementValue<string>(Feed, NAMESPACE, "playlistId"); }
    }
    public class YoutubeItem : MediaItem
    {
        public YoutubeItem(SyndicationItem item) : base(item) { }
        public const string NAMESPACE = "http://www.youtube.com/xml/schemas/2015";
        public string? VideoId { get => SyndicationHelper.GetExtensionElementValue<string>(Item, NAMESPACE, "videoId"); }
        public string? ChannelId { get => SyndicationHelper.GetExtensionElementValue<string>(Item, NAMESPACE, "channelId"); }
    }
}
