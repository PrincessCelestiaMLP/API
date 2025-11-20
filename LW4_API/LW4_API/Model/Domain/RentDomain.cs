using LW4_API.Model.DTO;
using Microsoft.AspNetCore.Routing.Constraints;
using MongoDB.Bson.Serialization.Attributes;

namespace LW4_API.Model.Domain
{
    public class RentDomain
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public int ParkingSpaceId { get; set; }

        public double Price { get; set; }

        public DateTime? RentStart { get; set; }

        public DateTime? RentEnd { get; set; }

        public double Sum(ParkingSpaceDTO space)
        {
            if (space == null || RentStart == null || RentEnd == null)
                return 0;

            double hours = (RentEnd.Value - RentStart.Value).TotalHours;
            return space.PriceForHour * hours;
        }
    }
}
