using MongoDB.Bson.Serialization.Attributes;

namespace LW4_API.Model.DTO
{
    public class ClientDTO
    {
        public string Name { get; set; }
 
        public string Surname { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
