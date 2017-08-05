using OperationalTrainer.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Logic.MainLogic
{
    public class MarketingManager
    {
        private OrdersList CustomersOrdersList { get; set; }
        private OrdersList FutureCustomersOrdersList { get; set; }


        public MarketingManager(OrdersList customersOrderList, OrdersList futureCustomersOrdersList)
        {
            CustomersOrdersList = customersOrderList;
            FutureCustomersOrdersList = futureCustomersOrdersList;
        }

        /// <summary>
        /// return CustomersOrdersList
        /// </summary>
        /// <returns></returns>
        public OrdersList GetCustomersOrdersList() { return CustomersOrdersList; }

        /// <summary>
        /// Return Order from Customers Orders List
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public Order GetCustomerOrder(string orderID)
        { return CustomersOrdersList.GetOrder(orderID); }

        /// <summary>
        /// Add order to Customers Orders List
        /// </summary>
        /// <param name="order"></param>
        public void AddCustomerOrder(Order order)
        { CustomersOrdersList.AddOrder(order); }

        /// <summary>
        /// Remove order from Customers Orders List
        /// </summary>
        /// <param name="order"></param>
        public void RemoveCustomerOrder(Order order)
        { CustomersOrdersList.RemoveOrder(order); }


    }
}
