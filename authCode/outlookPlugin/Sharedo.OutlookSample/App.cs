using System.Threading.Tasks;
using Sharedo.Api.Client;
using Sharedo.Api.Security;
using Sharedo.OutlookSample.Services;

namespace Sharedo.OutlookSample
{
    /// <summary>
    /// Manages the creation and lifetime of the key services
    /// for the application - namely the TokenManagerUiController
    /// that manages the state of the application and it's link to
    /// sharedo as a singleton.
    /// </summary>
    public static class App
    {
        public const string ClientId = "my-outlook";
        public const string ClientSecret = "not a secret";
        public const string ClientRedirectUrl = "oob://localhost/my-outlook";

        public static ITokenManagerUiController TokenManager { get; private set; }
        public static ITokenClient TokenClient { get; private set; }
        public static ICoreHttpClient HttpClient { get; private set; }

        static App()
        {
            HttpClient = new CoreHttpClient(true);
            TokenClient = new TokenClient(HttpClient);
            TokenManager = new TokenManagerUiController(TokenClient);
        }

        internal static async Task LoadAsync()
        {
            await TokenManager.LoadAsync();
        }
    }
}
