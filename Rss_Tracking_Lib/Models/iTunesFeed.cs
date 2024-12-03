using Rss_Tracking_Lib.Helpers;
using Rss_Tracking_Lib.Models.iTunes_RSS;
using System.ServiceModel.Syndication;

namespace Rss_Tracking_Lib.Models
{
    public class ITunesFeed : MediaFeed
    {
        public ITunesFeed(SyndicationFeed feed) : base(feed) { }
        public const string NAMESPACE = "http://www.itunes.com/dtds/podcast-1.0.dtd";
        public new IEnumerable<ITunesItem> Items { get => Feed.Items.Select(x => new ITunesItem(x)); }
        public string? ITunesType { get => SyndicationHelper.GetExtensionElementValue<string>(Feed, NAMESPACE, "type"); }
        public string? Summary { get => SyndicationHelper.GetExtensionElementValue<string>(Feed, NAMESPACE, "summary"); }
        public ITunesOwner? Owner { get => SyndicationHelper.GetExtensionElementValue<ITunesOwner>(Feed, NAMESPACE, "owner"); }
        public string? Author { get => SyndicationHelper.GetExtensionElementValue<string>(Feed, NAMESPACE, "author"); }
        public string? Explicit { get => SyndicationHelper.GetExtensionElementValue<string>(Feed, NAMESPACE, "explicit"); }
        public ITunesCategory? Category { get => SyndicationHelper.GetExtensionElementValue<ITunesCategory>(Feed, NAMESPACE, "category"); }
        public ITunesImage? Image { get => SyndicationHelper.GetExtensionElementValue<ITunesImage>(Feed, NAMESPACE, "image"); }
    }
    public class ITunesItem : MediaItem
    {
        public ITunesItem(SyndicationItem item) : base(item) { }
        public const string NAMESPACE = "http://www.itunes.com/dtds/podcast-1.0.dtd";
        public string? ITunesTitle { get => SyndicationHelper.GetExtensionElementValue<string>(Item, NAMESPACE, "title"); }
        public string? EpisodeType { get => SyndicationHelper.GetExtensionElementValue<string>(Item, NAMESPACE, "episodeType"); }
        public int? Episode { get => SyndicationHelper.GetExtensionElementValue<int>(Item, NAMESPACE, "episode"); }
        public string? Author { get => SyndicationHelper.GetExtensionElementValue<string>(Item, NAMESPACE, "author"); }
        public ITunesImage? Image { get => SyndicationHelper.GetExtensionElementValue<ITunesImage>(Item, NAMESPACE, "image"); }
        public int? Duration { get => SyndicationHelper.GetExtensionElementValue<int>(Item, NAMESPACE, "duration"); }
        public int? Season { get => SyndicationHelper.GetExtensionElementValue<int>(Item, NAMESPACE, "season"); }
    }
}
