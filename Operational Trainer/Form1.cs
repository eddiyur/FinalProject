using FinalProject.GUI;
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

namespace Operational_Trainer
{
    public partial class Form1 : Form
    {
        string folderPath= @"C:\Users\eyurkovs\Desktop\final progect\FinalProject\FinalProject\Operational Trainer\DataSets\";
        FileManager fileManager;
        public Form1()
        {
            InitializeComponent();
        }

        private void loadScenarioToolStripMenuItem_Click(object sender, EventArgs e)
        {

            fileManager.openFilePathXML();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fileManager = new FileManager();
            dataGridViewCU.DataSource = fileManager.GetCSV(folderPath + "customer.csv");
            dataGridViewPU.DataSource = fileManager.GetCSV(folderPath + "PurchaseOrders.csv");
            dataGridViewPr.DataSource = fileManager.GetCSV(folderPath + "ProductionOrder.csv");
            dataGridViewBank.DataSource = fileManager.GetCSV(folderPath + "bank.csv");
            dataGridViewWa.DataSource = fileManager.GetCSV(folderPath + "Warehouse.csv");
            dataGridViewTools.DataSource = fileManager.GetCSV(folderPath + "Tools.csv");


        }

        private void purchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewSupplierOrder newSupplierOrder = new NewSupplierOrder(folderPath+"Suppliers_List.csv");
            newSupplierOrder.ShowDialog();
        }
    }
}
