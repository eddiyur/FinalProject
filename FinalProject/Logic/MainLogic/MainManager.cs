using FinalProject.Logic;
using FinalProject.Logic.MainLogic;
using OperationalTrainer.Data_Structures;
using OperationalTrainer.GUI;
using OperationalTrainer.Logic.Warehouse;
using System;
using System.Data;
using UtilitiesFileManager;

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
        private ProcessesSchedule CurrentProcesses { get; set; }


        public EventHandler<NewOrderArrivedEventArgs> Event_NewCustomerOrderArrived;
        public EventHandler Event_CustomerOrdersListUpdate;
        public EventHandler Event_SupplierOrdersListUpdate;
        public EventHandler Event_EndOfTimeTickSchedule;
        public EventHandler Event_DataLoaded;
        public EventHandler Event_cantDeliverOrder;


        public NewOrderArrivedEventArgs NewOrderArrivedEventArgs { get; set; }

        enum ProcessesSchedule
        {
            BeginningOfTheTimeTick,
            NewCustomerOrdersArrived,
            SupplierOrdersDelivered,
            EndOfProcess
        }
        public MainManager()
        { }

        private void init(string filePath)
        {
            LoadData ld = new LoadData();
            InitOperationalTrainerDataSet initOperationalTrainerDataSet = ld.LoadInitData(filePath);

            CurrnetTime = initOperationalTrainerDataSet.OperationalTrainerInitDataSet.startDate;

            dataManager = new DataManager(initOperationalTrainerDataSet.OperationalTrainerDataSet);
            dataManager.UpdateTime(CurrnetTime);

            clock = new Clock(CurrnetTime);
            clock.Tick += ClockTick;

            Warehouse = new WarehouseClass(initOperationalTrainerDataSet.OperationalTrainerInitDataSet.WarehouseInitInventory, initOperationalTrainerDataSet.OperationalTrainerInitDataSet.WarehouseMaxCapacity);
            bank = new Bank(initOperationalTrainerDataSet.OperationalTrainerInitDataSet.BankCurrentBalance);
            DataSummary = new DataSummaryClass(Warehouse, dataManager, bank, CurrnetTime);
        }


        private void CustomerOrderDelivery(string orderID)
        {
            Order order = dataManager.DataSet.CustomersOrderList.GetOrder(orderID);
            bool canGetOrder = Warehouse.CanGetOrder(order);

            if (!canGetOrder)
                cantDeliverOrder();
            else
            {
                Warehouse.GetOrder(order);
                dataManager.DataSet.CustomersOrderList.RemoveOrder(order);
                Event_CustomerOrdersListUpdate(this, null);
            }


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

        public void CreateXMLScenario(CSVScenarioFilePath cSVScenarioFilePath)
        {
            LoadData loadData = new LoadData();
            loadData.CreateXMLScenario(cSVScenarioFilePath);

        }

        public void LoadScenario()
        {
            FileManager fileManager = new FileManager();
            string filePath = fileManager.openFilePathXML();

            if (!string.IsNullOrEmpty(filePath))
            {
                init(filePath);
                DataLoaded();
            }
        }



        //logic



        public void testLogic()
        {
            ProcessesScheduleParser();
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



        //events
        private void TimeTickScheduleEnd()
        { Event_EndOfTimeTickSchedule(this, null); }

        private void DataLoaded()
        { Event_DataLoaded(this, null); }

        private void cantDeliverOrder()
        { Event_cantDeliverOrder(this, null); }


        //get partiotion

        /// <summary>
        /// Return Current Time
        /// </summary>
        /// <returns></returns>
        public DateTime GetCurrentTime()
        { try { return CurrnetTime; } catch { return new DateTime(); } }
        public DataTable GetCustomerOrdersDataTable()
        {
            try
            {
                return DataSummary.GenerateCustomerOrdersDataTable();
            }
            catch (Exception)
            {
                return null;
            }

        }
        public DataTable GetSupplierOrdersDataTable()
        {
            try
            {
                return DataSummary.GenerateSupplierOrdersDataTable();
            }
            catch
            {
                return null;
            }
        }
        public DataTable GetBankDataTable()
        {
            try
            {
                return DataSummary.GenerateBank();
            }
            catch
            {
                return null;
            }
        }
        public DataTable GetWarehouseDataTable()
        {
            try
            {
                return DataSummary.GenerateWarehouse();
            }
            catch
            {
                return null;
            }
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



        //set partiotion

        public void NewCustomerOrderDecline(Order newOrder)
        { }
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
        /// Start the Ticker
        /// </summary>
        public void StartClock()
        {
            clock.nextHour();
        }

        public void CustomerOrderDeliveryApproved(string orderID)
        { CustomerOrderDelivery(orderID); }



        public void EventEnded()
        { }


    }//end class MainManager
}
