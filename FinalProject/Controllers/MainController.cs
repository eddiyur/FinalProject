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

namespace FinalProject.Controllers
{
    public static class MainController
    {
        private static MainManager mainManager;
        private static MainForm mainForm;
        public static void Initialize(MainForm mForm)
        {
            mainManager = new MainManager();
            mainForm = mForm;
            mainManager.NewCustomerOrderArrived += NewCustomerOrderArriver;
            mainManager.EndOfTimeTick += EndOfTimeTick;
            mainManager.CustomerOrderAdded += CustomerOrderAdded;
        }

        private static void CustomerOrderAdded(object sender, EventArgs e)
        {
             mainForm.UpdateGUI();
        }

        private static void EndOfTimeTick(object sender, EventArgs e)
        {
            mainForm.UpdateGUI();
        }

        public static void StartClock()
        { mainManager.StartClock(); }

        public static void NewCustomerOrderArriver(object sender, NewOrderArrivedEventArgs args)
        {
            OrderForm of = new OrderForm(args.Order, OrderForm.OrderFormType.newOrder);
            of.ShowDialog();
        }

        public static void NewOrderArrived(Order order)
        { mainManager.NewCustomerOrderApproved(order); }

        public static void NewCustomerOrderEventEnd()
        { mainManager.EventEnded(); }

        public static void test()
        { mainManager.testLogic(); }

        public static DataTable GetCustomerOrdersDataTable()
        { return mainManager.GetCustomerOrdersDataTable(); }

        public static DataTable GetSupplierOrdersDataTable()
        { return mainManager.GetSupplierOrdersDataTable(); }

        public static DataTable GetBankDataTable()
        { return mainManager.GetBankDataTable(); }

        public static DataTable GetWarehouseDataTable()
        { return mainManager.GetWarehouseDataTable(); }

        public static DateTime GetCurrentTime()
        { return mainManager.GetCurrentTime(); }

        public static void NewOrderDecline(Order order)
        { mainManager.NewCustomerOrderDecline(order); }

        public static void NewOrderApproved(Order order)
        { mainManager.NewCustomerOrderApproved(order); }
    }//end  MainController
}
