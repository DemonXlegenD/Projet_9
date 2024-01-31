using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHealing;

namespace NPotionType
{
    public class HyperPotion : PotionAbstract
    {
        public HyperPotion() : base("2", "Hyper potion", "Heal your pokemon", 1500, 1, 200){ }
    }
}
