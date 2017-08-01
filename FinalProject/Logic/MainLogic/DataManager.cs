using OperationalTrainer.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationalTrainer.Logic.MainLogic
{
    public class DataStructureClass
    {

        public ProductClassList ProductsMetaDataList { get; set; }
        public SuppliersList SuppliersList { get; set; }

        public OrdersList CustomersOrderList { get; set; }

        public OrdersList SupplieOrderList { get; set; }
        public int LastSupplierOrderIndex { get; set; }

        public OrdersList FutureCustomersOrderList { get; set; }

        public ToolTypeClassList ToolTypelist { get; set; }

       

    }


    public class InitDataStructureClass
    {
        public DateTime startDate { get; set; }
        public double WarehouseMaxCapacity { get; set; }
        public Double BankCurrentBalance { get; set; }
        public ToolsList ToolList { get; set; }
        public Dictionary<ProductClass, double> WarehouseInitInventory { get; set; }

    }

    public class InitDataLoad
    {
        public InitDataStructureClass InitDataStructure { get; set; }
        public DataStructureClass DataStructure { get; set; }

        public InitDataLoad()
        { DataStructure = new DataStructureClass(); }
    }

    public class DataManager
    {
        //   private Clock _clock;
        public DataStructureClass DataSet { get; set; }
        public DateTime CurrnetTime { get; set; }

        public DataManager(DataStructureClass dataSet)
        { DataSet = dataSet; }

        public void UpdateTime(DateTime currnetTime)
        { CurrnetTime = currnetTime; }

        /// <summary>
        /// Removes from future_Customers_Order_List Orders with given date and return them
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public OrdersList getNewCustomerOrdersList(DateTime date)
        {
            OrdersList newCustomerOrdersList = DataSet.FutureCustomersOrderList.GetOrdersByOrderDate(date);
            DataSet.FutureCustomersOrderList.RemoveOrders(newCustomerOrdersList);
            return newCustomerOrdersList;
        }

        public OrdersList getSupplierOrderDelivered(DateTime date)
        {
            OrdersList newOrdersList = DataSet.SupplieOrderList.GetOrdersByOrderDeliveryDate(date);
            DataSet.SupplieOrderList.RemoveOrders(newOrdersList);
            return newOrdersList;
        }

        /// <summary>
        /// Removes from future_Customers_Order_List Orders with CurrentDate and return them
        /// </summary>
        /// <returns></returns>
        public OrdersList getNewCustomerOrdersList(Clock clock)
        {
            return getNewCustomerOrdersList(clock.ClockTime.Time);
        }
    }
}
