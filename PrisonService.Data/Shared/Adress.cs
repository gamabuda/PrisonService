using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonService.Data.Shared
{
    public class Adress
    {
        public Adress(string title) 
        {
            Id = Guid.NewGuid().ToString();
            Title = title;
        }

        public string Id { get; }
        public string Title { get; }

        public override string ToString()
        {
            return Title;
        }
    }
}
