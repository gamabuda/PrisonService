using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonService.Data.Shared
{
    [BsonIgnoreExtraElements]
    public class Employee
    {
        private Employee(string number, string fullname, string post, string prison, string password)
        {
            Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            Number = number;
            Fullname = fullname;
            Post = post;
            Prison = prison;
            Password = password;
        }

        private Employee()
        {   }

        [BsonId]
        public string Id { get; set; }

        public string Number { get; set; }
        public string Fullname { get; set; }
        public string Post { get; set; }
        public string Prison { get; set; }
        public string Password { get; set; }

        public static Employee Create(string number, string fullname, string post, string prison, string password)
        {
            return new Employee(number, fullname, post, prison, password);
        }

        public static Employee CreateEmpty()
        {
            return new Employee();
        }
    }
}
