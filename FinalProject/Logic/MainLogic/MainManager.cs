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
        public EventHandler<NewOrderArrivedEventArgs> NewOrderArrived;
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
            DataSummary = new DataSummaryClass(Warehouse, dataManager,bank, CurrnetTime);
        }



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
            CurrnetTime = new DateTime(2017, 02, 01);
            OrdersList newOrders = dataManager.getNewCustomerOrdersList(CurrnetTime);

            dataManager.UpdateTime(CurrnetTime);

            DataSummary.GenerateCustomerOrdersDataTable(newOrders);

            // ProcessesScheduleParser();

        }


        public DataTable GetCustomerOrdersDataTable()
        { return DataSummary.GenerateCustomerOrdersDataTable(); }

        public DataTable GetSupplierOrdersDataTable()
        { return DataSummary.GenerateSupplierOrdersDataTable(); }

        public DataTable GetBankDataTable()
        { return DataSummary.GenerateBank(); }


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
                    break;
                case ProcessesSchedule.NewCustomerOrdersArrived:
                    NewCustomerOrder();
                    break;
                case ProcessesSchedule.SupplierOrdersDelivered:
                    break;
                case ProcessesSchedule.EndOfProcess:
                    clock.nextHour();
                    break;
                default:
                    break;
            }
            ProcessesScheduleParser();

        }

        private void NewCustomerOrder()
        {
            OrdersList newOrders = dataManager.getNewCustomerOrdersList(CurrnetTime);

            if (newOrders.OrderList.Count > 0)
            {
                var args = new NewOrderArrivedEventArgs();
                args.Order = newOrders.OrderList[0];
                NewOrderArrived(this, args);
            }
        }


        /// <summary>
        ///Get new order to add to customer order list
        /// </summary>
        /// <param name="newOrder"></param>
        public void NewCustomerOrderApproved(Order newOrder)
        {
            dataManager.DataSet.CustomersOrderList.AddOrder(newOrder);
        }

        /// <summary>
        /// Get new order user dinied
        /// </summary>
        /// <param name="newOrder"></param>
        public void NewCustomerOrderDenied(Order newOrder)
        { }

        public void EventEnded()
        {
            ProcessesScheduleParser();
        }


    }//end class MainManager
}
