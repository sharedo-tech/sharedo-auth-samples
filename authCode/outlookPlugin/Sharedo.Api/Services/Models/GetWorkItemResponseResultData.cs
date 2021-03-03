using System;
using Newtonsoft.Json;

namespace Sharedo.Api.Services.Models
{
    public class GetWorkItemResponseResultData
    {
        public string Reference { get; set; }
        public string Title { get; set; }

        [JsonProperty("taskDueDate.date.utc.value")]
        public DateTime? Due { get; set; }
    }
}