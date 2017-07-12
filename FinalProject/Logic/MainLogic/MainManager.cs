using FinalProject.Logic;
using OperationalTrainer.Data_Structures;
using OperationalTrainer.GUI;
using OperationalTrainer.Logic.Warehouse;
using System;

namespace OperationalTrainer.Logic.MainLogic
{
    public class MainManager
    {
        public DataManager dataManager { get; set; }
        private Clock clock { get; set; }
        private WarehouseClass Warehouse { get; set; }
        private Bank bank { get; set; }
        private DateTime CurrnetTime { get; set; }
        private ProcessesSchedule CurrentProcesses;

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

            clock = new Clock(CurrnetTime);
            clock.Tick += ClockTick;

            Warehouse = new WarehouseClass(dataManager.DataSet.ProductsMetaDataList, initOperationalTrainerDataSet.WarehouseMaxCapacity);
            bank = new Bank(initOperationalTrainerDataSet.BankCurrentBalance);
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
            // mainLogic();
            testLogic();
        }


        void testLogic()
        {
            CurrnetTime = new DateTime(2017, 02, 01);
            NewCustomerOrder();

        }

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
                OrderForm of = new OrderForm(newOrders.OrderList[0]);
                of.Show();
                //  MessageBox.Show("new order Araive");

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


    }//end class MainManager
}
