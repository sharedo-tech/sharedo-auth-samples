using System.Collections.Generic;
using System.Threading.Tasks;
using Sharedo.Api.Security.Models;

namespace Sharedo.Api.Security
{
    public interface ITokenClient
    {
        /// <summary>
        /// Creates a HTTP URL that will initiate a user login session with callbacks to an
        /// interceptable url containing authorisation code responses.
        /// </summary>
        string CreateAuthorisationCodeUrl(string identityUrl, string clientId, string redirectUrl);

        /// <summary>
        /// Parses an authorisation response dictionary (intercepted from the result of a
        /// login initiated from CreateAuthorisationCodeUrl) into an AuthorisationResponse
        /// strongly typed object (containing code and error conditions)
        /// </summary>
        AuthorisationResponse ParseResponse(Dictionary<string, string> values);

        /// <summary>
        /// Requests access and refresh tokens from a valid authorisation code and the redirect Url
        /// it was issued to. This is used after the user has logged in via a web view, which provides
        /// the auth code. The resulting access and refresh tokens are then used instead to maintain
        /// comms with the APIs
        /// </summary>
        Task<TokenResponse> RequestAccessAndRefreshTokenFromAuthCodeAsync(string identityUrl, string clientId, string clientSecret, string authCode, string redirectUrl);

        /// <summary>
        /// Obtains a new access token (and possibly a new refresh token) using a refresh token.
        /// </summary>
        Task<TokenResponse> RequestNewTokensFromRefreshTokenAsync(string identityUrl, string clientId, string clientSecret, string refreshToken);

        /// <summary>
        /// Revokes the tokens specified
        /// </summary>
        Task RevokeTokensAsync(string identityUrl, string clientId, string clientSecret, string accessToken, string refreshToken);

        /// <summary>
        /// Gets the basic user information from the access token
        /// </summary>
        Task<UserInfo> GetUserInfoFromAccessToken(string identityUrl, string accessToken);
    }
}