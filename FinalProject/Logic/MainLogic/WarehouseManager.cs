using OperationalTrainer.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperationalTrainer.Logic.Warehouse
{
    public class WarehouseManager
    {
        //event out of stouck
        //event out of capasity


        private Dictionary<ProductClass, double> Inventory { get; }
        public double MaxCapacity { get; }
        public double CurrentCapacity { get; set; }

        public WarehouseManager(ProductClassList ProductsList, double maxCapacity)
        {
            Inventory = new Dictionary<ProductClass, double>();
            foreach (ProductClass product in ProductsList.ProductList)
                Inventory.Add(product, 0);
            MaxCapacity = maxCapacity;
            updateCapacity();
        }

        public WarehouseManager(Dictionary<ProductClass, double> ProductsList, double maxCapacity)
        {
            Inventory = ProductsList;
            MaxCapacity = maxCapacity;
            updateCapacity();
        }

        /// <summary>
        /// Add amount of products to inventory
        /// </summary>
        /// <param name="product"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private void AddProduct(ProductClass product, int amount)
        {
            Inventory[product] += amount;
            updateCapacity();
        }

        /// <summary>
        /// Get amount of products from inventory
        /// </summary>
        /// <param name="product"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private void GetProduct(ProductClass product, int amount)
        {
            Inventory[product] -= amount;

            if (Inventory[product] < 0)
                //event
                MessageBox.Show("Warehouse Manager: " + product.ProductID + " out of stock", "Error");
            updateCapacity();
        }

        /// <summary>
        /// return true if amount smaller than product inventory
        /// </summary>
        /// <param name="product"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private bool CanGetProduct(ProductClass product, int amount)
        { return Inventory[product] >= amount; }

        /// <summary>
        ///Calculate the capacity of the inventory
        /// </summary>
        /// <returns></returns>
        private void updateCapacity()
        {
            double capacity = 0;
            foreach (KeyValuePair<ProductClass, double> product in Inventory)
                capacity = capacity + product.Key.ProductCapacity * product.Value;

            CurrentCapacity = capacity;
            if (CurrentCapacity > MaxCapacity)
            {
                //event
                MessageBox.Show("Warehouse Manager: out of Capacity", "Error");
            }
        }

        /// <summary>
        ///Extract products from inventory
        /// </summary>
        /// <param name="order"></param>
        public void GetProducts(Order order)
        {
            foreach (PriceTable priceTable in order.OrderProductsList)
                GetProduct(priceTable.Product, priceTable.Amount);
        }


        /// <summary>
        ///Add products to inventory
        /// </summary>
        /// <param name="order"></param>
        public void AddProducts(Order order)
        {
            foreach (PriceTable priceTable in order.OrderProductsList)
                AddProduct(priceTable.Product, priceTable.Amount);
        }

        /// <summary>
        /// Check if can get products from inventory
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool CanGetProducts(Order order)
        {
            foreach (PriceTable priceTable in order.OrderProductsList)
                if (!CanGetProduct(priceTable.Product, priceTable.Amount))
                    return false;
            return true;
        }

        /// <summary>
        /// Return the Amount of product in inventory
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public double GetAmount(ProductClass product)
        { return Inventory[product]; }

    }//end warehouse Class
}
