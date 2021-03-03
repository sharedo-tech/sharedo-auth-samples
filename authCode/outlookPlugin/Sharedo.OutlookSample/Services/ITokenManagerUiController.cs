using System.Collections.Generic;
using System.Threading.Tasks;
using Sharedo.Api.Security.Models;
using Sharedo.OutlookSample.Services.Models;

namespace Sharedo.OutlookSample.Services
{
    public interface ITokenManagerUiController
    {
        /// <summary>
        /// Event that can be subscribed to be notified when the sharedo link is
        /// established and when it is lost or deactivated.
        /// </summary>
        event SharedoLinkStateChangedEventHandler SharedoLinkStateChanged;

        /// <summary>
        /// Determines the state of the integration to sharedo
        /// </summary>
        TokenStatus State { get; }

        /// <summary>
        /// Determines if the integration with sharedo has been configured or not.
        /// </summary>
        bool IsLinkConfigured { get; }

        /// <summary>
        /// The configured base URL for the sharedo APIs
        /// </summary>
        string SharedoUrl { get; }

        /// <summary>
        /// The configured base URL for the sharedo identity server APIs
        /// </summary>
        string IdentityUrl { get; }

        /// <summary>
        /// The current user identity, retrieved from the identity server
        /// </summary>
        string UserIdentity { get; }

        /// <summary>
        /// The current user name, retrieved from the identity server
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Initialises the manager, loads tokens etc from settings and validates the token
        /// is valid, loads user info where necessary etc.
        /// </summary>
        /// <returns></returns>
        Task LoadAsync();

        /// <summary>
        /// Configures the URLs for the linking of the plugin to sharedo
        /// </summary>
        void ConfigureLink(string sharedoUrl, string identityUrl);

        /// <summary>
        /// Creates an authorisation code login url for this client
        /// </summary>
        string CreateAuthorisationCodeUrl();

        /// <summary>
        /// Parses an authorisation response dictionary (intercepted from the result of a
        /// login initiated from CreateAuthorisationCodeUrl) into an AuthorisationResponse
        /// strongly typed object (containing code and error conditions)
        /// </summary>
        AuthorisationResponse ParseAuthorisationResponse(Dictionary<string, string> parameters);

        /// <summary>
        /// Removes the tokens from storage and, if a valid refresh token
        /// was present, revokes the token so it can no longer be used.
        /// </summary>
        Task RevokeTokensAsync();

        /// <summary>
        /// Exchanges an authorisation code response for valid access and refresh tokens,
        /// which are then persisted to settings. Returns false if there was a problem
        /// exchanging the auth code for tokens.
        /// </summary>
        Task<bool> GetTokensFromAuthorisationCodeResponseAsync(AuthorisationResponse response);

        /// <summary>
        /// Gets the current access token, or a new one as necessary, and return it. This may also trigger
        /// the TokenStatus in State to change and therefore the SharedoLinkStateChanged event to fire.
        /// </summary>
        Task<string> GetAccessTokenAsync();
    }
}