using static DictionaryTaskForCs.Product;

namespace DictionaryTaskForCs
{
    internal class InventoryManager
    {
        public Dictionary<int, Product> Inventory { get; private set; }

        public InventoryManager()
        {
            Inventory = new()
            {
                {5, new Product("name5", HardwareType.GPU, "desc5", 900, 1400)},
                {1, new Product("name1", HardwareType.GPU, "desc1", 500, 1000)},
                {2, new Product("name2", HardwareType.CPU, "desc2", 600, 1100)},
                {3, new Product("name3", HardwareType.RAM, "desc3", 700, 1200)},
                {4, new Product("name4", HardwareType.RAM, "desc4", 800, 1300)},
            };
        }

        public void ListAllItems()
        {
            foreach (var item in Inventory)
            {
                Console.WriteLine(item.Key + "\t" + item.Value.Name + "\t" + item.Value.Description + "\t" + item.Value.Price + "\t" + item.Value.Quantity);
            }
        }
        public void AddItem()
        {
            Console.Write("Enter a product name: ");
            string name = Console.ReadLine()!;

            Console.Write("Enter a product hardware type: ");
            if (!Enum.TryParse(Console.ReadLine(), true, out Product.HardwareType hardware))
            {
                Console.WriteLine("Type doesn't exist.");
                return;
            }

            Console.Write("Enter a product description: ");
            string description = Console.ReadLine()!;

            Console.Write("Enter a product price: ");
            if (!Int32.TryParse(Console.ReadLine(), out int price))
            {
                Console.WriteLine("NaN");
                return;
            }

            Console.Write("Enter a product quantity: ");
            if (!Int32.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("NaN");
                return;
            }


            Inventory.Add(Inventory.Count + 1, new Product(name, hardware, description, price, quantity));
        }
        public void ListFilteredItems()
        {
            Console.Write("Hardware type: ");

            if (Enum.TryParse(Console.ReadLine(), true, out Product.HardwareType hardware))
            {
                foreach (Product product in Inventory.Values)
                {
                    if (product.Hardware.Equals(hardware))
                    {
                        Console.WriteLine(product.Name + " " + product.Hardware + " " + product.Price);
                    }
                }
            }
            else
            {
                Console.WriteLine("Error!");
            }
        }
        public void RemoveItem()
        {
            ListAllItems();

            Console.WriteLine("Type the number of the product you want to delete.");
            if (!Int32.TryParse(Console.ReadLine(), out int index))
            {
                Console.WriteLine("Nan");
            }

            Inventory.Remove(index);
        }
        public void RemoveAllItems()
        {
            Inventory.Clear();
        }
        public void CalculateTotalCost(double tax)
        {
            foreach (Product product in Inventory.Values)
            {
                Console.WriteLine(product.Name + " " + (product.Price + product.Price * tax));
            }

        }

        public void UpdatePrice()
        {
            ListAllItems();

            Console.Write("Type the number of the product you want to update: ");
            if (!Int32.TryParse(Console.ReadLine(), out int index))
            {
                Console.WriteLine("NaN");
                return;
            }

            Console.Write("Type in the updated price: ");
            if (!Int32.TryParse(Console.ReadLine(), out int price))
            {
                Console.WriteLine("NaN");
                return;
            }

            Inventory[index - 1].Price = price;
        }

        public void UpdateQuantity()
        {
            ListAllItems();

            Console.Write("Type the number of the product you want to update: ");
            if (!Int32.TryParse(Console.ReadLine(), out int index))
            {
                Console.WriteLine("NaN");
                return;
            }

            Console.Write("Type in the updated quantity: ");
            if (!Int32.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("NaN");
                return;
            }

            Inventory[index - 1].Quantity = quantity;
        }
    }
}
