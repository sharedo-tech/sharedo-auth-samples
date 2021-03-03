using System;

namespace Sharedo.Api.Services.Models
{
    public class TaskLite
    {
        public DateTime? DueDateTime { get; set; }
        public string Reference { get; set; }
        public string Title { get; set; }

        public string DisplayDue => DueDateTime?.ToLocalTime().ToString("r");
    }
}