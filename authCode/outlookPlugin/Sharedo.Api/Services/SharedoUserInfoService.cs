using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Sharedo.Api.Client;
using Sharedo.Api.Services.Models;

namespace Sharedo.Api.Services
{
    public class SharedoUserInfoService : ISharedoUserInfoService
    {
        private readonly ICoreHttpClient _http;

        public SharedoUserInfoService(ICoreHttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<UserClaim>> GetCurrentUserClaims(string sharedoUrl, string accessToken)
        {
            try
            {
                var api = SharedoUrls.Create(sharedoUrl, "/api/security/userInfo");
                var result = await _http.GetAsync<UserInfo>(api, bearerToken: accessToken);

                return result.ToFlatClaims();
            }
            catch (HttpRequestException)
            {
                return new UserClaim[0];
            }
        }
    }
}