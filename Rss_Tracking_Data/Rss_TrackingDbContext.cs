using Microsoft.EntityFrameworkCore;
using Rss_Tracking_Data.Entities;

namespace Rss_Tracking_Data
{
    public class Rss_TrackingDbContext : DbContext
    {
        public DbSet<Feed> Feeds { get; set; }
        public DbSet<FeedsAuthors> FeedsAuthors { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserFavorite> UserFavorites { get; set; }
        public Rss_TrackingDbContext(DbContextOptions<Rss_TrackingDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Feed>().HasData(
                new Feed
                {
                    Id = new Guid("da69f989-6159-4f03-a777-e4880345e7e6"),
                    CreatorId = "yt:channel:o_IB5145EVNcf8hw1Kku7w",
                    Title = "The Game Theorists",
                    Description = "The Game Theorists",
                    Platform = Enums.Platform.Youtube,
                    YoutubeType = Enums.YoutubeType.Channel,
                    ChannelId = "o_IB5145EVNcf8hw1Kku7w",
                    PlaylistId = string.Empty,
                    FeedUrl = "https://www.youtube.com/feeds/videos.xml?channel_id=UCo_IB5145EVNcf8hw1Kku7w",
                    PublishDate = new DateTime(2009, 8, 22, 18, 1, 46),
                    LastUpdated = DateTime.UtcNow
                },
                new Feed
                {
                    Id = new Guid("ac3dd517-b238-4e36-a6dd-937bf6ee68e2"),
                    CreatorId = "yt:playlist:PLOl4b517qn8iG3DBk-0TSm3fcP3_zgTlf",
                    Title = "The Game Theorists",
                    Description = "Dating Sims with a Side of Horror",
                    Platform = Enums.Platform.Youtube,
                    YoutubeType = Enums.YoutubeType.Playlist,
                    ChannelId = "UCo_IB5145EVNcf8hw1Kku7w",
                    PlaylistId = "PLOl4b517qn8iG3DBk-0TSm3fcP3_zgTlf",
                    FeedUrl = "https://www.youtube.com/feeds/videos.xml?playlist_id=PLOl4b517qn8iG3DBk-0TSm3fcP3_zgTlf",
                    PublishDate = new DateTime(2024, 8, 31, 7, 38, 51),
                    LastUpdated = DateTime.UtcNow
                },
                new Feed
                {
                    Id = new Guid("d125ff8e-13e6-4adf-b280-f176fb77420d"),
                    CreatorId = "796469f9-ea34-46a2-8776-ad0f015d6beb",
                    Title = "Hildy the Barback and the Lake of Fire",
                    Description = @"When an evil force threatens to incinerate the fantastical land of Golgorath, it's up to Hildy the Barback and her friends to kick some ass on a comedic quest to save the world. Oh, also there are lots of dragons. Melissa McCarthy leads an all-star cast, including Ben Falcone, Octavia Spencer, Glenn Close, and more, in Hildy the Barback and the Lake of Fire, a legendary adventure you won’t want to miss! Featuring original music in every episode, it's a hilarious extravaganza of epic proportions. For Hildy merch, head to: https://hildy.store/ Sales and Distribution by Lemonada Media",
                    Platform = Enums.Platform.Omny,
                    YoutubeType = Enums.YoutubeType.None,
                    ChannelId = "b9faff99-0415-4160-bdf7-b1fd0118afb7",
                    PlaylistId = "b72a6ac6-adfd-440a-ae2d-b1fd01196597",
                    ImageUrl = "https://www.omnycontent.com/d/programs/796469f9-ea34-46a2-8776-ad0f015d6beb/b9faff99-0415-4160-bdf7-b1fd0118afb7/image.jpg?t=1727888672&size=Large",
                    FeedUrl = "https://www.omnycontent.com/d/playlist/796469f9-ea34-46a2-8776-ad0f015d6beb/b9faff99-0415-4160-bdf7-b1fd0118afb7/b72a6ac6-adfd-440a-ae2d-b1fd01196597/podcast.rss",
                    PublishDate = DateTime.MinValue,
                    LastUpdated = DateTime.UtcNow
                },
                new Feed
                {
                    Id = new Guid("a0748a81-91b4-450b-8469-3175284c1083"),
                    CreatorId = "Nicholas Galinski",
                    Title = "Opus Theos D&D",
                    Description = @"In a modified world of Theros, where gods interfere in the lives of mortals to advance their own agendas, six adventurers uncover mysteries and prophecies in the wake of the death of the world's greatest hero. Features popular streamers playing together in-person, including TrumpSC, Jaycgee, DKane, JellyPeanut, YungLlama, Selkie_Smooth, and RazorATX.

This podcast features recordings of actual play sessions recorded live on twitch.tv/RazorATX on Tuesdays at 6 PM Pacific time.

Please excuse poor audio in early episodes. Our audio equipment has improved as the show has gone on!",
                    Platform = Enums.Platform.iTunes,
                    YoutubeType = Enums.YoutubeType.None,
                    ChannelId = string.Empty,
                    PlaylistId = string.Empty,
                    ImageUrl = "https://media.rss.com/opustheosdnd/20200817_043153_ff4da72ce6b39877ae7bb29ac4b87ea4.jpg",
                    FeedUrl = "https://media.rss.com/opustheosdnd/feed.xml",
                    PublishDate = new DateTime(2020, 8, 28, 0, 8, 42),
                    LastUpdated = new DateTime(2023, 10, 30, 15, 40, 15)
                }
            );

            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Id = new Guid("9b2448b2-4ad0-4f1d-a781-deadc9afe03d"),
                    Name = "The Game Theorists",
                    Uri = "https://www.youtube.com/channel/UCo_IB5145EVNcf8hw1Kku7w",
                },
                new Author
                {
                    Id = new Guid("c33ac723-8757-41b1-a6f1-34ea6502281a"),
                    Name = "Lemonada Media",
                    Email = "hey@lemonadamedia.com",
                },
                new Author
                {
                    Id = new Guid("73c512fa-d771-457c-8996-54b59cdd8b6a"),
                    Name = "Nicolas Galinski",
                }
            );
            modelBuilder.Entity<FeedsAuthors>().HasData(
                new FeedsAuthors
                {
                    Id = new Guid("e1429ca7-1a3e-4145-b46b-1db03302843a"),
                    FeedId = new Guid("da69f989-6159-4f03-a777-e4880345e7e6"),
                    AuthorId = new Guid("9b2448b2-4ad0-4f1d-a781-deadc9afe03d")
                },
                new FeedsAuthors
                {
                    Id = new Guid("24fadfa7-cc5a-4575-8336-3a62c75fb49d"),
                    FeedId = new Guid("ac3dd517-b238-4e36-a6dd-937bf6ee68e2"),
                    AuthorId = new Guid("9b2448b2-4ad0-4f1d-a781-deadc9afe03d")
                },
                new FeedsAuthors
                {
                    Id = new Guid("951ede19-caab-4b44-abc1-e055de3d3288"),
                    FeedId = new Guid("d125ff8e-13e6-4adf-b280-f176fb77420d"),
                    AuthorId = new Guid("c33ac723-8757-41b1-a6f1-34ea6502281a")
                },
                new FeedsAuthors
                {
                    Id = new Guid("c942a7d7-695c-4948-b8a2-426361193d43"),
                    FeedId = new Guid("a0748a81-91b4-450b-8469-3175284c1083"),
                    AuthorId = new Guid("73c512fa-d771-457c-8996-54b59cdd8b6a")
                }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
