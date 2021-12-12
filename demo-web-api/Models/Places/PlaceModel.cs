using System.Collections.Generic;

namespace demo_web_api.Models.Places
{
    public class PlaceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> TagsIds { get; set; }
    }
}
