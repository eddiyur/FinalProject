using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data_Structures
{
    public class PriceTable
    {
        public ProductClass Product { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }

        public PriceTable(ProductClass product, int amount, double price)
        {
            this.Product = product;
            Amount = amount;
            Price = price;
        }

        public PriceTable()
        {
        }

        public double getTotalCalculation()
        {
            return Amount * Price;
        }


        public static DataTable ToDataTable(List<PriceTable> orderProductsList)
        {
            string productName = "productName";
            string amount = "Amount";
            string price = "Price";
            string total = "Total";

            string[] HeadersNames = { productName, amount, price, total };
            return getDataTableFromPriceTable(orderProductsList, HeadersNames);
        }



        public static DataTable getDataTableFromPriceTable(List<PriceTable> orderProductsList, string[] HeadersNames)
        {

            DataTable dt = new DataTable("PriceTable");

            foreach (string header in HeadersNames)
            {
                dt.Columns.Add(header);
            }

            foreach (PriceTable priceTable in orderProductsList)
            {
                DataRow dataRow = dt.NewRow();
                dataRow[HeadersNames[0]] = priceTable.Product.ProductName;
                dataRow[HeadersNames[1]] = priceTable.Amount.ToString();
                dataRow[HeadersNames[2]] = priceTable.Price.ToString();
                dataRow[HeadersNames[3]] = priceTable.getTotalCalculation().ToString();
                dt.Rows.Add(dataRow);
            }
            return dt;
        }


    }//end class orderDetails

}
