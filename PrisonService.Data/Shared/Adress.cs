using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonService.Data.Shared
{
    [BsonIgnoreExtraElements]
    public class Adress
    {
        public Adress(string title)
        {
            Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            Title = title;
        }
        [BsonId]
        public string Id { get; }
        public string Title { get; }

        public override string ToString()
        {
            return Title;
        }
    }
}