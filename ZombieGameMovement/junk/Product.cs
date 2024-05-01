namespace DictionaryTaskForCs
{
    internal class Product
    {
        public enum HardwareType{
            NONE,
            CPU,
            GPU,
            RAM,
            MOTHERBOARD,
            STORAGE,
            CPUCOOLER,
            PSU,
        }

        public string Name { get; }
        public HardwareType Hardware {  get; }
        public string Description { get; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public Product(string name, HardwareType hardware, string description, int price, int quantity)
        {
            Name = name;
            Hardware = hardware;
            Description = description;
            Price = price;
            Quantity = quantity;
        }
    }
}
