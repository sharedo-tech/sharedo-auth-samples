using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Sharedo.Api.Client;
using Sharedo.Api.Services.Models;

namespace Sharedo.Api.Services
{
    public class SharedoTaskService : ISharedoTaskService
    {
        private readonly ICoreHttpClient _http;

        public SharedoTaskService(ICoreHttpClient http)
        {
            _http = http;
        }
        
        public async Task<List<TaskLite>> GetAllTasks(string sharedoUrl, string accessToken)
        {
            try
            {
                var api = SharedoUrls.Create(sharedoUrl, "api/v1/public/workItem/findByQuery");
                var request = new GetWorkItemRequest()
                    .ForType("task")
                    .EnrichWith("reference", "title", "taskDueDate.date.utc.value");

                var response = await _http.PostAsync<GetWorkItemRequest, GetWorkItemResponse>(api, request, bearerToken: accessToken);
                
                var results = response.Results
                    .Select(r => new TaskLite
                    {
                        Reference = r.Data.Reference,
                        Title = r.Data.Title,
                        DueDateTime = r.Data.Due
                    }).ToList();

                return results;
            }
            catch (HttpRequestException)
            {
                return new List<TaskLite>();
            }
        }
    }
}