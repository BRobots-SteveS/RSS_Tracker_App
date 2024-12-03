using Rss_Tracking_Lib.Helpers;
using System.ServiceModel.Syndication;

namespace Rss_Tracking_Lib.Models
{
    public partial class OmnyFeed : ITunesFeed
    {
        public OmnyFeed(SyndicationFeed feed) : base(feed) { }
        public const string NAMESPACE = "https://omny.fm/rss-extensions";
        public new IEnumerable<OmnyItem> Items { get => Feed.Items.Select(x => new OmnyItem(x)); }
        public Guid OrganizationId { get => SyndicationHelper.GetExtensionElementValue<Guid>(Feed, NAMESPACE, "organizationId"); }
        public Guid NetworkId { get => SyndicationHelper.GetExtensionElementValue<Guid>(Feed, NAMESPACE, "networkId"); }
        public Guid ProgramId { get => SyndicationHelper.GetExtensionElementValue<Guid>(Feed, NAMESPACE, "programId"); }
        public Guid PlaylistId { get => SyndicationHelper.GetExtensionElementValue<Guid>(Feed, NAMESPACE, "playlistId"); }

    }
    public class OmnyItem : ITunesItem
    {
        public OmnyItem(SyndicationItem item) : base(item) { }
        public const string NAMESPACE = "https://omny.fm/rss-extensions";
        public Guid ClipId { get => SyndicationHelper.GetExtensionElementValue<Guid>(Item, NAMESPACE, "clipId"); }
    }
}
