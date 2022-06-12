using trippicker_api.Entities.ManyToMany;
using System.Collections.Generic;

namespace trippicker_api.Entities
{
    public class PlaceEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<PlaceTagEntity> PlaceTags { get; set; }
        public List<FileEntity> Images { get; set; }
    }
}
