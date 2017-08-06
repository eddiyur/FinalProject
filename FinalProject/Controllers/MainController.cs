using FinalProject.GUI;
using OperationalTrainer.Data_Structures;
using OperationalTrainer.GUI;
using OperationalTrainer.Logic.MainLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject.Controllers
{
    public static class MainParameters
    {
        public static bool GameStarted { get; set; }
    }

    public static class MainController
    {

        private static MainManager mainManager;
        private static MainForm mainForm;
        public static void Initialize(MainForm mForm)
        {
            MainParameters.GameStarted = false;

            mainManager = new MainManager();
            mainForm = mForm;
            mainManager.Event_NewCustomerOrderArrived += NewCustomerOrderArriver;
            mainManager.Event_EndOfTimeTickSchedule += EndOfTimeTickSchedule;
            mainManager.Event_CustomerOrdersListUpdate += CustomerOrdersListUpdate;
            mainManager.Event_SupplierOrdersListUpdate += Event_SupplierOrdersListUpdate;
            mainManager.Event_DataLoaded += Event_DataLoaded;
            mainManager.Event_cantDeliverOrder += cantDeliverOrder;
            mainManager.Event_NewSupplierOrderDelivered += NewSupplierOrderDelivered;
        }


        private static void cantDeliverOrder(object sender, EventArgs e)
        { MessageBox.Show("can't deliver this order", "Error"); }

        private static void Event_DataLoaded(object sender, EventArgs e)
        {
            MainParameters.GameStarted = true;
            mainForm.UpdateGUI();
        }

        private static void Event_SupplierOrdersListUpdate(object sender, EventArgs e)
        { mainForm.UpdateGUI(); }

        private static void CustomerOrdersListUpdate(object sender, EventArgs e)
        { mainForm.UpdateGUI(); }

        private static void EndOfTimeTickSchedule(object sender, EventArgs e)
        { mainForm.UpdateGUI(); }

        public static void StartClock()
        { mainManager.StartClock(); }

        public static void NewCustomerOrderArriver(object sender, OrderEventArgs args)
        {
            OrderViewForm of = new OrderViewForm(args.Order, OrderViewForm.OrderFormType.newOrder);
            of.ShowDialog();
        }

        private static void NewSupplierOrderDelivered(object sender, OrderEventArgs args)
        {
            OrderViewForm of = new OrderViewForm(args.Order, OrderViewForm.OrderFormType.SupplierOrderDelivered);
            of.ShowDialog();
        }

        public static void CustomerOrderDeliveryApproved(string orderID)
        { mainManager.CustomerOrderDeliveryApproved(orderID); }

        public static DataTable GetCustomerOrdersDataTable()
        { return mainManager.GetCustomerOrdersDataTable(); }

        public static DataTable GetSupplierOrdersDataTable()
        { return mainManager.GetSupplierOrdersDataTable(); }

        public static DataTable GetProductionsDataTable()
        { return mainManager.GetProductionsDataTable(); }

        public static DataTable GetBankDataTable()
        { return mainManager.GetBankDataTable(); }

        public static DataTable GetWarehouseDataTable()
        { return mainManager.GetWarehouseDataTable(); }

        internal static void CreateXMLScenario(CSVScenarioFilePath cSVScenarioFilePath)
        { mainManager.CreateXMLScenario(cSVScenarioFilePath); }

        public static DateTime GetCurrentTime()
        { return mainManager.GetCurrentTime(); }

        public static ProductClassList GetProductsMetaData()
        { return mainManager.GetProductsMetaData(); }

        public static SuppliersList GetSuppliersList()
        { return mainManager.GetSuppliersMetaData(); }

        public static void NewCustomerOrderDecline(Order order)
        { mainManager.NewCustomerOrderDecline(order); }

        public static void NewCustomerOrderApproved(Order order)
        { mainManager.NewCustomerOrderApproved(order); }

        public static void NewSupplierOrderApproved(Order order)
        { mainManager.NewSupplierOrderApproved(order); }

        public static void LoadScenario()
        { mainManager.LoadScenario(); }

        public static void SupplierOrderDeliveredAproved(Order order)
        { mainManager.SupplierOrderDeliveredAproved(order); }
    }//end  MainController
}
