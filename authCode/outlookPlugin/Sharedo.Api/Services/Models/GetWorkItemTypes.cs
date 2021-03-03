using System.Collections.Generic;

namespace Sharedo.Api.Services.Models
{
    public class GetWorkItemTypes
    {
        public List<string> IncludeTypes { get; set; }
        public List<string> IncludeTypesDerivedFrom { get; set; }

        public GetWorkItemTypes()
        {
            IncludeTypes = new List<string>();
            IncludeTypesDerivedFrom = new List<string>();
        }

        public GetWorkItemTypes IncludeTypeAndDescendants(string type)
        {
            IncludeTypes.Add(type);
            IncludeTypesDerivedFrom.Add(type);
            return this;
        }
    }
}