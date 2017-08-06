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
    public class OrderEventArgs : EventArgs
    {
        public Order Order { get; set; }
    }

    public class MainManager
    {
        public DataManager dataManager { get; set; }
        private ProductionManager productionManager { get; set; }
        private Clock clock { get; set; }
        private WarehouseManager WarehouseManager { get; set; }
        private FinanceManager financeManager { get; set; }
        private DateTime CurrnetTime { get; set; }
        private DataSummaryClass DataSummary { get; set; }
        private ProcessesSchedule CurrentProcesses { get; set; }
        private PurchaseManager purchaseManager { get; set; }
        private MarketingManager marketingManager { get; set; }


        public EventHandler<OrderEventArgs> Event_NewCustomerOrderArrived;
        public EventHandler<OrderEventArgs> Event_NewSupplierOrderDelivered;

        public EventHandler Event_CustomerOrdersListUpdate;
        public EventHandler Event_SupplierOrdersListUpdate;
        public EventHandler Event_EndOfTimeTickSchedule;
        public EventHandler Event_DataLoaded;
        public EventHandler Event_cantDeliverOrder;

        public OrderEventArgs NewOrderArrivedEventArgs { get; set; }

        enum ProcessesSchedule
        {
            BeginningOfTheTimeTick,
            SupplierOrdersDelivered,
            NewCustomerOrdersArrived,
            Production,
            EndOfProcess
        }
        public MainManager()
        { }

        private void init(string filePath)
        {
            LoadData ld = new LoadData();
            InitDataLoad initDataSet = ld.LoadInitData(filePath);

            CurrnetTime = initDataSet.InitParameters.startDate;

            dataManager = new DataManager(initDataSet.MetaData);
            dataManager.UpdateTime(CurrnetTime);

            clock = new Clock(CurrnetTime);
            clock.Tick += ClockTick;

            WarehouseManager = new WarehouseManager(initDataSet.InitParameters.InitWarehouseInventory, initDataSet.InitParameters.WarehouseMaxCapacity);
            financeManager = new FinanceManager(initDataSet.InitParameters.InitBankStartBalance);
            DataSummary = new DataSummaryClass(WarehouseManager, dataManager, financeManager, CurrnetTime, marketingManager, purchaseManager);
            productionManager = new ProductionManager(initDataSet.InitLists.InitToolsList, initDataSet.MetaData.ToolTypeMetaData);
            purchaseManager = new PurchaseManager(initDataSet.InitLists.InitPurchaseOrders);
            marketingManager = new MarketingManager(initDataSet.InitLists.InitCustomersOrderList, initDataSet.InitLists.InitFutureCustomersOrderList);


            foreach (ProductionOrder productionOrder in initDataSet.InitLists.InitProductionOrderList.GetProductionOrderList())
                productionManager.AddProductionOrder(productionOrder);

            productionManager.tempStartProduction();
        }

        public DataTable GetProductionsDataTable()
        {
            try
            {
                return DataSummary.toolListToDT(productionManager.GetToolsList());
            }
            catch
            {
                return null;
            }
        }

        private void CustomerOrderDelivery(string orderID)
        {
            Order order = marketingManager.GetCustomerOrder(orderID);
            bool canGetOrder = WarehouseManager.CanGetProducts(order);

            if (!canGetOrder)
                cantDeliverOrder();
            else
            {
                WarehouseManager.GetProducts(order);
                marketingManager.RemoveCustomerOrder(order);
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
                    NewCustomerOrders();
                    break;
                case ProcessesSchedule.SupplierOrdersDelivered:
                    SupplierOrderDeliver();
                    //ProcessesScheduleParser();
                    break;
                case ProcessesSchedule.Production:
                    productionTick();
                    break;
                case ProcessesSchedule.EndOfProcess:
                    TimeTickScheduleEnd();
                    //  clock.nextHour();
                    break;
                default:
                    break;
            }

        }//end ProcessesScheduleParser

        private void productionTick()
        {
            productionManager.tempNextTick(CurrnetTime);

            ProcessesScheduleParser();
        }

        private void SupplierOrderDeliver()
        {
            OrdersList newOrders = purchaseManager.GetPurchaseOrders(CurrnetTime);
            if (newOrders.OrderList.Count > 0)
            {
                foreach (Order order in newOrders.OrderList)
                {
                    var args = new OrderEventArgs();
                    args.Order = order;
                    Event_NewSupplierOrderDelivered(this, args);
                }
            }
            ProcessesScheduleParser();
        }


        /// <summary>
        /// Checks if new customer orders arrived
        /// </summary>
        private void NewCustomerOrders()
        {
            OrdersList newOrders = marketingManager.GetFutureCustomersOrder(CurrnetTime);

            if (newOrders.OrderList.Count > 0)
            {
                foreach (Order order in newOrders.OrderList)
                {
                    var args = new OrderEventArgs();
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
                return DataSummary.GenerateCustomerOrdersDataTable(marketingManager.GetCustomersOrdersList());
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
                return DataSummary.GenerateSupplierOrdersDataTable(purchaseManager.GetPurchaseOrders());
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
                return DataSummary.GenerateBank(purchaseManager.GetPurchaseOrders(), marketingManager.GetCustomersOrdersList(), financeManager.CurrentBalance);
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
                return DataSummary.GenerateWarehouse(purchaseManager.GetPurchaseOrders(), marketingManager.GetCustomersOrdersList());
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
        { return dataManager.getProductsMetaData(); }

        /// <summary>
        /// Return SuppliersMetaData
        /// </summary>
        /// <returns></returns>
        public SuppliersList GetSuppliersMetaData()
        { return dataManager.getSuppliersMetaData(); }



        //set partiotion

        public void NewCustomerOrderDecline(Order newOrder)
        { }
        public void NewSupplierOrderApproved(Order order)
        {
            purchaseManager.AddSupplierOrder(order);
            Event_SupplierOrdersListUpdate(this, null);
        }

        /// <summary>
        ///Add new customer order to customer orders list
        /// </summary>
        /// <param name="newOrder"></param>
        public void NewCustomerOrderApproved(Order newOrder)
        {
            marketingManager.AddCustomerOrder(newOrder);
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

        public void SupplierOrderDeliveredAproved(Order order)
        {
            WarehouseManager.AddProducts(order);
            financeManager.UpdateBalance(order);
            Event_SupplierOrdersListUpdate(this, null);
        }

        public void EventEnded()
        { }


    }//end class MainManager
}
