using System;

namespace trippicker_api.Models.Files
{
    public class FileItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Uri Url { get; set; }
    }
}
