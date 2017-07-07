using FinalProject.Data_Structures;
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
            DataTable dt =  DB.customerOrderList.ToDataTable();
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
           
            string FolderPath = LoadData.getTempFolderPath();
            string filePath = FolderPath +"ProductCSV.csv";
            FileManager fm = new FileManager();
            DataTable dt = fm.GetCSV(filePath);

            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.CreateElement("dataset");
            XmlAttribute att = null;
            doc.InsertBefore(xmlDeclaration, doc.DocumentElement);
            doc.AppendChild(root);

            var productsList = doc.CreateElement("ProductsList");
            root.AppendChild(productsList);

            foreach (DataRow row in dt.Rows)
            {
                var product = doc.CreateElement("Product");
                foreach (DataColumn column in dt.Columns)
                {
                    var tagName = doc.CreateElement(column.ColumnName);
                    tagName.InnerText = row[column].ToString();
                    product.AppendChild(tagName);
                }
                productsList.AppendChild(product);
            }

            using (var writer = new XmlTextWriter(FolderPath + "eddi.xml", Encoding.UTF8) { Formatting = Formatting.Indented })
            {
                doc.WriteTo(writer);
            }

        }
    }
}
