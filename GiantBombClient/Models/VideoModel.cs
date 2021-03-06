﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GiantBombClient.Models
{
    public class VideoModel : IDataModel
    {
        [JsonProperty("api_detail_url")]
        public string ApiDetailUrl { get; set; }
        [JsonProperty("deck")]
        public string Deck { get; set; }
        [JsonProperty("hd_url")]
        public string HdUrl { get; set; }
        [JsonProperty("high_url")]
        public string HighUrl { get; set; }
        [JsonProperty("low_url")]
        public string LowUrl { get; set; }
        [JsonProperty("embed_player")]
        public string EmbedPlayer { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("length_seconds")]
        public int LengthSeconds
        {
            get => (int)FormattedLength.TotalSeconds;
            set => FormattedLength = TimeSpan.FromSeconds(value);
        }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("publish_date")]
        public string PublishDate
        {
            get => FormattedDate.ToString();
            set
            {
                var unformatted = DateTime.Parse(value);
                var pacific = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
                FormattedDate = TimeZoneInfo.ConvertTime(unformatted, pacific, TimeZoneInfo.Utc);
            } 
        }
        [JsonProperty("site_detail_url")]
        public string SiteDetailUrl { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("image")]
        public ImageModel Image { get; set; }
        [JsonProperty("user")]
        public string User { get; set; }
        [JsonProperty("video_type")]
        public string VideoType { get; set; }
        [JsonProperty("video_show")]
        public VideoShowModel VideoShow { get; set; }
        [JsonProperty("video_categories")]
        public IList<VideoCategoryModel> VideoCategories { get; set; }
        [JsonProperty("youtube_id")]
        public object YoutubeId { get; set; }

        public string CategoryDisplay { get; set; }
        public TimeSpan FormattedLength { get; set; }
        public DateTimeOffset FormattedDate { get; set; }
    }

}
