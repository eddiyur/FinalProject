using OperationalTrainer.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationalTrainer.Logic.MainLogic
{
    public class OperationalTrainerDataSetStructure
    {

        //    public DateTime CurrentDateTime { get; set; }

        public ProductClassList ProductsMetaDataList { get; set; }
        public SuppliersList SuppliersList { get; set; }
        public OrdersList CustomersOrderList { get; set; }
        public OrdersList SupplieOrderList { get; set; }
        public OrdersList futureCustomersOrderList { get; set; }

    }

    public class InitOperationalTrainerDataSet
    {
        //init data
        public DateTime startDate { get; set; }
        public double WarehouseMaxCapacity { get; set; }
        public Double BankCurrentBalance { get; set; }
        public OperationalTrainerDataSetStructure OperationalTrainerDataSet { get; set; }

        public InitOperationalTrainerDataSet()
        { OperationalTrainerDataSet = new OperationalTrainerDataSetStructure(); }
    }

    public class DataManager
    {
        private Clock _clock;
        public OperationalTrainerDataSetStructure DataSet { get; set; }

        public DataManager(OperationalTrainerDataSetStructure dataSet)
        {
            DataSet = dataSet;
        }

        public void ConnectToClock(Clock clock)
        {
            _clock = clock;
            _clock.Tick += ClockTick;
        }

        public void ClockTick(object sender, ClockTimeEventArgs e)
        {
            //    DataSet.CurrentDateTime = e.Time;
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

        /// <summary>
        /// Removes from future_Customers_Order_List Orders with CurrentDate and return them
        /// </summary>
        /// <returns></returns>
        public OrdersList getNewCustomerOrdersList(Clock clock)
        {
            return getNewCustomerOrdersList(clock.ClockTime.Time);
            // return getNewCustomerOrdersList(DataSet.CurrentDateTime);
        }
    }
}
