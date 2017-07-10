using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OperationalTrainer.Data_Structures.Order;

namespace OperationalTrainer.Data_Structures
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
        enum OrderDTStructure
        {
            Name,
            ID,
            OrderID,
            OrderDate,
            OrderDeliveryDate,
            OrderStatus
        }

        public PersonClass Person { get; set; }
        public OrderTypeEnum OrderType { get; set; }
        public string OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderDeliveryDate { get; set; }
        public List<PriceTable> OrderProductsList { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }

        public Order() { }
        public Order(OrderTypeEnum orderType)
        { OrderType = orderType; }

        public Order(string orderID)
        { OrderID = orderID; }
        public Order(PersonClass person, OrderTypeEnum orderType, string orderID, DateTime orderDate, DateTime orderDeliveryDate, List<PriceTable> productsList)
        {
            Person = person;
            OrderType = orderType;
            OrderID = orderID;
            OrderDate = orderDate;
            OrderDeliveryDate = orderDeliveryDate;
            OrderProductsList = productsList;
        }

        public override bool Equals(object obj)
        {
            Order order = (Order)obj;
            return OrderID.Equals(order.OrderID);
        }

        public override int GetHashCode()
        {
            return OrderID.GetHashCode();

        }

        /// <summary>
        /// Convert Order type to DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable ToDataTable()
        {
            DataTable dt = new DataTable();
            DataTable priceTableDT = PriceTable.ToDataTable(OrderProductsList);

            foreach (OrderDTStructure header in Enum.GetValues(typeof(OrderDTStructure)))
                dt.Columns.Add(header.ToString());

            foreach (DataColumn column in priceTableDT.Columns)
                dt.Columns.Add(column.ColumnName);

            foreach (DataRow drow in priceTableDT.Rows)
            {
                DataRow newDrow = dt.NewRow();
                newDrow[OrderDTStructure.Name.ToString()] = Person.Name;
                newDrow[OrderDTStructure.ID.ToString()] = Person.ID;
                newDrow[OrderDTStructure.OrderID.ToString()] = OrderID;
                newDrow[OrderDTStructure.OrderDate.ToString()] = OrderDate.ToShortDateString();
                newDrow[OrderDTStructure.OrderDeliveryDate.ToString()] = OrderDeliveryDate.ToShortDateString();
                newDrow[OrderDTStructure.OrderStatus.ToString()] = OrderStatus.ToString();

                foreach (DataColumn header in priceTableDT.Columns)
                    newDrow[header.ColumnName] = drow[header.ColumnName];

                dt.Rows.Add(newDrow);
            }
            return dt;
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

            foreach (PriceTable priceTable in OrderProductsList)
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


    }//end OrderClass


    public class OrdersList
    {
        public List<Order> OrderList;

        public OrdersList()
        {
            OrderList = new List<Order>();
        }


        /// <summary>
        /// Add Order type to OrderList
        /// </summary>
        /// <param name="order"></param>
        public void AddOrder(Order order)
        {
            OrderList.Add(order);
        }

        /// <summary>
        /// Rerutn Order from OrderList
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public Order GetOrder(Order order)
        {
            return OrderList[OrderList.IndexOf(order)];
        }

        /// <summary>
        /// Return Order from OrderList
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public Order GetOrder(string orderID)
        {
            Order order = new Order(orderID);
            return OrderList[OrderList.IndexOf(order)];
        }

        /// <summary>
        /// Retutn OrderList with given Order Status
        /// </summary>
        /// <param name="orderStatus"></param>
        /// <returns></returns>
        public List<Order> GetOrders(OrderStatusEnum orderStatus)
        {
            List<Order> result = new List<Order>();
            foreach (Order order in OrderList)
                if (order.OrderStatus.Equals(orderStatus))
                    result.Add(order);
            return result;
        }

        /// <summary>
        /// Retutn OrderList with given OrderDate
        /// </summary>
        /// <param name="orderDate"></param>
        /// <returns></returns>
        public OrdersList GetOrders(DateTime orderDate)
        {

            OrdersList result = new OrdersList();
            foreach (Order order in OrderList)
                if (order.OrderDate.Date == orderDate.Date)
                    result.AddOrder(order);

            return result;
        }

        /// <summary>
        /// Remove Order From order List
        /// </summary>
        /// <param name="order"></param>
        public void RemoveOrder(Order order)
        {
            OrderList.Remove(order);
        }

        /// <summary>
        /// Remove Orders From Order List
        /// </summary>
        /// <param name="orderList"></param>
        public void RemoveOrders(OrdersList orderList)
        {
            if (orderList.OrderList.Count > 0)
                foreach (Order order in orderList.OrderList)
                    RemoveOrder(order);
        }

        /// <summary>
        /// Convert OrderList type to DataTable
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable ToDataTable()
        {
            try
            {
                DataTable ordersDT = OrderList[0].ToDataTable();
                for (int i = 1; i < OrderList.Count; i++)
                    ordersDT.Merge(OrderList[i].ToDataTable());
                return ordersDT;
            }
            catch (Exception)
            {
                return null;
            }
        }



    }//end OrdersList

}// end nameSpace
