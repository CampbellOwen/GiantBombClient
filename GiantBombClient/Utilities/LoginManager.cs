using System;
using System.Threading.Tasks;
using Windows.Storage;
using GiantBombClient.Models;
using GiantBombClient.Utilities.NetworkProviders;
using GiantBombClient.Utilities.Queries;
using GiantBombClient.Utilities.Requests;

namespace GiantBombClient.Utilities
{
    public sealed class LoginManager
    {
        #region Singleton Stuff

        private static readonly Lazy<LoginManager> Lazy = new Lazy<LoginManager>(() => new LoginManager());

        public static LoginManager Instance => Lazy.Value; 

        private LoginManager()
        {
            localSettings = ApplicationData.Current.LocalSettings;
        }

        #endregion

        #region Fields

        private string apiKey;
        private readonly ApplicationDataContainer localSettings;

        #endregion

        #region Properties

        public string ApiKey
        {
            get
            {
                if (apiKey != null)
                {
                    return apiKey;
                }

                object key;
                localSettings.Values.TryGetValue("apikey", out key);

                apiKey = key as string;
                return apiKey;
            }

            private set
            {
                apiKey = value;
                localSettings.Values["apikey"] = apiKey;
            }
        }

        #endregion

        #region Methods

        public bool IsLoggedIn()
        {
            return ApiKey != null;
        }

        public async Task<bool> LogIn(string code)
        {
            var query = new LoginQuery(code);
            var loginRequest = new NetworkRequest<LoginResultModel>(new DefaultNetworkProvider(), query);
            var res = await loginRequest.GetResultAsync() as LoginResultModel;

            if (res?.Status == "success")
            {
                ApiKey = res.RegToken;
                return true;
            }          

            return false;
        }

        #endregion
    }
}
