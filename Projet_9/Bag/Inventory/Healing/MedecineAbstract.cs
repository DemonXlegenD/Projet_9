using NInventory;
using System;
namespace NHealing
{
    public abstract class MedecineAbstract : ItemAbstract
    {
        public string Effect { get; set; }
        public int HealingPower { get; set; }

        public MedecineAbstract() : base()
        {

        }

        public MedecineAbstract(string id, string name, string description, int price, int quantity, string effect, int healingPower)
            : base(id, name, description, price, quantity)
        {
            Effect = effect;
            HealingPower = healingPower;
        }

        public override void DisplayItemDetails()
        {
            base.DisplayItemDetails();
            Console.WriteLine($"Effect: {Effect}");
            Console.WriteLine($"Healing Power: {HealingPower}");
        }
    }
}
