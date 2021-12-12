using System.Collections.Generic;

namespace demo_web_api.Entities
{
    public class PlaceEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TagEntity> Tags { get; set; }
    }
}
