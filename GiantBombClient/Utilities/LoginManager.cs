using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiantBombClient.Utilities
{
    public sealed class LoginManager
    {
        #region Singleton Stuff

        private static readonly Lazy<LoginManager> lazy = new Lazy<LoginManager>(() => new LoginManager());

        public static LoginManager Instance { get { return lazy.Value; } }

        private LoginManager()
        {

        }

        #endregion

        #region Fields

        #endregion

        #region Properties

        public string ApiKey { get; private set; }

        #endregion

        #region Methods

        #endregion
    }
}
