using Rss_Tracking_Lib.Helpers;

namespace Rss_Tracking_Lib.Models
{
    public class YoutubeFeed : MediaFeed
    {
        private const string NAMESPACE = "yt";
        public new IEnumerable<YoutubeItem> Items { get => base.Items.Select(x => (YoutubeItem)x); }
        public string? ChannelId { get => SyndicationHelper.GetExtensionElementValue<string>(this, NAMESPACE, "channelId"); }
        public string? PlaylistId { get => SyndicationHelper.GetExtensionElementValue<string>(this, NAMESPACE, "playlistId"); }
    }
    public class YoutubeItem : MediaItem
    {
        private const string NAMESPACE = "yt";
        public string? VideoId { get => SyndicationHelper.GetExtensionElementValue<string>(this, NAMESPACE, "videoId"); }
        public string? ChannelId { get => SyndicationHelper.GetExtensionElementValue<string>(this, NAMESPACE, "channelId"); }
    }
}
