using System;

namespace NInventory
{
    public abstract class ItemAbstract
    {
        protected string _id;
        protected string _name;
        protected string _description;
        protected int _price;
        protected int _quantity;
        protected int _totalPrice;

        public string Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name;} set { _name = value; } }
        public string Description { get { return _description;} set { _description = value; } }
        public int Price { get { return _price; } set { _price = value; } }
        public int Quantity { get { return _quantity; } set { _quantity = value; } }
        public int TotalPrice { get { return _totalPrice;} set { _totalPrice = value; } }

        protected ItemAbstract()
        {
            Id = "0";
            Name = "Item";
            Description = "This a plain item";
            Price = 10;
            Quantity = 1;
            TotalPrice = Price * Quantity;
        }

        protected ItemAbstract(string id, string name, string description, int price, int quantity)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
            TotalPrice = price * quantity;
        }

        public virtual void DisplayItemDetails()
        {
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine($"Price: {Price}");
            Console.WriteLine($"Quantity: {Quantity}");
            Console.WriteLine($"TotalPrice: {TotalPrice}");
        }
    }
}
