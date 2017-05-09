using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GiantBombClient.Utilities;
using GiantBombClient.Views;
using ViewModels;

namespace GiantBombClient.ViewModels
{
    public class LoginViewModel : NotificationBase
    {
        #region Fields

        private bool isLoading;
        private string textBoxContent;
        private bool isError;

        #endregion

        #region Properties
        public bool IsLoading {
            get => isLoading;
            set => SetProperty(ref isLoading, value);
        }

        public bool IsError
        {
            get => isError;
            set => SetProperty(ref isError, value);
        }

        public string TextBoxContent
        {
            get => textBoxContent;
            set => SetProperty(ref textBoxContent, value);
        }

        #endregion

        #region Methods

        public LoginViewModel()
        {
            IsLoading = false;
            IsError = false;
        }

        public void TryLogIn(string code)
        {
            TextBoxContent = "";
            IsLoading = true;
            LoginManager.Instance.LogIn(code).ContinueWith(
                async (t) =>
                {
                    await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                        () =>
                        {
                            this.IsLoading = false;
                            if (LoginManager.Instance.IsLoggedIn())
                            {
                                (Window.Current.Content as Frame)?.Navigate(typeof(HomeView));
                            }
                            else
                            {
                                IsError = true;
                            }
                        });
                });
        }

        #endregion
    }
}
