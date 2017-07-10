using OperationalTrainer.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationalTrainer.Logic.MainLogic
{
    public class OperationalTrainerDataSet
    {
        public DateTime CurrentDateTime { get; set; }
        public ProductClassList ProductsMetaDataList { get; set; }
        public SuppliersList SuppliersList { get; set; }
        public OrdersList CustomersOrderList { get; set; }
        public OrdersList SupplieOrderList { get; set; }
        public OrdersList futureCustomersOrderList { get; set; }
    }


    public class DataManager
    {

        public OperationalTrainerDataSet DataSet { get; set; }

        public DataManager()
        {
            LoadData ld = new LoadData();
            DataSet = ld.LoadInitData();
        }

        public void UpdateClock(DateTime newCurrentDateTime)
        {
            DataSet.CurrentDateTime = newCurrentDateTime;
        }

        /// <summary>
        /// Removes from future_Customers_Order_List Orders with given date and return them
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public OrdersList getNewCustomerOrdersList(DateTime date)
        {
            OrdersList newCustomerOrdersList = DataSet.futureCustomersOrderList.GetOrders(date);
            DataSet.futureCustomersOrderList.RemoveOrders(newCustomerOrdersList);
            return newCustomerOrdersList;
        }

    }
}
