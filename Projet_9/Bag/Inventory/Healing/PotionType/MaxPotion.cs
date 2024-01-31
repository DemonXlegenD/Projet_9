using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHealing;

namespace NPotionType
{
    public class MaxPotion : PotionAbstract
    {
        public MaxPotion() : base("3", "Max potion", "Heal your pokemon", 2000, 1, 1000) { }
    }
}
