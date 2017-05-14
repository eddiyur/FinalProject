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
            SupplierOrder
        };

        public enum OrderStatusEnum
        {
            open,
            approved,
            canceled,
            delivered
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
    }
}
