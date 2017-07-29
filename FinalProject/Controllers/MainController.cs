﻿using FinalProject.GUI;
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
            mainManager.Event_NewCustomerOrderArrived += NewCustomerOrderArriver;
            mainManager.Event_EndOfTimeTickSchedule += EndOfTimeTickSchedule;
            mainManager.Event_CustomerOrdersListUpdate += CustomerOrdersListUpdate;
        }

        private static void CustomerOrdersListUpdate(object sender, EventArgs e)
        {
            mainForm.UpdateGUI();
        }

        private static void EndOfTimeTickSchedule(object sender, EventArgs e)
        { mainForm.UpdateGUI(); }

        public static void StartClock()
        { mainManager.StartClock(); }

        public static void NewCustomerOrderArriver(object sender, NewOrderArrivedEventArgs args)
        {
            OrderViewForm of = new OrderViewForm(args.Order, OrderViewForm.OrderFormType.newOrder);
            of.ShowDialog();
        }

        //public static void NewOrderArrived(Order order)
        //{ mainManager.NewCustomerOrderApproved(order); }

        //public static void NewCustomerOrderEventEnd()
        //{ mainManager.EventEnded(); }

        //public static void test()
        //{ mainManager.testLogic(); }

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

        public static ProductClassList GetProductsMetaData()
        { return mainManager.GetProductsMetaData(); }

        public static SuppliersList GetSuppliersList ()
        { return mainManager.GetSuppliersList(); }

        public static void NewOrderDecline(Order order)
        { mainManager.NewCustomerOrderDecline(order); }

        public static void NewOrderApproved(Order order)
        { mainManager.NewCustomerOrderApproved(order); }


    }//end  MainController
}
