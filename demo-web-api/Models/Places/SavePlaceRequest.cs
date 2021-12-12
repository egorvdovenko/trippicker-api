using System.Collections.Generic;

namespace demo_web_api.Models.Places
{
    public class SavePlaceRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> TagsIds { get; set; }
    }
}
