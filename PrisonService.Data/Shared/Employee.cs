using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonService.Data.Shared
{
    public class Employee
    {
        private Employee(string id, string fullname, string post, string prison)
        {
            Id = id;
            Fullname = fullname;
            Post = post;
            Prison = prison;
        }

        private Employee()
        {   }

        public string Id { get; set; }
        public string Fullname { get; set; }
        public string Post { get; set; }
        public string Prison { get; set; }

        public static Employee Create(string id, string fullname, string post, string prison)
        {
            return new Employee(id, fullname, post, prison);
        }

        public static Employee CreateEmpty()
        {
            return new Employee();
        }
    }
}
