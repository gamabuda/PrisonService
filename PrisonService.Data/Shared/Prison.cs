using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonService.Data.Shared
{
    [BsonIgnoreExtraElements]
    public class Prison
    {
        public Prison(string name)
        {
            Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            Name = name;
        }
        [BsonId]
        public string Id { get; }
        public string Name { get; set; }
    }
}
