using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using GiantBombClient.Models;
using GiantBombClient.Utilities.NetworkProviders;
using GiantBombClient.Utilities.Queries;
using GiantBombClient.Utilities.Requests;
using ViewModels;

namespace GiantBombClient.ViewModels
{
    public class ShowViewModel : NotificationBase
    {
        public VideoShowModel Show { get; set; }

        public bool IsLoaded { get; private set; }

        private ObservableCollection<VideoModel> videos;
        public ObservableCollection<VideoModel> Videos
        {
            get => videos;
            set => SetProperty(ref videos, value);
        }

        public ShowViewModel(VideoShowModel show)
        {
            Videos = new ObservableCollection<VideoModel>();
            Show = show;
            IsLoaded = false;
        }

        public async void PopulateVideos()
        {
            VideoListQuery query;
            if (Show.IsCategory)
            {
                query = new VideoListQuery(filter: string.Format("video_categories:{0}", Show.Id));
            }
            else
            {
                query = Show.Id > 0
                    ? new VideoListQuery(filter: string.Format("video_show:{0}", Show.Id))
                    : new VideoListQuery();
            }
            var request = new NetworkRequest<VideoListModel>(new DefaultNetworkProvider(), query);
            var videos = await request.GetResultAsync() as VideoListModel;

            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    this.Videos.Clear();
                    foreach (var video in videos?.Videos ?? new List<VideoModel>())
                    {
                        video.CategoryDisplay = video.VideoShow?.Title ?? video.VideoCategories.FirstOrDefault()?.Name ?? string.Empty;
                        this.Videos.Add(video);
                    }
                });
            IsLoaded = true;
        }
    }
}
