using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
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
        private ObservableCollection<ShowViewModel> shows;

        public ObservableCollection<ShowViewModel> Shows
        {
            get => shows;
            set => SetProperty(ref shows, value);
        }

        private ShowViewModel currentShow;

        public ShowViewModel CurrentShow
        {
            get => currentShow;
            set
            {
                SetProperty(ref currentShow, value);
                if (!CurrentShow.IsLoaded)
                {
                    //CurrentShow.PopulateVideos();
                }
            }
        }

        public HomeViewModel()
        {
            shows = new ObservableCollection<ShowViewModel>();
            PopulateShows();
        }

        public async void PopulateShows()
        {
            var showQuery = new ShowListQuery();
            var showRequest = new NetworkRequest<ShowListModel>(new DefaultNetworkProvider(), showQuery);
            var showList = await showRequest.GetResultAsync() as ShowListModel;

            var categoryQuery = new CategoryListQuery();
            var categoryRequest = new NetworkRequest<CategoryListModel>(new DefaultNetworkProvider(), categoryQuery);
            var categoryList = await categoryRequest.GetResultAsync() as CategoryListModel;

            showList?.Shows.Add(new VideoShowModel
            {
                ApiDetailUrl = string.Empty,
                Deck = string.Empty,
                Id = -1,
                Image = null,
                Position = -1,
                SiteDetailUrl = string.Empty,
                Title = "All Videos"
            });
            var filteredCategories =
                categoryList?.Categories.Where(
                    category => new int[] { 20, 5, 6, 11, 8, 10 }.Any(id => id == category.Id));
            if (filteredCategories != null)
            {
                foreach (var category in filteredCategories)
                {
                    showList?.Shows.Add(new VideoShowModel
                    {
                        ApiDetailUrl = category.ApiDetailUrl,
                        Deck = string.Empty,
                        Id = category.Id,
                        Image = null,
                        IsCategory = true,
                        Position = -1,
                        SiteDetailUrl = category.SiteDetailUrl,
                        Title = category.Name
                    });
                }
            }

            var filteredShows = showList?.Shows.Where(show => new int[] { 2 }.Any(i => i != show.Id)).OrderBy(show => show.Title);

            if (filteredShows == null)
            {
                return;
            }
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    Shows.Clear();
                    

                    foreach (var show in filteredShows)
                    {
                        Shows.Add(new ShowViewModel(show));
                    }
                    CurrentShow = Shows.FirstOrDefault();
                });
            //if (CurrentShow != null)
            //{
            //    PopulateVideos();
            //}

        }
    }
}
