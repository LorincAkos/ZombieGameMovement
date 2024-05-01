namespace DictionaryTaskForCs
{
    internal class Program
    {
        //TODO: Use dictionary, use 6 methods or properties, min. 20 data, plus use delegate

        public delegate void CalculateTotalCostDelegate(double tax);
        public delegate void UpdateDelegate();
        static void Main()
        {
            const double tax = 0.27;
            InventoryManager inventoryManager = new();
            CalculateTotalCostDelegate calculateTotalCost = inventoryManager.CalculateTotalCost;
            UpdateDelegate updatePrice = inventoryManager.UpdatePrice;
            UpdateDelegate updateQuantity = inventoryManager.UpdateQuantity;

            Help();

            while (true)
            {
                Console.Write("\nType in the number of command: ");

                string commandNumber = Console.ReadLine()!;
                Console.Write("\n");

                switch (commandNumber)
                {
                    case "0":
                        Help();
                        break;
                    case "1":
                        inventoryManager.ListAllItems();
                        break;
                    case "2":
                        inventoryManager.AddItem();
                        break;
                    case "3":
                        inventoryManager.ListFilteredItems();
                        break;
                    case "4":
                        inventoryManager.RemoveItem();
                        break;
                    case "5":
                        inventoryManager.RemoveAllItems();
                        break;
                    case "6":
                        calculateTotalCost(tax);
                        break;
                    case "7":
                        updatePrice();
                        break;
                    case "8":
                        updateQuantity();
                        break;
                    case "9":
                        return;
                    default:
                        Console.WriteLine("Command not found.");
                        break;
                }
            }
        }

        static void Help()
        {
            Console.WriteLine("0. Help");
            Console.WriteLine("1. List all items.");
            Console.WriteLine("2. Add item.");
            Console.WriteLine("3. Filter items.");
            Console.WriteLine("4. Remove a specified item.");
            Console.WriteLine("5. Remove all items.");
            Console.WriteLine("6. Calculate the total prices of the items.");
            Console.WriteLine("7. Update price of a specified item.");
            Console.WriteLine("8. Update quantity of a specified item.");
            Console.WriteLine("9. Exit.");
        }
    }
}
