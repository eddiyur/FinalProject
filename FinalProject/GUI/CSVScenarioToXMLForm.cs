using FinalProject.Controllers;
using OperationalTrainer.Data_Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UtilitiesFileManager;

namespace FinalProject.GUI
{

    public partial class CSVScenarioToXMLForm : Form
    {

        FileManager fileManager;
        CSVScenarioFilePath cSVScenarioFilePath;
        public CSVScenarioToXMLForm()
        {
            cSVScenarioFilePath = new CSVScenarioFilePath();
            fileManager = new FileManager();

            InitializeComponent();


        }

        void readyFiles()
        {
            cSVScenarioFilePath.InitData = @"C:\Users\eyurkovs\Desktop\final progect\FinalProject\FinalProject\FinalProject\dataSets\testScenario\initData.csv";
            cSVScenarioFilePath.ProductsList = @"C:\Users\eyurkovs\Desktop\final progect\FinalProject\FinalProject\FinalProject\dataSets\testScenario\Product.csv";
            cSVScenarioFilePath.WarehouseInitInventory = @"C:\Users\eyurkovs\Desktop\final progect\FinalProject\FinalProject\FinalProject\dataSets\testScenario\Warehouse.csv";
            cSVScenarioFilePath.CustomersOrderList = @"C:\Users\eyurkovs\Desktop\final progect\FinalProject\FinalProject\FinalProject\dataSets\testScenario\Customer_Order_List.csv";
            cSVScenarioFilePath.FutureCustomersOrderList = @"C:\Users\eyurkovs\Desktop\final progect\FinalProject\FinalProject\FinalProject\dataSets\testScenario\Future_Customer_Order_List.csv";
            cSVScenarioFilePath.SuppliersOrderList = @"C:\Users\eyurkovs\Desktop\final progect\FinalProject\FinalProject\FinalProject\dataSets\testScenario\Supplier_Orders_List.csv";
            cSVScenarioFilePath.SuppliersList = @"C:\Users\eyurkovs\Desktop\final progect\FinalProject\FinalProject\FinalProject\dataSets\testScenario\Suppliers_List.csv";
        }


        private void updateGUi()
        {
            label2.Text = cSVScenarioFilePath.InitData;
            label3.Text = cSVScenarioFilePath.ProductsList;
            label7.Text = cSVScenarioFilePath.WarehouseInitInventory;
            label5.Text = cSVScenarioFilePath.SuppliersList;
            label13.Text = cSVScenarioFilePath.CustomersOrderList;
            label11.Text = cSVScenarioFilePath.FutureCustomersOrderList;
            label9.Text = cSVScenarioFilePath.SuppliersOrderList;
        }

        private void button_InitData_Click(object sender, EventArgs e)
        {
            cSVScenarioFilePath.InitData = fileManager.openFilePathCSV();
            updateGUi();
        }

        private void button_Product_Click(object sender, EventArgs e)
        {
            cSVScenarioFilePath.ProductsList = fileManager.openFilePathCSV();
            updateGUi();
        }

        private void button_Warehouse_Click(object sender, EventArgs e)
        {
            cSVScenarioFilePath.WarehouseInitInventory = fileManager.openFilePathCSV();
            updateGUi();
        }

        private void button_Suppliers_List_Click(object sender, EventArgs e)
        {
            cSVScenarioFilePath.SuppliersList = fileManager.openFilePathCSV();
            updateGUi();
        }

        private void button_Customer_Order_List_Click(object sender, EventArgs e)
        {
            cSVScenarioFilePath.CustomersOrderList = fileManager.openFilePathCSV();
            updateGUi();
        }

        private void button_Future_Customer_Order_List_Click(object sender, EventArgs e)
        {
            cSVScenarioFilePath.FutureCustomersOrderList = fileManager.openFilePathCSV();
            updateGUi();
        }

        private void button_Supplier_Orders_List_Click(object sender, EventArgs e)
        {
            cSVScenarioFilePath.SuppliersOrderList = fileManager.openFilePathCSV();
            updateGUi();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainController.CreateXMLScenario(cSVScenarioFilePath);

        }

        private void CSVScenarioToXMLForm_Load(object sender, EventArgs e)
        {
            readyFiles();
            updateGUi();
        }
    }
}
