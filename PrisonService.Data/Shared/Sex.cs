using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonService.Data.Shared
{
    public class Sex
    {
        public Sex(bool IsMan)
        {
            Value = IsMan;
        }
        public bool Value { get; set; }

        public override string ToString()
        {
            if(Value)
            {
                return "Мужской";
            }
            else
            {
                return "Женский";
            }
        }
    }
}
