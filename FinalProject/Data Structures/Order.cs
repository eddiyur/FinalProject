using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FinalProject.Data_Structures.Order;

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

        public PersonClass Person { get; set; }
        public OrderTypeEnum OrderType { get; set; }
        public string OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderDeliveryDate { get; set; }
        public List<PriceTable> OrderProductsList { get; set; }

        public OrderStatusEnum OrderStatus;

        public Order()
        { }
        public Order(OrderTypeEnum orderType)
        { OrderType = orderType; }

        public Order(string orderID)
        { OrderID = orderID; }

        public override bool Equals(object obj)
        {
            Order order = (Order)obj;
            return OrderID.Equals(order.OrderID);
        }


        public override int GetHashCode()
        {
            return OrderID.GetHashCode();

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
        public Order(PersonClass person, OrderTypeEnum orderType, string orderID, DateTime orderDate, DateTime orderDeliveryDate, List<PriceTable> productsList)
        {
            Person = person;
            OrderType = orderType;
            OrderID = orderID;
            OrderDate = orderDate;
            OrderDeliveryDate = orderDeliveryDate;
            OrderProductsList = productsList;
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


    }//end orderClass


    public class OrdersList
    {
        public List<Order> OrderList;

        public OrdersList()
        {
            OrderList = new List<Order>();
        }

        public void AddOrder(Order order)
        {
            OrderList.Add(order);
        }

        public Order GetOrder(Order order)
        {
            return OrderList[OrderList.IndexOf(order)];
        }

        public Order GetOrder(string orderID)
        {
            Order order = new Order(orderID);
            return OrderList[OrderList.IndexOf(order)];
        }

        public List<Order> GetOrders(OrderStatusEnum orderStatus)
        {
            List<Order> result = new List<Order>();
            foreach (Order order in OrderList)
                if (order.OrderStatus.Equals(orderStatus))
                    result.Add(order);
            return result;
        }

        public void RemoveOrder(Order order)
        {
            OrderList.Remove(order);
        }

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
