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

        private VideoObservableCollection videos;
        public VideoObservableCollection Videos
        {
            get => videos;
            set => SetProperty(ref videos, value);
        }

        public ShowViewModel(VideoShowModel show)
        {
            Show = show;

            var filter = Show.IsCategory ? "video_categories:{0}" : "video_show:{0}";

            var query = Show.Id > 0
                    ? new VideoListQuery(filter: string.Format(filter, Show.Id))
                    : new VideoListQuery();
            

            Videos = new VideoObservableCollection(int.MaxValue, new LoadMoreQuery{Endpoint = query.Query.AbsoluteUri + "&limit={0}&offset={1}"});
        }
    }
}
