namespace Sharedo.OutlookSample.Services.Models
{
    /// <summary>
    /// Decorates the current access token (if any) with state.
    /// </summary>
    public class AccessTokenDecorator
    {
        /// <summary>
        /// Standard response indicating there are no tokens available
        /// </summary>        
        public static AccessTokenDecorator NoTokens = new AccessTokenDecorator(TokenStatus.NoTokens);
        
        /// <summary>
        /// Standard response indicating that the link to sharedo has not
        /// yet been configured.
        /// </summary>
        public static AccessTokenDecorator LinkNotConfigured = new AccessTokenDecorator(TokenStatus.LinkNotConfigured);

        /// <summary>
        /// The response to obtaining a token
        /// </summary>
        public TokenStatus Status { get; set; }

        /// <summary>
        /// The detail of the error message if Status != TokenStatus.Success
        /// </summary>
        public string ErrorDetail { get; set; }

        /// <summary>
        /// The access token that was granted
        /// </summary>
        public string Token { get; set; }

        public AccessTokenDecorator()
        {
        }

        public AccessTokenDecorator(string accessToken)
        {
            Status = TokenStatus.Success;
            Token = accessToken;
        }

        public AccessTokenDecorator(TokenStatus status, string errorDetail = null)
        {
            Status = status;
            ErrorDetail = errorDetail;
        }
    }
}