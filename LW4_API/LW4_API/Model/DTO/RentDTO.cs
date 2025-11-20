using MongoDB.Bson.Serialization.Attributes;

namespace LW4_API.Model.DTO
{
    public class RentDTO
    {
        public int ClientId { get; set; }

        public int ParkingSpaceId { get; set; }

        public DateTime? RentStart { get; set; }

        public DateTime? RentEnd { get; set; }
    }
}
