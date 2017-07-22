using FinalProject.Controllers;
using FinalProject.FileManagerFolder;
using FinalProject.GUI;
using OperationalTrainer.Data_Structures;
using OperationalTrainer.FileManagerFolder;
using OperationalTrainer.Logic.MainLogic;
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


        private void Form1_Load(object sender, EventArgs e)
        {
            //LoadData loadData = new LoadData();
            //loadData.LoadLists();

            //   DB.customerOrderList = loadData.customerOrderList;

        }



        public Clock clock;
        public MainManager mn;

        private void button4_Click(object sender, EventArgs e)
        {
            clock = new Clock(new DateTime(2017, 01, 31));
            mn = new MainManager();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OrdersMainForm CustomerMainForm = new OrdersMainForm(MainController.GetCustomerOrdersDataTable());
            CustomerMainForm.Show();

            OrdersMainForm SupplierMainForm = new OrdersMainForm(MainController.GetSupplierOrdersDataTable());
            SupplierMainForm.Show();


            OrdersMainForm bankMainForm = new OrdersMainForm(MainController.GetBankDataTable());
            bankMainForm.Show();

            OrdersMainForm WarehouseMainForm = new OrdersMainForm(MainController.GetWarehouseDataTable());
            WarehouseMainForm.Show();
           
        }


    }//end class
}
