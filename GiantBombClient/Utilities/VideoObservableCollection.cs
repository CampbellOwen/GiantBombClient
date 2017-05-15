using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Gaming.Input;
using Windows.UI.Core;
using Windows.UI.Xaml.Data;
using GiantBombClient.Models;
using GiantBombClient.Utilities.NetworkProviders;
using GiantBombClient.Utilities.Queries;
using GiantBombClient.Utilities.Requests;

namespace GiantBombClient.Utilities
{
    public class VideoObservableCollection : ObservableCollection<VideoModel>, ISupportIncrementalLoading
    {
        public VideoObservableCollection(int totalSize, LoadMoreQuery query) : base()
        {
            TotalSize = totalSize;
            Query = query;
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return Task.Run(async () =>
            {
                Query.Count = (int)count;
                Query.Offset = Count;
                var data = await new NetworkRequest<VideoListModel>(new DefaultNetworkProvider(), Query).GetResultAsync() as VideoListModel;
                TotalSize = data.NumberOfTotalResults;

                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    () =>
                    {
                        foreach (var video in data.Videos)
                        {
                            this.Add(video);
                        }
                    });
                

                return new LoadMoreItemsResult {Count = (uint)data.NumberOfPageResults};
            }).AsAsyncOperation<LoadMoreItemsResult>();
        }

        public int TotalSize { get; set; }

        public bool HasMoreItems => Count < TotalSize;
        public LoadMoreQuery Query { get; set; }
    }
}
