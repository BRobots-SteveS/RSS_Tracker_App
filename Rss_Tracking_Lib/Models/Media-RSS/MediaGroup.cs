namespace Rss_Tracking_Lib.Models.Media_RSS
{
    public class MediaGroup
    {
        public string? Title;
        public string? Description;
        public List<MediaContent>? Content;
        public MediaThumbnail? Thumbnail;
        public MediaCommunity? Community;
    }
    public class MediaThumbnail
    {
        public string Url;
        public int Width = 0;
        public int Height = 0;
    }
    public class MediaCommunity
    {
        public class StarRating
        {
            public int Count = 0;
            public float average = 5.00f;
            public int min = 1;
            public int max = 5;
        }
        public class Statistics
        {
            public double Views = 0;
        }
    }
}
