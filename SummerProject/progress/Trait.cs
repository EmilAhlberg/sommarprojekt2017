using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerProject.achievements
{
    public class Trait
    {
        public string TraitName { get; set; }
        public float Counter { get; set; }          


        public Trait(string traitName)
        {
            TraitName = traitName;
            Counter = 0;       
        }

        public bool CheckCondition(int threshold)
        {
           if (Counter > threshold)
            {                
                return true;
            }
            return false;
        }
    }
}
