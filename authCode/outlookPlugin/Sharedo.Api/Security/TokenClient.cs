using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Sharedo.Api.Client;
using Sharedo.Api.Security.Models;

namespace Sharedo.Api.Security
{
    /// <summary>
    /// Provides an abstraction for working with tokens and the various token APIs
    /// within the identity server.
    /// </summary>
    public class TokenClient : ITokenClient
    {
        private const string TokenEndpoint = "/connect/token";
        private const string AuthoriseEndpoint = "/connect/authorize";
        private const string RevokeEndpoint = "/connect/revocation";
        private const string UserInfoEndpoint = "/connect/userinfo";

        private readonly ICoreHttpClient _http;

        /// <summary>
        /// Constructor, providing an implementation of the ICoreHttpClient
        /// </summary>
        /// <param name="http"></param>
        public TokenClient(ICoreHttpClient http)
        {
            _http = http;
        }

        /// <summary>
        /// Creates a HTTP URL that will initiate a user login session with callbacks to an
        /// interceptable url containing authorisation code responses.
        /// </summary>
        public string CreateAuthorisationCodeUrl(string identityUrl, string clientId, string redirectUrl)
        {
            var authUri = CreateIdentityUrl(identityUrl, AuthoriseEndpoint);

            var parameters = new Dictionary<string, string>()
            {
                { "response_type", "code" },
                { "scope", "openid profile sharedo offline_access" },
                { "client_id", clientId },
                { "redirect_uri", redirectUrl },
                { "prompt", "login" },
                { "response_mode", "form_post" },
                { "state", Guid.NewGuid().ToString() },
                { "nonce", Guid.NewGuid().ToString() }
            };

            var queryString = parameters.AsQueryString();
            var url = string.Format("{0}?{1}", authUri, queryString);

            return url;
        }

        /// <summary>
        /// Parses an authorisation response dictionary (intercepted from the result of a
        /// login initiated from CreateAuthorisationCodeUrl) into an AuthorisationResponse
        /// strongly typed object (containing code and error conditions)
        /// </summary>
        public AuthorisationResponse ParseResponse(Dictionary<string, string> values)
        {
            var authCode = values.TryGet("code");
            var isError = values.ContainsKey("error");

            var result = new AuthorisationResponse(authCode, isError);
            return result;
        }

        /// <summary>
        /// Requests access and refresh tokens from a valid authorisation code and the redirect Url
        /// it was issued to. This is used after the user has logged in via a web view, which provides
        /// the auth code. The resulting access and refresh tokens are then used instead to maintain
        /// comms with the APIs
        /// </summary>
        public async Task<TokenResponse> RequestAccessAndRefreshTokenFromAuthCodeAsync(string identityUrl, string clientId, string clientSecret, string authCode, string redirectUrl)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    {"grant_type", "authorization_code"},
                    {"redirect_uri", redirectUrl},
                    {"code", authCode},
                    {"client_id", clientId},
                    {"client_secret", clientSecret}
                };

                var url = CreateIdentityUrl(identityUrl, TokenEndpoint);
                var body = new FormUrlEncodedContent(parameters);

                var responseRaw = await _http.SendRawAsync(HttpMethod.Post, url, body);
                var response = Json.Deserialise<TokenResponse>(responseRaw);

                return response;
            }
            catch (HttpRequestException)
            {
                return TokenResponse.Error();
            }
        }

        /// <summary>
        /// Revokes the tokens specified
        /// </summary>
        public async Task RevokeTokensAsync(string identityUrl, string clientId, string clientSecret, string accessToken, string refreshToken)
        {
            var url = CreateIdentityUrl(identityUrl, RevokeEndpoint);

            if (!string.IsNullOrWhiteSpace(accessToken))
                await Revoke(url, clientId, clientSecret, accessToken, "access_token");

            if (!string.IsNullOrWhiteSpace(refreshToken))
                await Revoke(url, clientId, clientSecret, refreshToken, "refresh_token");
        }

        private async Task Revoke(string url, string clientId, string clientSecret, string token, string tokenType)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    {"client_id", clientId},
                    {"client_secret", clientSecret},
                    {"token", token},
                    {"token_type_hint", tokenType}
                };

                var body = new FormUrlEncodedContent(parameters);
                await _http.SendRawAsync(HttpMethod.Post, url, body);
            }
            catch(HttpRequestException)
            {
                // Ok to swallow exception here, though should probably log the issue somewhere
            }
        }

        /// <summary>
        /// Obtains a new access token (and possibly a new refresh token) using a refresh token.
        /// </summary>
        public async Task<TokenResponse> RequestNewTokensFromRefreshTokenAsync(string identityUrl, string clientId, string clientSecret, string refreshToken)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    {"grant_type", "refresh_token"},
                    {"refresh_token", refreshToken },
                    {"client_id", clientId},
                    {"client_secret", clientSecret}
                };

                var url = CreateIdentityUrl(identityUrl, TokenEndpoint);
                var body = new FormUrlEncodedContent(parameters);

                var responseRaw = await _http.SendRawAsync(HttpMethod.Post, url, body);
                var response = Json.Deserialise<TokenResponse>(responseRaw);

                return response;
            }
            catch (HttpRequestException)
            {
                return TokenResponse.Error();
            }
        }

        private static string CreateIdentityUrl(string identityUrl, string endpoint)
        {
            var identityUri = new Uri(identityUrl);
            var authUri = new Uri(identityUri, endpoint);
            return authUri.AbsoluteUri;
        }

        /// <summary>
        /// Gets the basic user information from the access token
        /// </summary>
        public async Task<UserInfo> GetUserInfoFromAccessToken(string identityUrl, string accessToken)
        {
            try
            {
                var url = CreateIdentityUrl(identityUrl, UserInfoEndpoint);
                var response = await _http.GetAsync<UserInfo>(url, bearerToken: accessToken);

                return response;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}
