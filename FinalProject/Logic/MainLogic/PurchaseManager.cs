using OperationalTrainer.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Logic.MainLogic
{
    public class PurchaseManager
    {
        private OrdersList PurchaseOrders { get; set; }

        public PurchaseManager(OrdersList purchaseOrders)
        {
            PurchaseOrders = purchaseOrders;
        }

        /// <summary>
        /// Add new purchase order to purchase Orders list
        /// </summary>
        /// <param name="order"></param>
        public void AddSupplierOrder(Order order)
        { PurchaseOrders.AddOrder(order); }


        /// <summary>
        /// Extract orders with earlier delivery date than given date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public OrdersList GetPurchaseOrders(DateTime date)
        {
            //OrdersList newOrdersList = PurchaseOrders.GetOrdersByOrderDeliveryDate(date);
            //PurchaseOrders.RemoveOrders(newOrdersList);
            return PurchaseOrders.ExtractOrdersByOrderDeliveryDate(date);
        }

    

        internal OrdersList GetPurchaseOrders()
        {
            return PurchaseOrders;
        }
    }
}
