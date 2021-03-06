﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Core;
using Windows.UI.WebUI;
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

namespace GiantBombClient.Views
{
    public sealed partial class VideoView : Page
    {
        private const double SaveVideoTimeIntervalMilliseconds = 5000;

        public VideoViewModel VideoViewModel;

        private DispatcherTimer saveVideoTimeTimer;

        public VideoView()
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;

            this.VideoViewModel = new VideoViewModel();
            this.InitializeComponent();

            saveVideoTimeTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(SaveVideoTimeIntervalMilliseconds)
            };
            saveVideoTimeTimer.Tick += SaveVideoTime;

            this.VideoPlayer.MediaPlayer.MediaOpened += MediaPlayerOnMediaOpened;
        }

        private void SaveVideoTime(object sender, object e)
        {
            VideoViewModel.SaveCurrentTime(VideoPlayer.MediaPlayer.PlaybackSession.Position);
        }

        private async void MediaPlayerOnMediaOpened(MediaPlayer sender, object args)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {
                    VideoPlayer.MediaPlayer.PlaybackSession.Position = VideoViewModel.CurrentTime;
                });
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var video = e.Parameter as VideoModel;

            VideoViewModel.CurrentVideo = video;
        }

        protected override async void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {
                    VideoViewModel.SaveCurrentTime(VideoPlayer.MediaPlayer.PlaybackSession.Position);
                    VideoPlayer.MediaPlayer.Dispose();
                });
        }

        private static void OnBackRequested(object sender, BackRequestedEventArgs backRequestedEventArgs)
        {
            var rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                return;
            }

            if (rootFrame.CanGoBack)
            {
                backRequestedEventArgs.Handled = true;
                rootFrame.GoBack();
            }
        }
    }
}
