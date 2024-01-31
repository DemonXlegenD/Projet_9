using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHealing;

namespace NPotionType
{
    public class Potion : PotionAbstract
    {
        public Potion() : base("0", "Potion", "Heal your pokemon", 10, 1, 20){
            base.DisplayItemDetails();
        }


    }
}
