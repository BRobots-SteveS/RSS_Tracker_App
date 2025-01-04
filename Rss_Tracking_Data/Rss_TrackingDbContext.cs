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
            modelBuilder.Entity<Episode>().HasData(
                new Episode
                {
                    Id = new Guid("70b71d40-99b9-4ac6-a066-e3e48bb14a23"),
                    EpisodeId = "vt_sIL3VwKU",
                    Name = "Game Theory: The SECRET Cult Of Dress To Impress... UNMASKED",
                    Url = "https://www.youtube.com/watch?v=vt_sIL3VwKU",
                    ThumbnailUrl = "https://i3.ytimg.com/vi/vt_sIL3VwKU/hqdefault.jpg",
                    Description = @"*SUBSCRIBE to Game Theory!* Don't miss a Game Theory! ► https://www.youtube.com/@GameTheory/?sub_confirmation=1 Dress to Impress is officially a horror game! There has been so much added to this game since the last time we covered it and it changes EVERYTHING we thought we knew about this Roblox experience. Where is Lana? What is Aggy's plan? Have we sufficiently impressed with our dress? Let's find out! The game ► https://www.roblox.com/games/15101393044/Dress-To-Impress ‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐ *Credits:* Writers: Eddie “NostalGamer” Robinson and Tom Robinson Editors: Tyler Mascola, Danial ""BanditRants"" Keristoufil, and Warak Sound Designer: Yosi Berman ‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐ Need Royalty Free Music for your Content? Try Epidemic Sound. Get Your 30 Day Free Trial Now ► http://share.epidemicsound.com/MatPat ‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐ #DressToImpress #DTI #Roblox #DTILore #Theory #GameTheory",
                    LastUpdated = new DateTime(2024, 11, 18, 2, 13, 52),
                    Published = new DateTime(2024, 11, 16, 18, 5, 0),
                    FeedId = new Guid("da69f989-6159-4f03-a777-e4880345e7e6")
                },
                new Episode
                {
                    Id = new Guid("829ecc03-b35e-4402-952e-0ac8f5e9339e"),
                    EpisodeId = "M1Ql42HAdSg",
                    Name = "Game Theory: Wooly SOLVES Amanda The Adventurer 2?!",
                    Url = "https://www.youtube.com/watch?v=M1Ql42HAdSg",
                    ThumbnailUrl = "https://i2.ytimg.com/vi/M1Ql42HAdSg/hqdefault.jpg",
                    Description = @"*SUBSCRIBE to SAVE Amanda!* ► https://www.youtube.com/@GameTheory/?sub_confirmation=1 Amanda The Adventurer 2 has been a SMASH hit this year, being one of YouTube's favorite games! The VHS-based horror game took the scene by storm a few years ago and never looked back! But one question has always remained... Who exactly is Wooly, Amanda's fuzzie companion. Today we answer that question... and a few others! So press play and get ready theorists! The game ► https://store.steampowered.com/app/2826800/Amanda_the_Adventurer_2/ ‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐ *Credits:* Writers: Eddie Robinson and Tom Robinson Editors: Tyler Mascola, Warak, Danial ""BanditRants"" Keristoufi, and Axellent Sound Designer: Yosi Berman ‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐ Need Royalty Free Music for your Content? Try Epidemic Sound. Get Your 30 Day Free Trial Now ► http://share.epidemicsound.com/MatPat ‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐ #AmandaTheAdventurer2 #AmandTheAdventurer #VHS #Theory #GameTheory",
                    LastUpdated = new DateTime(2024, 11, 16, 4, 13, 25),
                    Published = new DateTime(2024, 11, 9, 18, 5, 0),
                    FeedId = new Guid("da69f989-6159-4f03-a777-e4880345e7e6")
                },
                new Episode
                {
                    Id = new Guid("18f6e6a6-b2b8-4346-8145-593350ccc7da"),
                    EpisodeId = "NUXxR6xuAqM",
                    Name = "Game Theory: We FINALLY Solved Cooking Companions Biggest Secret! (Dread Weight)",
                    Url = "https://www.youtube.com/watch?v=NUXxR6xuAqM",
                    ThumbnailUrl = "https://i3.ytimg.com/vi/NUXxR6xuAqM/hqdefault.jpg",
                    Description = @"*SUBSCRIBE* To SAVE The Chompettes ► https://www.youtube.com/@GameTheory/?sub_confirmation=1 Cooking Companions was a horror visual novel game that came out a few years ago that everyone slept on! It had every element you need in a fantastic scary game, from great visuals, interesting characters, and of course… satisfying lore! So when Dread Weight, the sequel released we knew it was a game to keep an eye on. After playing through the game I feel like we’re able to finally answer some of the BIGGEST questions we were left with from Cooking Companions, so sharpen those knives theorists because it’s time to cook! ‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐ Need Royalty Free Music for your Content? Try Epidemic Sound. Get Your 30 Day Free Trial Now ► http://share.epidemicsound.com/MatPat ‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐ Credits: Writers: Tom Robinson, Taylor ""Swag Dracula"" Bailey, and Eddie “NostalGamer” Robinson Editors: Alex ""Sedge"" Sedgwick, Jerika (NekoOnigiri), Koen Verhagen, Millie Ferris, and Axellent Sound Designer: Yosi Berman Thumbnail Artist: DasGnomo ‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐‐ #DreadWeight #CookingCompanions #VisualNovel #Lore #Theory #GameTheory #Matpat",
                    LastUpdated = new DateTime(2024, 9, 24, 1, 12, 17),
                    Published = new DateTime(2024, 8, 31, 17, 5, 0),
                    FeedId = new Guid("ac3dd517-b238-4e36-a6dd-937bf6ee68e2")
                },
                new Episode
                {
                    Id = new Guid("037b4ffb-0435-467d-95df-13f3e502a593"),
                    EpisodeId = "nZWl62FVN6U",
                    Name = "Game Theory: The Grim Lore of Cooking Companions",
                    Url = "https://www.youtube.com/watch?v=nZWl62FVN6U",
                    ThumbnailUrl = "https://i3.ytimg.com/vi/nZWl62FVN6U/hqdefault.jpg",
                    Description = @"Can you EAT your clothes to SURVIVE? Check out the Food Theory ! ► https://youtu.be/5nMEJEndAV4 Theorists, not since Doki Doki Literature Club have I seen a game that makes me scared the cute character is going to come out of the computer and get me. Welcome to Cooking Companions, the sweet looking cooking game that is hiding some unimaginable horrors. You'll have to grab a snack and watch to find out what secrets I've uncovered. Be careful though, you never know what may be hiding in your food. Find out more about how to support Ukraine ► https://bit.ly/3K9HjF3 Find out more about the game here! ► https://store.steampowered.com/app/1263230/Cooking_Companions/ Get Your Theory Wear! ► https://theorywear.com/ SUBSCRIBE for every TASTY Theory! ► https://goo.gl/kQWHkJ Need Royalty Free Music for your Content? Try Epidemic Sound. Get A 30 Day Free Trial! ► http://share.epidemicsound.com/MatPat More THEORIES: FNAF, The FINAL Timeline ►► https://bit.ly/2MlHYFe FNAF, The Monster We MISSED! ►► https://youtu.be/_ygN8HLCaJg FNAF This Theory Changes Everything ► https://bit.ly/2JUQUn6 FNAF, You Were Meant To Lose ► https://youtu.be/7bn8hM9k0b0 FNAF 6, No More Secrets ► https://bit.ly/2LVCq4u Credits: Writers: Matthew Patrick and Tom Robinson Editors: Pedro Freitas, Jerika (NekoOnigiri), and Alex ""Sedge"" Sedgwick Assistant Editor: Caitie Turner (Caiterpillart) Sound Editor: Yosi Berman #CookingCompanions #Lore #CookingCompanionsLore #BabaYaga #Secrets #Scary #MatPat #Theory #GameTheory",
                    LastUpdated = new DateTime(2024, 10, 8, 4, 57, 26),
                    Published = new DateTime(2024, 4, 5, 18, 5, 55),
                    FeedId = new Guid("ac3dd517-b238-4e36-a6dd-937bf6ee68e2")
                },
                new Episode
                {
                    Id = new Guid("31aeaf26-ac82-49a1-9631-d6806d9deb08"),
                    EpisodeId = "4",
                    Name = "The Beginning of the Middle",
                    Url = "https://dts.podtrac.com/redirect.mp3/swap.fm/track/SsIrU3p3kh3dH9Mxptdp/traffic.omny.fm/d/clips/796469f9-ea34-46a2-8776-ad0f015d6beb/b9faff99-0415-4160-bdf7-b1fd0118afb7/b88517e5-d94a-4bb8-bc8c-b227016e42e2/audio.mp3?utm_source=Podcast&in_playlist=b72a6ac6-adfd-440a-ae2d-b1fd01196597",
                    ThumbnailUrl = "https://www.omnycontent.com/d/programs/796469f9-ea34-46a2-8776-ad0f015d6beb/b9faff99-0415-4160-bdf7-b1fd0118afb7/image.jpg?t=1727888672&size=Large",
                    Description = @"<p>Ur-Grall enlists his cocky nephew Dra-Ell to help destroy Golgorath, much to Drith’s disdain. Meanwhile, our heroes split up to retrieve the missing pieces of the Dread Aegis, which can turn the tide of war.</p> <p><strong>Cast list:</strong><br>Hildy the Barback: Melissa McCarthy<br>Narrator: Octavia Spencer<br>Announcer: Glenn Close<br>Gorwan the Dragon: Allison Janney<br>Drith: Jim Rash<br>Dra-ell The Usurper: Joel McHale<br>Urgrall the Horned One: Ben Falcone<br>Gerd: Sarah Baker<br>Perta: Michelle Noh<br>Mirabelle: Ana Scotney<br>Fennick: Steve Mallory<br>Irfta Deathcastle/Ilsa the Dryad: Annie Mumolo<br>Otto: Andrew Friedman<br>Queen Canessa/Wandrith: Stephanie Courtney<br>Duke Bronwyn: Nat Faxon<br>Captain G’Nash: Reno Wilson<br>Kristoff the Great: Billy Gardell<br>General Penhill: Stephen RootT<br>he Bard (and music by): Damon Jones<br>King Thymin: Cedric Yarbrough<br>King Fryman: Drew Droege<br>Town Cryer: Michael Hitchcock</p> <p> </p><p>See <a href=""https://omnystudio.com/listener"">omnystudio.com/listener</a> for privacy information.</p>",
                    Published = new DateTime(2024, 11, 15, 8, 30, 0),
                    EpisodeType = "full",
                    Duration = 1775,
                    IsExplicit = true,
                    FeedId = new Guid("d125ff8e-13e6-4adf-b280-f176fb77420d")
                },
                new Episode
                {
                    Id = new Guid("d11f3f6b-5169-49fa-8728-adbafd520a83"),
                    EpisodeId = "3",
                    Name = "The End of the Beginning",
                    Url = "https://dts.podtrac.com/redirect.mp3/swap.fm/track/SsIrU3p3kh3dH9Mxptdp/traffic.omny.fm/d/clips/796469f9-ea34-46a2-8776-ad0f015d6beb/b9faff99-0415-4160-bdf7-b1fd0118afb7/0702275d-68cc-4a42-ae94-b21f014b5e72/audio.mp3?utm_source=Podcast&in_playlist=b72a6ac6-adfd-440a-ae2d-b1fd01196597",
                    ThumbnailUrl = "https://www.omnycontent.com/d/programs/796469f9-ea34-46a2-8776-ad0f015d6beb/b9faff99-0415-4160-bdf7-b1fd0118afb7/image.jpg?t=1727888672&size=Large",
                    Description = @"<p>As Ur-Grall and Drith make plans to turn Golgorath into a lake of fire, Hildy and friends strategize with the Elves. Their quest continues with new allies, but they soon face a deadly encounter with the savage yet sexy Centaurs.</p> <p><strong>Cast list:</strong><br>Hildy the Barback: Melissa McCarthy<br>Narrator: Octavia Spencer<br>Announcer: Glenn Close<br>Gorwan the Dragon: Allison Janney<br>Drith: Jim Rash<br>Dra-ell The Usurper: Joel McHale<br>Urgrall the Horned One: Ben Falcone<br>Gerd: Sarah Baker<br>Perta: Michelle Noh<br>Mirabelle: Ana Scotney<br>Fennick: Steve Mallory<br>Irfta Deathcastle/Ilsa the Dryad: Annie Mumolo<br>Otto: Andrew Friedman<br>Queen Canessa/Wandrith: Stephanie Courtney<br>Duke Bronwyn: Nat Faxon<br>Captain G’Nash: Reno Wilson<br>Kristoff the Great: Billy Gardell<br>General Penhill: Stephen RootT<br>he Bard (and music by): Damon Jones<br>King Thymin: Cedric Yarbrough<br>King Fryman: Drew Droege<br>Town Cryer: Michael Hitchcock</p><p>See <a href=""https://omnystudio.com/listener"">omnystudio.com/listener</a> for privacy information.</p>",
                    Published = new DateTime(2024, 11, 8, 8, 30, 0),
                    EpisodeType = "full",
                    Duration = 1718,
                    IsExplicit = true,
                    FeedId = new Guid("d125ff8e-13e6-4adf-b280-f176fb77420d")
                },
                new Episode
                {
                    Id = new Guid("d5369e5d-e4be-44b0-96c0-5c9c9f9605c9"),
                    EpisodeId = "3",
                    Name = "Hill Giant Rumble - Opus Theos Ep. 3",
                    Url = "https://rss.com/podcasts/opustheosdnd/69301",
                    ThumbnailUrl = "https://media.rss.com/opustheosdnd/20200817_043153_ff4da72ce6b39877ae7bb29ac4b87ea4.jpg",
                    Description = @"<p>Welcome to episode 3 of our Dungeons & Dragons, tabletop RPG show, set in a modified Mythic Odysseys of Theros. These casts are from games of D&D played live on twitch weekly at twitch.tv/RazorATX. It's time to get out of the crumbling temple, but with both healers down an encounter with an injured Hill Giant and some Ogres leaves the party worse for the wear. Notes: We had an audio hiccup in the middle of the video that made us all sound deeper, but audio quality in general has improved greatly otherwise from this episode on. Cast: DM - twitch.tv/RazorATX PimpNastyRanger - twitch.tv/Jaycgee Fortilos - twitch,tv/TrumpSC ""Johnny"" - twitch.tv/JellyPeanut Kane - twitch.tv/DKane Llama - twitch.tv/YungLlama Smoot - twitch.tv/Selkie_Smooth Producer - twitch.tv/Neeina</p>",
                    Published = new DateTime(2020, 8, 28, 0, 8, 42),
                    EpisodeType = "full",
                    Duration = 7158,
                    IsExplicit = false,
                    FeedId = new Guid("a0748a81-91b4-450b-8469-3175284c1083")
                },
                new Episode
                {
                    Id = new Guid("760683c4-0894-4d6e-89e2-b8a0fa0129a9"),
                    EpisodeId = "2",
                    Name = "Exploding Corpses - Opus Theos Ep. 2",
                    Url = "https://rss.com/podcasts/opustheosdnd/66231",
                    ThumbnailUrl = "https://media.rss.com/opustheosdnd/20200817_043153_ff4da72ce6b39877ae7bb29ac4b87ea4.jpg",
                    Description = @"<p>Welcome to episode 2 of our Dungeons & Dragons, tabletop RPG show. These casts are from games of D&D played live on twitch weekly at twitch.tv/RazorATX.</p><p>The party is still a bit uneasy with one another, but working together well as they push their way through the unfamiliar structure they've been trapped inside. They encounter Smoot and have a tense exchange, then come across some terrifying undead and some strange fey that make corpses explode.</p><p>Set in the Theros D&D setting with some modifications.</p><p>Notes:</p><p>Please excuse the audio in this episode! Our equipment has improved since getting started and is better in future episodes.</p><p>Cast:</p><p>DM - twitch.tv/RazorATX</p><p>PimpNastyRanger - twitch.tv/Jaycgee</p><p>Fortilos - twitch,tv/TrumpSC</p><p>""Johnny"" - twitch.tv/JellyPeanut</p><p>Kane - twitch.tv/DKane</p><p>Llama - twitch.tv/YungLlama</p><p>Smoot - twitch.tv/Selkie_Smooth</p><p>Producer - twitch.tv/Neeina</p>",
                    Published = new DateTime(2020, 8, 17, 10, 17, 45),
                    EpisodeType = "full",
                    Duration = 12467,
                    IsExplicit = false,
                    FeedId = new Guid("a0748a81-91b4-450b-8469-3175284c1083")
                }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
