namespace Sharedo.Api.Services.Models
{
    public class GetWorkItemEnrichment
    {
        public string Path { get; set; }

        public GetWorkItemEnrichment(string dataComposerPath)
        {
            Path = dataComposerPath;
        }
    }
}