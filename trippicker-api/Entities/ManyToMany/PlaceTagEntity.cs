namespace trippicker_api.Entities.ManyToMany
{
    public class PlaceTagEntity
    {
        public int PlaceId { get; set; }
        public PlaceEntity Place { get; set; }
        public int TagId { get; set; }
		public TagEntity Tag { get; set; }
    }
}
