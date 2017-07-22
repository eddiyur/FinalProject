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
        public static void Initialize()
        {
            mainManager = new MainManager();
            mainManager.NewOrderArrived += NewOrderArriver;
        }

        public static void StartClock()
        {
            mainManager.StartClock();
        }

        public static void NewOrderArriver(object sender, NewOrderArrivedEventArgs args)
        {
            OrderForm of = new OrderForm(args.Order);
            of.ShowDialog();
        }

        public static void NewOrderArrived(Order order)
        {
            mainManager.NewCustomerOrderApproved(order);
        }

        public static void NewCustomerOrderEventEnd()
        {
            mainManager.EventEnded();
        }

        public static void test()
        {
            mainManager.testLogic();
        }

        public static DataTable GetCustomerOrdersDataTable()
        {
            return mainManager.GetCustomerOrdersDataTable();
        }

        public static DataTable GetSupplierOrdersDataTable()
        {
            return mainManager.GetSupplierOrdersDataTable();
        }

        public static DataTable GetBankDataTable()
        {
            return mainManager.GetBankDataTable();
        }

    }//end  MainController
}
