using System.Collections.Generic;

namespace Sharedo.Api.Services.Models
{
    public class GetWorkItemResponse
    {
        public int TotalCount { get; set; }
        public List<GetWorkItemResponseResult> Results { get; set; }

        public GetWorkItemResponse()
        {
            Results = new List<GetWorkItemResponseResult>();
        }
    }
}