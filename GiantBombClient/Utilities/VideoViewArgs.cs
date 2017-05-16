using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiantBombClient.Models;

namespace GiantBombClient.Utilities
{
    public class VideoViewArgs
    {
        public string Quality { get; set; }
        public VideoModel Video { get; set; }

        public VideoViewArgs(VideoModel video, string quality)
        {
            Video = video;
            Quality = quality;
        }
    }
}
