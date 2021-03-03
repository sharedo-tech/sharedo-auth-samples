using Newtonsoft.Json;

namespace Sharedo.Api.Security.Models
{
    public class UserInfo
    {
        [JsonProperty("idp")]    
        public string IdentityProvider { get; set; }

        [JsonProperty("sub")]
        public string Identity { get; set; }
        
        [JsonProperty("sharedo/firstname")]
        public string FirstName { get; set; }
        
        [JsonProperty("sharedo/lastname")]
        public string Surname { get; set; }
    }
}
