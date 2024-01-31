using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHealing;

namespace NPotionType
{
    public class FullRestore : PotionAbstract
    {
        public FullRestore() : base("4", "Max potion", "Heal your pokemon", 3000, 1, 200){ }
    }
}
