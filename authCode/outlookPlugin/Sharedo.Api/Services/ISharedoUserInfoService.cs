using System.Collections.Generic;
using System.Threading.Tasks;
using Sharedo.Api.Services.Models;

namespace Sharedo.Api.Services
{
    public interface ISharedoUserInfoService
    {
        Task<IEnumerable<UserClaim>> GetCurrentUserClaims(string sharedoUrl, string accessToken);
    }
}
