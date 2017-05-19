using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data_Structures
{


    public class Order
    {
        public enum OrderTypeEnum
        {
            CustomerOrder,
            SupplierOrder,
        };

        public enum OrderStatusEnum
        {
            Open,
            Approved,
            InProgress,
            Canceled,
            Delivered
        }

        public Person person { get; set; }
        public OrderTypeEnum OrderType { get; set; }
        public string OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderDeliveryDate { get; set; }
        public List<PriceTable> ProductsList { get; set; }

        public OrderStatusEnum OrderStatus;

        public Order()
        {

        }

        /// <summary>
        /// /not ready
        /// </summary>
        /// <returns></returns>
        public List<ProductionOrder> generateProductionOrdersFromCustomerOrder()
        {
            ///to develop//
            string productOrderId = "";
            ////    

            List<ProductionOrder> productionOrderList = new List<ProductionOrder>();

            foreach (PriceTable priceTable in ProductsList)
            {
                int amount = priceTable.Amount;
                for (int i = 0; i < amount; i++)
                {
                    ProductionOrder productionOrder = new ProductionOrder(productOrderId, OrderDate, OrderDeliveryDate, priceTable.Product);
                    productionOrderList.Add(productionOrder);
                }

            }

            return productionOrderList;
        }


    }//end orderClass
}
