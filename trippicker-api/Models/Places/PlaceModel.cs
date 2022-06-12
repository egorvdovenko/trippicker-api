using System.Collections.Generic;
using trippicker_api.Models.Files;

namespace trippicker_api.Models.Places
{
    public class PlaceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<int> TagsIds { get; set; }
        public List<FileItem> Images { get; set; }
    }
}
