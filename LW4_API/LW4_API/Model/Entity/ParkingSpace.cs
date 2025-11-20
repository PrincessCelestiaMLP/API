using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Threading.Tasks;

namespace LW4_API.Model.Entity
{
    /// <summary>
    /// Модель, що представляє один елемент списку завдань (ParkingSpace).
    /// </summary>
    public class ParkingSpace
    {
        [BsonId]

        public int Id { get; set; }

   
        public int? UserId { get; set; }

        [BsonElement("IsVIP")]
        public bool IsVip { get; set; }

        [BsonElement("priceforhour")]
        public int PriceForHour { get; set; }
    }

}
