using System.Collections.Generic;

namespace trippicker_api.Models.Places
{
    public class PlaceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> TagsIds { get; set; }
    }
}
