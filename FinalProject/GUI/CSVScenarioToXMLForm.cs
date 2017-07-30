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

        private void updateGUi()
        {
            label2.Text = cSVScenarioFilePath.InitData;
            label3.Text = cSVScenarioFilePath.ProductsList;
            label7.Text= cSVScenarioFilePath.WarehouseInitInventory;
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
    }
}
