using MongoDB.Bson.Serialization.Attributes;

namespace LW4_API.Model.DTO
{
    public class ParkingSpaceDTO
    {
        public int? UserId { get; set; }
        public bool IsVip { get; set; }
        public int PriceForHour { get; set; }
    }
}
