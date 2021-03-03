namespace Sharedo.Api.Services.Models
{
    public class GetWorkItemSearchRequest
    {
        public Paging Page { get; set; }
        public GetWorkItemTypes Types { get; set; }
        public GetWorkItemPhases Phase { get; set; }

        public GetWorkItemSearchRequest()
        {
            Page = new Paging(50, 1);
            Types = new GetWorkItemTypes();
            Phase = new GetWorkItemPhases();
        }

        public GetWorkItemSearchRequest ForType(string workItemType)
        {
            Types.IncludeTypeAndDescendants(workItemType);
            return this;
        }
    }
}