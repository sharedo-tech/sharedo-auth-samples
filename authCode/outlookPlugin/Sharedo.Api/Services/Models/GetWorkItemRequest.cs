using System.Collections.Generic;

namespace Sharedo.Api.Services.Models
{
    public class GetWorkItemRequest
    {
        public GetWorkItemSearchRequest Search { get; set; }
        public List<GetWorkItemEnrichment> Enrich { get; set; }

        public GetWorkItemRequest()
        {
            Search = new GetWorkItemSearchRequest();
            Enrich = new List<GetWorkItemEnrichment>();
        }
        
        public GetWorkItemRequest ForType(string workItemType)
        {
            Search.ForType(workItemType);
            return this;
        }

        public GetWorkItemRequest EnrichWith(params string[] dataComposerFields)
        {
            foreach (var field in dataComposerFields)
            {
                Enrich.Add(new GetWorkItemEnrichment(field));
            }

            return this;
        }
    }
}