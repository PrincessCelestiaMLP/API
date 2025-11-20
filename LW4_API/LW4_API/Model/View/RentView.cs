namespace LW4_API.Model.View
{
    public class RentView
    {
        public string ClientName { get; set; } = string.Empty;   
        public string ClientSurname { get; set; } = string.Empty;

        public int ParkingSpaceId { get; set; }                 
        public bool IsVip { get; set; }
        public int PriceForHour { get; set; }

        public double Price { get; set; }
        public DateTime? RentStart { get; set; }
        public DateTime? RentEnd { get; set; }
    }
}
