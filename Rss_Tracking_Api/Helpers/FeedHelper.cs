using System.ServiceModel.Syndication;
using System.Xml;

namespace Rss_Tracking_Api.Helpers
{
    public static class FeedHelper
    {
        public static SyndicationFeed GetLatestFeed(Uri uri)
        {
            using var reader = XmlReader.Create(uri.AbsoluteUri);
            return SyndicationFeed.Load(reader);
        }

        public static SyndicationItem GetLatestItem(SyndicationFeed feed)
        {
            if (feed == null || !feed.Items.Any()) throw new KeyNotFoundException("Failed to find any items for given URI");
            var itemList = feed.Items.OrderByDescending(x => x.LastUpdatedTime);
            return itemList.First();
        }
    }
}
