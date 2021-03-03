using System.Collections.Generic;
using System.Threading.Tasks;
using Sharedo.Api.Services.Models;

namespace Sharedo.Api.Services
{
    public interface ISharedoTaskService
    {
        Task<List<TaskLite>> GetAllTasks(string sharedoUrl, string accessToken);
    }
}