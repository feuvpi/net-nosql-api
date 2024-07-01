using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using api_reservas.Core.Interfaces;

namespace api_reservas.Core.Models.BaseModels
{
    public class BaseEntity : IBaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
