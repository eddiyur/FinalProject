using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FinalProject.Data_Structures.Order;

namespace FinalProject.Data_Structures
{
    public class ProductionOrder
    {
        public string OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderDeliveryDate { get; set; }
        public ProductClass Product { get; set; }
        public OrderStatusEnum OrderStatus;

        public ProductionOrder(string orderID, DateTime orderDate, DateTime orderDeliveryDate, ProductClass product)
        {
            OrderID = orderID;
            OrderDate = orderDate;
            OrderDeliveryDate = OrderDeliveryDate;
            Product = product;
            OrderStatus = OrderStatusEnum.Approved;
        }


        public override bool Equals(object obj)
        {
            ProductionOrder productionOrder = (ProductionOrder)obj;
            return OrderID.Equals(productionOrder.OrderID);
        }


        public override int GetHashCode()
        {
            return OrderID.GetHashCode();
        }
    }//end class ProductionOrder
}
