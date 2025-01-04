using Rss_Tracking_Lib.Models;
using Rss_Tracking_Data.Enums;
using System.ServiceModel.Syndication;
using Rss_Tracking_Data.Entities;

namespace Rss_Tracking_Api.Helpers
{
    public static class FeedMapper
    {
        public static IEnumerable<Episode> GetItems(IEnumerable<SyndicationItem> items)
        {
            return items.Select(item => new Episode
            {
                Name = item.Title.Text,
                Url = item.BaseUri.AbsolutePath,
                EpisodeId = item.Id,
                Published = item.PublishDate.Date,
                LastUpdated = item.LastUpdatedTime.Date,
                Description = DescriptionSanitizer(item.Summary.Text)
            });
        }
        public static IEnumerable<Episode> GetItems(IEnumerable<MediaItem> items)
        {
            return items.Select(item =>
            {
                var firstUrl = item.Item.BaseUri?.AbsolutePath;
                string? firstImage = null;
                if (item.MediaGroup is null || item.MediaGroup.Count <= 0)
                {
                    var mediaContent = item.MediaContent;
                    if(!(mediaContent is null || mediaContent.Count <= 0))
                    {
                        var imageItem = mediaContent.Where(m => m.Type.StartsWith("image/")).FirstOrDefault();
                        var urlItem = mediaContent.Where(m => m.Type.StartsWith("audio/")).FirstOrDefault();
                        firstUrl = urlItem?.Url ?? firstUrl;
                        firstImage = imageItem?.Url ?? firstImage;
                    }
                }
                else
                {
                    var mediaContent = item.MediaGroup.FirstOrDefault()!.Content;
                    if (!(mediaContent is null || mediaContent.Count <= 0))
                    {
                        var imageItem = mediaContent.Where(m => m.Type.StartsWith("image/")).FirstOrDefault();
                        var urlItem = mediaContent.Where(m => m.Type.StartsWith("audio/")).FirstOrDefault();
                        firstUrl = urlItem?.Url ?? firstUrl;
                        firstImage = imageItem?.Url ?? firstImage;
                    }
                }
                return new Episode
                {
                    Name = item.MediaGroup?.FirstOrDefault()?.Title ?? item.Item.Title.Text,
                    Url = firstUrl,
                    ThumbnailUrl = firstImage,
                    EpisodeId = item.Item.Id,
                    Published = item.Item.PublishDate.Date,
                    LastUpdated = item.Item.LastUpdatedTime.Date,
                    Description = DescriptionSanitizer(item.MediaGroup?.FirstOrDefault()?.Description ?? item.Item.Summary?.Text)
                };
            });
        }
        public static IEnumerable<Episode> GetItems(IEnumerable<YoutubeItem> items)
        {
            var result = GetItems(items as IEnumerable<MediaItem>);
            for(var i = 0; i < items.Count(); i++)
            {
                result.ElementAt(i).EpisodeId = items.ElementAt(i).VideoId ?? result.ElementAt(i).EpisodeId;
            }
            return result;
        }
        public static IEnumerable<Episode> GetItems(IEnumerable<ITunesItem> items)
        {
            return items.Select(item =>
            {
                var firstUrl = item.Item.BaseUri?.AbsolutePath ?? string.Empty;
                string? firstImage = null;
                if (item.MediaGroup is null || item.MediaGroup.Count <= 0)
                {
                    var mediaContent = item.MediaContent;
                    if (!(mediaContent is null || mediaContent.Count <= 0))
                    {
                        var imageItem = mediaContent.Where(m => m.Type.StartsWith("image/")).FirstOrDefault();
                        var urlItem = mediaContent.Where(m => m.Type.StartsWith("audio/")).FirstOrDefault();
                        firstUrl = urlItem?.Url ?? firstUrl;
                        firstImage = imageItem?.Url ?? firstImage;
                    }
                }
                else
                {
                    var mediaContent = item.MediaGroup.FirstOrDefault()!.Content;
                    if (!(mediaContent is null || mediaContent.Count <= 0))
                    {
                        var imageItem = mediaContent.Where(m => m.Type.StartsWith("image/")).FirstOrDefault();
                        var urlItem = mediaContent.Where(m => m.Type.StartsWith("audio/")).FirstOrDefault();
                        firstUrl = urlItem?.Url ?? firstUrl;
                        firstImage = imageItem?.Url ?? firstImage;
                    }

                }
                return new Episode
                {
                    Name = item.MediaGroup?.FirstOrDefault()?.Title ?? item.Item.Title.Text,
                    Url = firstUrl,
                    ThumbnailUrl = firstImage,
                    EpisodeId = item.Item.Id,
                    Published = item.Item.PublishDate.Date,
                    LastUpdated = item.Item.LastUpdatedTime.Date,
                    Description = DescriptionSanitizer(item.MediaGroup?.FirstOrDefault()?.Description ?? item.Item.Summary.Text)
                };
            });
        }

        public static IEnumerable<Episode> GetItems(IEnumerable<OmnyItem> items)
        {
            return GetItems(items.Select(x => (ITunesItem)x));
        }

        public static Feed FeedToDbObject(SyndicationFeed feed, out List<Author> authors, out List<Episode> episodes)
        {
            authors = [.. feed.Authors.Select(x => new Author { Name = x.Name, Email = x.Email, Uri = x.Uri })];
            episodes = GetItems(feed.Items).ToList();
            return new Feed
            {
                CreatorId = feed.Id,
                Title = feed.Title.Text,
                FeedUrl = feed.Links.FirstOrDefault()?.Uri.AbsoluteUri ?? string.Empty,
                ImageUrl = feed.ImageUrl.AbsoluteUri,
                Description = DescriptionSanitizer(feed.Description.Text),
                LastUpdated = feed.LastUpdatedTime.Date,
                YoutubeType = YoutubeType.None,
                Platform = Platform.Basic
            };
        }
        public static Feed FeedToDbObject(MediaFeed feed, out List<Author> authors, out List<Episode> episodes)
        {
            authors = [.. feed.Feed.Authors.Select(x => new Author { Name = x.Name, Email = x.Email, Uri = x.Uri })];
            episodes = GetItems(feed.Items).ToList();
            return new Feed
            {
                CreatorId = feed.Feed.Id,
                Title = feed.Feed.Title.Text,
                FeedUrl = feed.Feed.Links.FirstOrDefault()?.Uri.AbsoluteUri ?? string.Empty,
                ImageUrl = feed.Feed.ImageUrl.AbsoluteUri,
                Description = DescriptionSanitizer(feed.Feed.Description?.Text),
                LastUpdated = feed.Feed.LastUpdatedTime.Date,
                YoutubeType = YoutubeType.None,
                Platform = Platform.Media
            };
        }
        public static Feed FeedToDbObject(YoutubeFeed feed, out List<Author> authors, out List<Episode> episodes)
        {
            authors = [.. feed.Feed.Authors.Select(x => new Author { Name = x.Name, Email = x.Email, Uri = x.Uri })];
            episodes = GetItems(feed.Items).ToList();
            return new Feed
            {
                CreatorId = feed.ChannelId ?? feed.Feed.Id,
                Title = feed.Feed.Title.Text,
                FeedUrl = string.IsNullOrEmpty(feed.PlaylistId) ? $"https://www.youtube.com/feeds/videos.xml?channel_id={feed.ChannelId}" : $"https://www.youtube.com/feeds/videos.xml?playlist_id={feed.PlaylistId}",
                ImageUrl = feed.Feed.ImageUrl?.AbsoluteUri,
                Description = DescriptionSanitizer(feed.Feed.Description?.Text),
                LastUpdated = feed.Feed.LastUpdatedTime.Date,
                YoutubeType = string.IsNullOrEmpty(feed.PlaylistId) ? YoutubeType.Channel : YoutubeType.Playlist,
                Platform = Platform.Youtube,
                ChannelId = feed.ChannelId,
                PlaylistId = feed.PlaylistId
            };
        }
        public static Feed FeedToDbObject(ITunesFeed feed, out List<Author> authors, out List<Episode> episodes)
        {
            authors = [.. feed.Feed.Authors.Select(x => new Author { Name = x.Name, Email = x.Email, Uri = x.Uri })];
            episodes = GetItems(feed.Items).ToList();
            return new Feed
            {
                CreatorId = feed.Feed.Id,
                Title = feed.Feed.Title.Text,
                FeedUrl = feed.Feed.Links.FirstOrDefault()?.Uri.AbsoluteUri ?? string.Empty,
                ImageUrl = feed.Image?.Href ?? feed.Feed.ImageUrl?.AbsoluteUri,
                Description = DescriptionSanitizer(feed.Summary ?? feed.Feed.Description.Text),
                LastUpdated = feed.Feed.LastUpdatedTime.Date,
                YoutubeType = YoutubeType.None,
                Platform = Platform.iTunes
            };
        }
        public static Feed FeedToDbObject(OmnyFeed feed, out List<Author> authors, out List<Episode> episodes)
        {
            authors = [.. feed.Feed.Authors.Select(x => new Author { Name = x.Name, Email = x.Email, Uri = x.Uri })];
            episodes = GetItems(feed.Items).ToList();
            return new Feed
            {
                CreatorId = feed.Feed.Id,
                Title = feed.Feed.Title.Text,
                ChannelId = $"{feed.OrganizationId}/{feed.ProgramId}",
                PlaylistId = feed.PlaylistId.ToString(),
                FeedUrl = $"https://www.omnycontent.com/d/playlist/{feed.OrganizationId}/{feed.ProgramId}/{feed.PlaylistId}/podcast.rss",
                ImageUrl = feed.Image?.Href ?? feed.Feed.ImageUrl?.AbsoluteUri,
                Description = DescriptionSanitizer(feed.Summary ?? feed.Feed.Description?.Text),
                LastUpdated = feed.Feed.LastUpdatedTime.Date,
                YoutubeType = YoutubeType.None,
                Platform = Platform.Omny
            };
        }
        private static string DescriptionSanitizer(string? description)
        {
            if (string.IsNullOrWhiteSpace(description)) return string.Empty;
            return description.Replace("<![CDATA[", "").Replace("]]>", "").Trim();
        }
    }
}
