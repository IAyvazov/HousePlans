namespace HousePlans.Data.Models
{
    public class Floor : BaseModel<int>
    {
        public Floor()
        {
            this.Rooms = new HashSet<Room>();
        }

        public int Number { get; set; }

        public int HouseId { get; set; }

        public Building House { get; set; }

        public IEnumerable<Room> Rooms { get; set; }
    }
}
