using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHealing;

namespace NPotionType
{
    public class SuperPotion : PotionAbstract
    {
        public SuperPotion() : base("1", "Super potion", "Heal your pokemon", 700, 1, 60){ }
    }
}
