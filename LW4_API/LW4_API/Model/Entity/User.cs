using LW4_API.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LW4_API.Model.Entity
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }


        [BsonElement("username")]
        public string Username { get; set; } = string.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("passwordHash")]
        public string Password { get; set; } = string.Empty;


        [BsonElement("role")]
        public Roles Role { get; set; } = Roles.User;

        [BsonElement("refreshToken")]
        public string? RefreshToken { get; set; }

        [BsonElement("refreshTokenExpiryTime")]
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
