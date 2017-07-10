using OperationalTrainer.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationalTrainer.Logic.Warehouse
{
    public class WarehouseClass
    {
        private Dictionary<ProductClass, double> Inventory { get; }
        public double MaxCapacity { get; }
        public double Capacity { get; set; }

        //add transakzion

        public WarehouseClass(List<ProductClass> ProductsList, double maxCapacity)
        {
            Inventory = new Dictionary<ProductClass, double>();
            foreach (ProductClass product in ProductsList)
                Inventory.Add(product, 0);
            MaxCapacity = maxCapacity;
            updateCapacity();
        }

        public WarehouseClass(Dictionary<ProductClass, double> ProductsList, double maxCapacity)
        {
            Inventory = ProductsList;
            MaxCapacity = maxCapacity;
            updateCapacity();
        }

        public bool AddProduct(ProductClass product, int amount)
        {
            if (MaxCapacity < Inventory[product]+amount)
                return false;
            Inventory[product] += amount;
            return updateCapacity();
        }

        public bool GetProduct(ProductClass product, int amount)
        {
            if (amount > Inventory[product])
                return false;
            else
                Inventory[product] -= amount;
            return updateCapacity();
        }

        private bool updateCapacity()
        {
            double capacity = 0;
            foreach (KeyValuePair<ProductClass, double> product in Inventory)
                capacity = capacity + product.Key.ProductCapacity * product.Value;

            Capacity = capacity;
            if (Capacity > MaxCapacity)
                return false;
            else return true;

        }
    }
}
