namespace Sharedo.Api.Security.Models
{
    /// <summary>
    /// The authorisation response
    /// </summary>
    public class AuthorisationResponse
    {
        /// <summary>
        /// The authorisation code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// If true, there was a problem obtaining an authorisation code
        /// </summary>
        public bool IsError { get; set; }

        public AuthorisationResponse()
        {
        }

        public AuthorisationResponse(string code, bool isError)
        {
            Code = code;
            IsError = isError;
        }
    }
}
