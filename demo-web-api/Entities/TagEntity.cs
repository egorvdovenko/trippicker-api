using trippicker_api.Entities.ManyToMany;
using System.Collections.Generic;

namespace trippicker_api.Entities
{
    public class TagEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
		public List<PlaceTagEntity> PlaceTags { get; set; }
	}
}
