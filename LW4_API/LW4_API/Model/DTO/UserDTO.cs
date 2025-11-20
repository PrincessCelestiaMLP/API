using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LW4_API.Model.DTO
{
    public class UserDTO
    {

        [BsonElement("username")]
        public string Username { get; set; } = string.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("passwordHash")]
        public string Password { get; set; } = string.Empty;
    }
}
