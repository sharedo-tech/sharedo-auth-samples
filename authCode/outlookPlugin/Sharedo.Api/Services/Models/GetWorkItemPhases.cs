namespace Sharedo.Api.Services.Models
{
    public class GetWorkItemPhases
    {
        public bool IncludeOpen { get; set; }
        public bool IncludeClosed { get; set; }
        public bool IncludeRemoved { get; set; }

        public GetWorkItemPhases()
        {
            IncludeOpen = true;
            IncludeClosed = false;
            IncludeRemoved = false;
        }
    }
}