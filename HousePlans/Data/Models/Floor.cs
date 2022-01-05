namespace HousePlans.Data.Models
{
    public class Floor :BaseModel<int>
    {
        public int Number { get; set; }

        public IEnumerable<Room> Rooms { get; set; }
    }
}
