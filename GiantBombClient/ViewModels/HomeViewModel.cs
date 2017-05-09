using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GiantBombClient.Models;
using GiantBombClient.Utilities;
using GiantBombClient.Utilities.NetworkProviders;
using GiantBombClient.Utilities.Queries;
using GiantBombClient.Utilities.Requests;
using GiantBombClient.Views;
using ViewModels;

namespace GiantBombClient.ViewModels
{
    public class HomeViewModel : NotificationBase
    {
        private ObservableCollection<VideoModel> videos;

        public ObservableCollection<VideoModel> Videos
        {
            get => videos;
            set => SetProperty(ref videos, value);
        }

        public HomeViewModel()
        {
            videos = new ObservableCollection<VideoModel>();
            PopulateVideos();
        }

        private async void PopulateVideos()
        {
            var query = new VideoListQuery();
            var request = new NetworkRequest<VideoListModel>(new DefaultNetworkProvider(), query);
            var videosResult = await request.GetResultAsync() as VideoListModel;
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    foreach (var video in videosResult?.Videos)
                    {
                        this.Videos.Add(video);
                    }
                });
        }
    }
}
