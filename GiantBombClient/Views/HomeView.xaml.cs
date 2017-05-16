using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using GiantBombClient.Models;
using GiantBombClient.Utilities;
using GiantBombClient.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GiantBombClient.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomeView : Page
    {
        public HomeView()
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            NavigationCacheMode = NavigationCacheMode.Enabled;
            HomeViewModel = new HomeViewModel();
            InitializeComponent();
        }

        public HomeViewModel HomeViewModel { get; set; }

        private void VideoClicked(object sender, ItemClickEventArgs e)
        {
            var video = e.ClickedItem as VideoModel;
            var quality = (QualityOptions.SelectedItem as ComboBoxItem)?.Name;

            var args = new VideoViewArgs(video, quality);
            var rootFrame = Window.Current.Content as Frame;
            rootFrame?.Navigate(typeof(VideoView), args);
        }

        private void CommandBarClicked(object sender, RoutedEventArgs e)
        {
            var button = sender as AppBarButton;

            if (button == RefreshButton)
            { 
                HomeViewModel.CurrentShow.Videos.Clear();
            }
        }
    }
}
