using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data_Structures
{
    public class PriceTable
    {
        public ProductClass Product { get; set; }
        public int Amount { get; set; }
        public double Cost { get; set; }

        public PriceTable(ProductClass product, int amount, double cost)
        {
            this.Product = product;
            Amount = amount;
            Cost = cost;
        }

        public PriceTable()
        {
        }

        public double getTotalCalculation()
        {
            return Amount * Cost;
        }

    }//end class orderDetails

}
