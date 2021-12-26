using System.Collections.Generic;

namespace trippicker_api.Models.Places
{
    public class SavePlaceRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> TagsIds { get; set; }
    }
}
