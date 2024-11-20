using Rss_Tracking_Lib.Helpers;
using Rss_Tracking_Lib.Models.iTunes_RSS;

namespace Rss_Tracking_Lib.Models
{
    public class ITunesFeed : MediaFeed
    {
        private const string NAMESPACE = "itunes";
        public new IEnumerable<ITunesItem> Items { get => base.Items.Select(x => (ITunesItem)x); }
        public string? ITunesType { get => SyndicationHelper.GetExtensionElementValue<string>(this, NAMESPACE, "type"); }
        public string? Summary { get => SyndicationHelper.GetExtensionElementValue<string>(this, NAMESPACE, "summary"); }
        public ITunesOwner? Owner { get => SyndicationHelper.GetExtensionElementValue<ITunesOwner>(this, NAMESPACE, "owner"); }
        public string? Author { get => SyndicationHelper.GetExtensionElementValue<string>(this, NAMESPACE, "author"); }
        public string? Explicit { get => SyndicationHelper.GetExtensionElementValue<string>(this, NAMESPACE, "explicit"); }
        public ITunesCategory? Category { get => SyndicationHelper.GetExtensionElementValue<ITunesCategory>(this, NAMESPACE, "category"); }
        public ITunesImage? Image { get => SyndicationHelper.GetExtensionElementValue<ITunesImage>(this, NAMESPACE, "image"); }
    }
    public class ITunesItem : MediaItem
    {
        private const string NAMESPACE = "itunes";
        public string? ITunesTitle { get => SyndicationHelper.GetExtensionElementValue<string>(this, NAMESPACE, "title"); }
        public string? EpisodeType { get => SyndicationHelper.GetExtensionElementValue<string>(this, NAMESPACE, "episodeType"); }
        public int? Episode { get => SyndicationHelper.GetExtensionElementValue<int>(this, NAMESPACE, "episode"); }
        public string? Author { get => SyndicationHelper.GetExtensionElementValue<string>(this, NAMESPACE, "author"); }
        public ITunesImage? Image { get => SyndicationHelper.GetExtensionElementValue<ITunesImage>(this, NAMESPACE, "image"); }
        public int? Duration { get => SyndicationHelper.GetExtensionElementValue<int>(this, NAMESPACE, "duration"); }
        public int? Season { get => SyndicationHelper.GetExtensionElementValue<int>(this, NAMESPACE, "season"); }
    }
}
