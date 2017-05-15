using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using GiantBombClient.Models;
using GiantBombClient.Utilities;
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

        private VideoObservableCollection videos;
        public VideoObservableCollection Videos
        {
            get => videos;
            set => SetProperty(ref videos, value);
        }

        public ShowViewModel(VideoShowModel show)
        {
            Show = show;
            IsLoaded = false;

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

            Videos = new VideoObservableCollection(int.MaxValue, new LoadMoreQuery{Endpoint = query.Query.AbsoluteUri + "&limit={0}&offset={1}"});
            
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
                        this.Videos.Add(video);
                    }
                });
            
            IsLoaded = true;
        }
    }
}
