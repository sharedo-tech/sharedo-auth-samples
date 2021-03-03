using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sharedo.Api.Security;
using Sharedo.Api.Security.Models;
using Sharedo.OutlookSample.Properties;
using Sharedo.OutlookSample.Services.Models;

namespace Sharedo.OutlookSample.Services
{
    /// <summary>
    /// Responsible for maintaining the security tokens and configuration
    /// of the link to sharedo. This controller obtains tokens, persists tokens
    /// and configuration, and exposes events to the application when the link is
    /// activated and/or broken.
    /// </summary>
    public class TokenManagerUiController : ITokenManagerUiController
    {
        private readonly ITokenClient _tokenClient;

        private TokenStatus _state;
        private string _refreshToken;
        private string _accessToken;
        private DateTime _accessTokenExpiry;

        /// <summary>
        /// Event that can be subscribed to be notified when the sharedo link is
        /// established and when it is lost or deactivated.
        /// </summary>
        public event SharedoLinkStateChangedEventHandler SharedoLinkStateChanged;

        /// <summary>
        /// Determines the state of the integration to sharedo
        /// </summary>
        public TokenStatus State
        {
            get { return _state; }
        }

        /// <summary>
        /// Handling method to set the state of the tokens
        /// </summary>
        private async Task SetState(TokenStatus value)
        {
            if (_state == value) return;
            _state = value;

            // State has changed
            await GetUserInfoAsync();

            // Invoke event handlers
            SharedoLinkStateChanged?.Invoke(_state);
        }

        /// <summary>
        /// The configured base URL for the sharedo APIs
        /// </summary>
        public string SharedoUrl { get; private set; }

        /// <summary>
        /// The configured base URL for the sharedo identity server APIs
        /// </summary>
        public string IdentityUrl { get; private set; }

        /// <summary>
        /// The current user identity, retrieved from the identity server
        /// </summary>
        public string UserIdentity { get; private set; }

        /// <summary>
        /// The current user name, retrieved from the identity server
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Determines if the integration with sharedo has been configured or not.
        /// </summary>
        public bool IsLinkConfigured
        {
            get { return !string.IsNullOrWhiteSpace(SharedoUrl) && !string.IsNullOrWhiteSpace(IdentityUrl); }
        }

        /// <summary>
        /// Creates
        /// </summary>
        public TokenManagerUiController(ITokenClient tokenClient)
        {
            _tokenClient = tokenClient;
        }

        /// <summary>
        /// Sets up the sharedo and identity urls.
        /// </summary>
        public void ConfigureLink(string sharedoUrl, string identityUrl)
        {
            SharedoUrl = sharedoUrl;
            IdentityUrl = identityUrl;
            Persist();
        }

        /// <summary>
        /// Load the tokens and configuration from settings
        /// </summary>
        public async Task LoadAsync()
        {
            SharedoUrl = Settings.Default.SharedoUrl;
            IdentityUrl = Settings.Default.IdentityUrl;
            _refreshToken = Settings.Default.RefreshToken;
            _accessToken = Settings.Default.AccessToken;
            _accessTokenExpiry = Settings.Default.AccessTokenExpiry;

            await GetAccessTokenAsync();
        }

        /// <summary>
        /// Save the tokens and configuration to settings
        /// </summary>
        private void Persist()
        {
            Settings.Default.SharedoUrl = SharedoUrl;
            Settings.Default.IdentityUrl = IdentityUrl;
            Settings.Default.RefreshToken = _refreshToken;
            Settings.Default.AccessToken = _accessToken;
            Settings.Default.AccessTokenExpiry = _accessTokenExpiry;
            Settings.Default.Save();
        }

        /// <summary>
        /// Sets or updates the refresh and access tokens
        /// </summary>
        private void UpdateTokens(string refresh, string access, DateTime expires)
        {
            _refreshToken = refresh;
            _accessToken = access;
            _accessTokenExpiry = expires;
            Persist();
        }

        /// <summary>
        /// Creates an authorisation code login url for this client
        /// </summary>
        public string CreateAuthorisationCodeUrl()
        {
            return _tokenClient.CreateAuthorisationCodeUrl(IdentityUrl, App.ClientId, App.ClientRedirectUrl);
        }

        /// <summary>
        /// Parses an authorisation response dictionary (intercepted from the result of a
        /// login initiated from CreateAuthorisationCodeUrl) into an AuthorisationResponse
        /// strongly typed object (containing code and error conditions)
        /// </summary>
        public AuthorisationResponse ParseAuthorisationResponse(Dictionary<string, string> parameters)
        {
            return _tokenClient.ParseResponse(parameters);
        }

        /// <summary>
        /// Removes the tokens from storage and, if a valid refresh token
        /// was present, revokes the token so it can no longer be used.
        /// </summary>
        public async Task RevokeTokensAsync()
        {
            if (!string.IsNullOrWhiteSpace(_refreshToken) || !string.IsNullOrEmpty(_accessToken))
            {
                await _tokenClient.RevokeTokensAsync(IdentityUrl, App.ClientId, App.ClientSecret, _accessToken, _refreshToken);
            }

            UpdateTokens(null, null, default(DateTime));
            await SetState(TokenStatus.NoTokens);
        }

        /// <summary>
        /// Exchanges an authorisation code response for valid access and refresh tokens,
        /// which are then persisted to settings. Returns false if there was a problem
        /// exchanging the auth code for tokens.
        /// </summary>
        public async Task<bool> GetTokensFromAuthorisationCodeResponseAsync(AuthorisationResponse response)
        {
            var tokenResponse = await _tokenClient.RequestAccessAndRefreshTokenFromAuthCodeAsync(IdentityUrl, App.ClientId, App.ClientSecret, response.Code, App.ClientRedirectUrl);

            if (tokenResponse.IsError)
            {
                await RevokeTokensAsync();
                return false;
            }

            UpdateTokens(tokenResponse.RefreshToken, tokenResponse.AccessToken, DateTime.Now.AddSeconds(tokenResponse.ExpiresIn));
            await SetState(TokenStatus.Success);

            return true;
        }

        /// <summary>
        /// Ensures the current access token has not expired yet, obtaining a new one if it has
        /// using the refresh token. Providing that is also valid...
        /// </summary>
        public async Task<string> GetAccessTokenAsync()
        {
            // Not linked or no refresh token
            if (!IsLinkConfigured)
            {
                await SetState(TokenStatus.LinkNotConfigured);
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(_accessToken) && string.IsNullOrWhiteSpace(_refreshToken))
            {
                await SetState(TokenStatus.NoTokens);
                return string.Empty;
            }

            // Access token that is still valid
            if (!string.IsNullOrWhiteSpace(_accessToken) && _accessTokenExpiry > DateTime.Now)
            {
                await SetState(TokenStatus.Success);
                return _accessToken;
            }

            // Access token needs to be refreshed - we may also obtain a new refresh token depending on sliding window
            var tokenResponse = await _tokenClient.RequestNewTokensFromRefreshTokenAsync(IdentityUrl, App.ClientId, App.ClientSecret, _refreshToken);
            if (tokenResponse.IsError)
            {
                await SetState(TokenStatus.RefreshTokenInvalid);
                return string.Empty;
            }

            UpdateTokens(tokenResponse.RefreshToken, tokenResponse.AccessToken, DateTime.Now.AddSeconds(tokenResponse.ExpiresIn));
            await SetState(TokenStatus.Success);
            return _accessToken;
        }

        private async Task GetUserInfoAsync()
        {
            // This is called whenever the token state changes - it loads or clears the user info
            var accessToken = await GetAccessTokenAsync();
            if (State == TokenStatus.Success)
            {
                // Access token is valid, call the user info endpoint
                var userInfo = await _tokenClient.GetUserInfoFromAccessToken(IdentityUrl, accessToken);
                if (userInfo != null)
                {
                    UserIdentity = string.Format("{0}:{1}", userInfo.IdentityProvider, userInfo.Identity);
                    UserName = string.Join(" ", userInfo.FirstName, userInfo.Surname);
                    return;
                }
            }

            // Not valid - clear user info, we're no longer valid
            UserIdentity = "Not signed in";
            UserName = "Not signed in";
        }
    }
}