using FinalProject.Logic;
using FinalProject.Logic.MainLogic;
using OperationalTrainer.Data_Structures;
using OperationalTrainer.GUI;
using OperationalTrainer.Logic.Warehouse;
using System;
using System.Data;

namespace OperationalTrainer.Logic.MainLogic
{
    public class NewOrderArrivedEventArgs : EventArgs
    {
        public Order Order { get; set; }
    }

    public class MainManager
    {
        public DataManager dataManager { get; set; }
        private Clock clock { get; set; }
        private WarehouseClass Warehouse { get; set; }
        private Bank bank { get; set; }
        private DateTime CurrnetTime { get; set; }
        private DataSummaryClass DataSummary { get; set; }

        private ProcessesSchedule CurrentProcesses;


        public EventHandler<NewOrderArrivedEventArgs> Event_NewCustomerOrderArrived;
        public EventHandler Event_CustomerOrdersListUpdate;
        public EventHandler Event_SupplierOrdersListUpdate;
        public EventHandler Event_EndOfTimeTickSchedule;

            public NewOrderArrivedEventArgs NewOrderArrivedEventArgs { get; set; }

        enum ProcessesSchedule
        {
            BeginningOfTheTimeTick,
            NewCustomerOrdersArrived,
            SupplierOrdersDelivered,
            EndOfProcess
        }

        public MainManager()
        {
            LoadData ld = new LoadData();
            InitOperationalTrainerDataSet initOperationalTrainerDataSet = ld.LoadInitData();

            CurrnetTime = initOperationalTrainerDataSet.startDate;

            dataManager = new DataManager(initOperationalTrainerDataSet.OperationalTrainerDataSet);
            dataManager.UpdateTime(CurrnetTime);

            clock = new Clock(CurrnetTime);
            clock.Tick += ClockTick;

            Warehouse = new WarehouseClass(dataManager.DataSet.ProductsMetaDataList, initOperationalTrainerDataSet.WarehouseMaxCapacity);
            bank = new Bank(initOperationalTrainerDataSet.BankCurrentBalance);
            DataSummary = new DataSummaryClass(Warehouse, dataManager, bank, CurrnetTime);
        }

        /// <summary>
        /// Return Current Time
        /// </summary>
        /// <returns></returns>
        public DateTime GetCurrentTime()
        {
            return CurrnetTime;
        }

        /// <summary>
        /// Start the Ticker
        /// </summary>
        public void StartClock()
        {
            clock.nextHour();
        }

               /// <summary>
        /// Listener  to the Clock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClockTick(object sender, ClockTimeEventArgs e)
        {
            CurrnetTime = e.Time;
            CurrentProcesses = ProcessesSchedule.BeginningOfTheTimeTick;
            //mainLogic();
            testLogic();
        }




        public void testLogic()
        {

            ProcessesScheduleParser();

        }


        public DataTable GetCustomerOrdersDataTable()
        { return DataSummary.GenerateCustomerOrdersDataTable(); }

        public DataTable GetSupplierOrdersDataTable()
        { return DataSummary.GenerateSupplierOrdersDataTable(); }

        public DataTable GetBankDataTable()
        { return DataSummary.GenerateBank(); }

        public DataTable GetWarehouseDataTable()
        { return DataSummary.GenerateWarehouse(); }

        private void mainLogic()
        {
            ProcessesScheduleParser();

        }

        private void ProcessesScheduleParser()
        {
            CurrentProcesses = (ProcessesSchedule)((int)CurrentProcesses + 1);

            switch (CurrentProcesses)
            {
                case ProcessesSchedule.BeginningOfTheTimeTick:
                    ProcessesScheduleParser();
                    break;
                case ProcessesSchedule.NewCustomerOrdersArrived:
                    NewCustomerOrder();
                    break;
                case ProcessesSchedule.SupplierOrdersDelivered:
                    ProcessesScheduleParser();
                    break;
                case ProcessesSchedule.EndOfProcess:
                    TimeTickScheduleEnd();
                    //  clock.nextHour();
                    break;
                default:
                    break;
            }

        }//end ProcessesScheduleParser

        private void TimeTickScheduleEnd()
        { Event_EndOfTimeTickSchedule(this, null); }


        /// <summary>
        /// Checks if new customer orders arrived
        /// </summary>
        private void NewCustomerOrder()
        {
            OrdersList newOrders = dataManager.getNewCustomerOrdersList(CurrnetTime);

            if (newOrders.OrderList.Count > 0)
            {
                foreach (Order order in newOrders.OrderList)
                {
                    var args = new NewOrderArrivedEventArgs();
                    args.Order = order;
                    Event_NewCustomerOrderArrived(this, args);
                }
            }
            ProcessesScheduleParser();
        }

        public void NewSupplierOrderApproved(Order order)
        {
            dataManager.DataSet.SupplieOrderList.AddOrder(order);
            Event_SupplierOrdersListUpdate(this, null);
        }


        /// <summary>
        ///Add new customer order to customer orders list
        /// </summary>
        /// <param name="newOrder"></param>
        public void NewCustomerOrderApproved(Order newOrder)
        {
            dataManager.DataSet.CustomersOrderList.AddOrder(newOrder);
            Event_CustomerOrdersListUpdate(this, null);
        }

        /// <summary>
        /// Return Products MetaData list
        /// </summary>
        /// <returns></returns>
        public ProductClassList GetProductsMetaData()
        { return dataManager.DataSet.ProductsMetaDataList; }


        /// <summary>
        /// Return suppliersList
        /// </summary>
        /// <returns></returns>
        public SuppliersList GetSuppliersList()
        { return dataManager.DataSet.SuppliersList; }


        /// <summary>
        /// Get new order user Decline
        /// </summary>
        /// <param name="newOrder"></param>
        public void NewCustomerOrderDecline(Order newOrder)
        { }

        public void EventEnded()
        { }


    }//end class MainManager
}
