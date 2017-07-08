﻿using FinalProject.Data_Structures;
using FinalProject.FileManagerFolder;
using FinalProject.GUI;
using FinalProject.Logic.MainLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using UtilitiesFileManager;

namespace FinalProject
{
    public partial class Form1 : Form
    {
        public struct DataSet
        {
            public OrdersList customerOrderList;
        }

        public DataSet DB;
        public Form1()
        {
            InitializeComponent();
            DB = new DataSet();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadCustomerOrderForm_Test();
            LoadAllOrders();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData loadData = new LoadData();
            loadData.LoadLists();

            DB.customerOrderList = loadData.customerOrderList;

        }

        private void LoadAllOrders()
        {
            DataTable dt = DB.customerOrderList.ToDataTable();
            OrdersTable ordersTable = new OrdersTable(dt);
            var result = ordersTable.ShowDialog();
        }

        private void loadCustomerOrderForm_Test()
        {
            List<Order> orderList = DB.customerOrderList.OrderList;
            CustomerOrderForm customerOrderForm = new CustomerOrderForm(orderList[0]);

            var result = customerOrderForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                var a = customerOrderForm.ReturnValue1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            DataTable dt = DB.customerOrderList.ToDataTable();

            GeneralDataGrid gd = new GeneralDataGrid(dt);
            gd.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProductParser.ProductClassCSVToXML();
        }

        public Clock clock;
        public MainManager mn;

        private void button4_Click(object sender, EventArgs e)
        {
            clock = new Clock(DateTime.Now);
            mn = new MainManager();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            mn.test(clock);
            clock.nextHour();
        }
    }
}
