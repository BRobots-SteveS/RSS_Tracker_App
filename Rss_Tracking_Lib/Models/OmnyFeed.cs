using Rss_Tracking_Lib.Helpers;

namespace Rss_Tracking_Lib.Models
{
    public partial class OmnyFeed : ITunesFeed
    {
        private const string NAMESPACE = "omny";
        public new IEnumerable<OmnyItem> Items { get => base.Items.Select(x => (OmnyItem)x); }
        public Guid OrganizationId { get => SyndicationHelper.GetExtensionElementValue<Guid>(this, NAMESPACE, "organizationId"); }
        public Guid NetworkId { get => SyndicationHelper.GetExtensionElementValue<Guid>(this, NAMESPACE, "networkId"); }
        public Guid ProgramId { get => SyndicationHelper.GetExtensionElementValue<Guid>(this, NAMESPACE, "programId"); }
        public Guid PlaylistId { get => SyndicationHelper.GetExtensionElementValue<Guid>(this, NAMESPACE, "playlistId"); }

    }
    public class OmnyItem : ITunesItem
    {
        private const string NAMESPACE = "omny";
        public Guid ClipId { get => SyndicationHelper.GetExtensionElementValue<Guid>(this, NAMESPACE, "clipId"); }
    }
}
