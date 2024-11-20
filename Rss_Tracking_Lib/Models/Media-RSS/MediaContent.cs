using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rss_Tracking_Lib.Models.Media_RSS
{
    public class MediaContent
    {
        public string Url;
        public string Type;
        public int Width = 0;
        public int Height = 0;
        public MediaPlayer? Player;
    }
    public class MediaPlayer
    {
        public string Url;
    }
}
