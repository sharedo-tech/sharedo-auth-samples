namespace Sharedo.OutlookSample.Services.Models
{
    public enum TokenStatus
    {
        /// <summary>
        /// We don't know the status
        /// </summary>
        Unknown,

        /// <summary>
        /// Everything was peachy, we got tokens
        /// </summary>
        Success,

        /// <summary>
        /// No tokens have yet been captured
        /// </summary>
        NoTokens,

        /// <summary>
        /// The refresh token has expired and could not be used to obtain
        /// a new access token.
        /// </summary>
        RefreshTokenInvalid,

        /// <summary>
        /// The sharedo/identity server link hasn't been configured
        /// </summary>
        LinkNotConfigured
    }
}