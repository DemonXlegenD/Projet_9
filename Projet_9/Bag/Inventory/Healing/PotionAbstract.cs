using NInventory;
using System;

namespace NHealing
{
    public abstract class PotionAbstract : ItemAbstract
    {
        protected int _heal;
        public int Heal { get { return _heal; } set {  _heal = value; } }

        public PotionAbstract() : base()
        {
            _heal = 20;
        }

        public PotionAbstract(string id, string name, string description, int price, int quantity, int heal)
            : base(id, name, description, price, quantity)
        {
            _heal = heal;
        }

        public override void DisplayItemDetails()
        {
            base.DisplayItemDetails(); 
            Console.WriteLine($"Heal: {Heal}");
        }
    }
}
