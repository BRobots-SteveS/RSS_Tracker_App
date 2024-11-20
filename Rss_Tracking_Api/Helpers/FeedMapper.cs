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
                Description = item.Summary.Text
            });
        }
        public static IEnumerable<Episode> GetItems(IEnumerable<MediaItem> items)
        {
            return items.Select(item =>
            {
                var firstUrl = item.BaseUri.AbsolutePath;
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
                    Name = item.MediaGroup?.FirstOrDefault()?.Title ?? item.Title.Text,
                    Url = firstUrl,
                    ThumbnailUrl = firstImage,
                    EpisodeId = item.Id,
                    Published = item.PublishDate.Date,
                    LastUpdated = item.LastUpdatedTime.Date,
                    Description = item.MediaGroup?.FirstOrDefault()?.Description ?? item.Summary.Text
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
                var firstUrl = item.BaseUri.AbsolutePath;
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
                    Name = item.MediaGroup?.FirstOrDefault()?.Title ?? item.Title.Text,
                    Url = firstUrl,
                    ThumbnailUrl = firstImage,
                    EpisodeId = item.Id,
                    Published = item.PublishDate.Date,
                    LastUpdated = item.LastUpdatedTime.Date,
                    Description = item.MediaGroup?.FirstOrDefault()?.Description ?? item.Summary.Text
                };
            });
        }

        public static IEnumerable<Episode> GetItems(IEnumerable<OmnyItem> items)
        {
            return GetItems(items.Select(x => (ITunesItem)x));
        }

        public static Creator FeedToDto(SyndicationFeed feed, out List<Author> authors, out List<Episode> episodes)
        {
            authors = [.. feed.Authors.Select(x => new Author { Name = x.Name, Email = x.Email, Uri = x.Uri })];
            episodes = GetItems(feed.Items).ToList();
            return new Creator
            {
                CreatorId = feed.Id,
                FeedUrl = feed.Links.FirstOrDefault()?.Uri.AbsoluteUri ?? string.Empty,
                ImageUrl = feed.ImageUrl.AbsoluteUri,
                Description = feed.Description.Text,
                LastUpdated = feed.LastUpdatedTime.Date,
                YoutubeType = YoutubeType.None,
                Platform = Platform.Basic
            };
        }
        public static Creator FeedToDto(MediaFeed feed, out List<Author> authors, out List<Episode> episodes)
        {
            authors = [.. feed.Authors.Select(x => new Author { Name = x.Name, Email = x.Email, Uri = x.Uri })];
            episodes = GetItems(feed.Items).ToList();
            return new Creator
            {
                CreatorId = feed.Id,
                FeedUrl = feed.Links.FirstOrDefault()?.Uri.AbsoluteUri ?? string.Empty,
                ImageUrl = feed.ImageUrl.AbsoluteUri,
                Description = feed.Description.Text,
                LastUpdated = feed.LastUpdatedTime.Date,
                YoutubeType = YoutubeType.None,
                Platform = Platform.Media
            };
        }
        public static Creator FeedToDto(YoutubeFeed feed, out List<Author> authors, out List<Episode> episodes)
        {
            authors = [.. feed.Authors.Select(x => new Author { Name = x.Name, Email = x.Email, Uri = x.Uri })];
            episodes = GetItems(feed.Items).ToList();
            return new Creator
            {
                CreatorId = feed.ChannelId ?? feed.Id,
                FeedUrl = feed.Links.FirstOrDefault()?.Uri.AbsoluteUri ?? string.Empty,
                ImageUrl = feed.ImageUrl.AbsoluteUri,
                Description = feed.Description.Text,
                LastUpdated = feed.LastUpdatedTime.Date,
                YoutubeType = string.IsNullOrEmpty(feed.PlaylistId) ? YoutubeType.Playlist : YoutubeType.Channel,
                Platform = Platform.Youtube
            };
        }
        public static Creator FeedToDto(ITunesFeed feed, out List<Author> authors, out List<Episode> episodes)
        {
            authors = [.. feed.Authors.Select(x => new Author { Name = x.Name, Email = x.Email, Uri = x.Uri })];
            episodes = GetItems(feed.Items).ToList();
            return new Creator
            {
                CreatorId = feed.Id,
                FeedUrl = feed.Links.FirstOrDefault()?.Uri.AbsoluteUri ?? string.Empty,
                ImageUrl = feed.Image?.Href ?? feed.ImageUrl.AbsoluteUri,
                Description = feed.Summary ?? feed.Description.Text,
                LastUpdated = feed.LastUpdatedTime.Date,
                YoutubeType = YoutubeType.None,
                Platform = Platform.iTunes
            };
        }
        public static Creator FeedToDto(OmnyFeed feed, out List<Author> authors, out List<Episode> episodes)
        {
            authors = [.. feed.Authors.Select(x => new Author { Name = x.Name, Email = x.Email, Uri = x.Uri })];
            episodes = GetItems(feed.Items).ToList();
            return new Creator
            {
                CreatorId = feed.Id,
                FeedUrl = feed.Links.FirstOrDefault()?.Uri.AbsoluteUri ?? string.Empty,
                ImageUrl = feed.Image?.Href ?? feed.ImageUrl.AbsoluteUri,
                Description = feed.Summary ?? feed.Description.Text,
                LastUpdated = feed.LastUpdatedTime.Date,
                YoutubeType = YoutubeType.None,
                Platform = Platform.Omny
            };
        }
    }
}
