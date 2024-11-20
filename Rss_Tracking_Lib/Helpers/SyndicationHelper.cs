using System.ServiceModel.Syndication;

namespace Rss_Tracking_Lib.Helpers
{
    public static class SyndicationHelper
    {
        public static T? GetExtensionElementValue<T>(SyndicationItem item, string extensionNamespace, string extensionElementName)
        {
            if (item.ElementExtensions.Any(x => x.OuterName == extensionElementName && x.OuterNamespace == extensionNamespace))
                return item.ElementExtensions.First(ee => ee.OuterNamespace == extensionNamespace && ee.OuterName == extensionElementName).GetObject<T>();
            return default;
        }
        public static T? GetExtensionElementValue<T>(SyndicationFeed item, string extensionNamespace, string extensionElementName)
        {
            if (item.ElementExtensions.Any(x => x.OuterName == extensionElementName && x.OuterNamespace == extensionNamespace))
                return item.ElementExtensions.First(ee => ee.OuterNamespace == extensionNamespace && ee.OuterName == extensionElementName).GetObject<T>();
            return default;
        }
        public static T? GetExtensionElementValue<T>(SyndicationItem item, string extensionElementName)
        {
            if (item.ElementExtensions.Any(x => x.OuterName == extensionElementName))
                return item.ElementExtensions.First(ee => ee.OuterName == extensionElementName).GetObject<T>();
            return default;
        }
        public static T? GetExtensionElementValue<T>(SyndicationFeed item, string extensionElementName)
        {
            if (item.ElementExtensions.Any(x => x.OuterName == extensionElementName))
                return item.ElementExtensions.First(ee => ee.OuterName == extensionElementName).GetObject<T>();
            return default;
        }
    }
}