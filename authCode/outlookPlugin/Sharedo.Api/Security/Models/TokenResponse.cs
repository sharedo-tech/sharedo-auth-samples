using Newtonsoft.Json;

namespace Sharedo.Api.Security.Models
{
    public class TokenResponse
    {
        public static TokenResponse Error()
        {
            return new TokenResponse
            {
                RefreshToken = null,
                AccessToken = null,
                ExpiresIn = 0,
                IsError = true
            };
        }

        [JsonIgnore]
        public bool IsError { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}