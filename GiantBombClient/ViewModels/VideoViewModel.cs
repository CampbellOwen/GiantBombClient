using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Media.PlayTo;
using GiantBombClient.Models;
using GiantBombClient.Utilities;
using GiantBombClient.Utilities.Extensions;
using GiantBombClient.Utilities.NetworkProviders;
using GiantBombClient.Utilities.Queries;
using GiantBombClient.Utilities.Requests;
using ViewModels;

namespace GiantBombClient.ViewModels
{
    public class VideoViewModel : NotificationBase
    {
        private VideoModel currentVideo;

        public VideoModel CurrentVideo
        {
            get => currentVideo;
            set 
            {
                SetProperty(ref currentVideo, value);
                VideoSource = CreateMediaPlaybackItemFromVideo(value);
                GetSavedTime().LogAndContinueOnFaulted();
            }
        }

        private MediaPlaybackItem videoSource;
        public MediaPlaybackItem VideoSource
        {
            get => videoSource;
            set => SetProperty(ref videoSource, value);
        }

        private TimeSpan currentTime;
        public TimeSpan CurrentTime
        {
            get => currentTime;
            set => SetProperty(ref currentTime, value);
        }

        public VideoViewModel()
        {
            CurrentTime = TimeSpan.Zero;
        }

        private MediaPlaybackItem CreateMediaPlaybackItemFromVideo(VideoModel video)
        {
            var videoUri = new Uri((video?.HdUrl ?? video?.HighUrl ?? video?.LowUrl) + "?api_key=" +
                                   LoginManager.Instance.ApiKey);
            var mediaSource = MediaSource.CreateFromUri(videoUri);
            var mediaPlaybackItem = new MediaPlaybackItem(mediaSource);
            return mediaPlaybackItem;
        }

        private async Task GetSavedTime()
        {
            var query = new SavedVideoTimeQuery(CurrentVideo.Id);
            var request = new NetworkRequest<SavedVideoTimeModel>(new DefaultNetworkProvider(), query);

            var time = await request.GetResultAsync() as SavedVideoTimeModel;

            
            CurrentTime = TimeSpan.FromSeconds(Convert.ToDouble(time?.Time ?? "0"));
        }

        public async Task SaveCurrentTime(TimeSpan time)
        {
            var query = new StoreVideoTimeQuery(CurrentVideo.Id, time.TotalSeconds);
            var request = new NetworkRequest<StoreVideoTimeResultModel>(new DefaultNetworkProvider(), query);
            await request.GetResultAsync();
        }
    }
}
