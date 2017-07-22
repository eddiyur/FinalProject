using OperationalTrainer.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperationalTrainer.Logic.Warehouse
{
    public class WarehouseClass
    {
        //event out of stouck
        //event out of capasity
        // can get?
        //can delivere?


        private Dictionary<ProductClass, double> Inventory { get; }
        public double MaxCapacity { get; }
        public double Capacity { get; set; }

        public WarehouseClass(ProductClassList ProductsList, double maxCapacity)
        {
            Inventory = new Dictionary<ProductClass, double>();
            foreach (ProductClass product in ProductsList.ProductList)
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
                MessageBox.Show("Error");
            updateCapacity();
        }


        private bool CanGetProduct(ProductClass product, int amount)
        {
            return Inventory[product] >= amount;
        }

        /// <summary>
        ///Calculate the capacity of the inventory
        /// </summary>
        /// <returns></returns>
        private void updateCapacity()
        {
            double capacity = 0;
            foreach (KeyValuePair<ProductClass, double> product in Inventory)
                capacity = capacity + product.Key.ProductCapacity * product.Value;

            Capacity = capacity;
            if (Capacity > MaxCapacity)
                MessageBox.Show("Error");
        }

        /// <summary>
        ///Extract products form order
        /// </summary>
        /// <param name="order"></param>
        public void GetOrder(Order order)
        {
            foreach (PriceTable priceTable in order.OrderProductsList)
                GetProduct(priceTable.Product, priceTable.Amount);
        }


        /// <summary>
        ///Add products from order
        /// </summary>
        /// <param name="order"></param>
        public void AddOrder(Order order)
        {
            foreach (PriceTable priceTable in order.OrderProductsList)
                AddProduct(priceTable.Product, priceTable.Amount);
        }


        public bool CanGetOrder(Order order)
        {
            foreach (PriceTable priceTable in order.OrderProductsList)
                if (!CanGetProduct(priceTable.Product, priceTable.Amount))
                    return false;
            return true;
        }


    }//end warehouse Class
}
