using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LW4_API.Model.Entity
{
    public class Rent
    {
        [BsonId]

        public int Id { get; set; }


        public int ClientId { get; set; }


        public int ParkingSpaceId { get; set; }

        [BsonElement("Price")]
        public int Price { get; set; }

        [BsonElement("RentStart")]
        public DateTime? RentStart { get; set; }

        [BsonElement("RentEnd")]
        public DateTime? RentEnd { get; set; }
    }
}
