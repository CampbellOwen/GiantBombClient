
using System;

namespace GiantBombClient.Utilities.Queries
{
    public class LoginQuery : IQuery
    {
        private const string Endpoint = "http://www.giantbomb.com/app/{0}/get-result?regCode={1}&format=json";
        //TODO Use resource file
        private const string AppName = "UnofficialDesktopClient";

        private readonly string code;

        public Uri Query => new Uri(string.Format(Endpoint, AppName, code));

        public LoginQuery(string code)
        {
            this.code = code;
        }
    }
}
