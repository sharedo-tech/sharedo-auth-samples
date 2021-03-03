using System;

namespace Sharedo.Api.Services.Models
{
    public class GetWorkItemResponseResult
    {
        public Guid Id { get; set; }
        public GetWorkItemResponseResultData Data { get; set; }

        public GetWorkItemResponseResult()
        {
            Data = new GetWorkItemResponseResultData();
        }
    }
}