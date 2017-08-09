using OperationalTrainer.Data_Structures;
using OperationalTrainer.GUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using UtilitiesFileManager;

namespace FinalProject.GUI
{
    public partial class NewSupplierOrder : Form
    {

        List<string> ProductsNameList;
        //DataTable PossibleSuppliersdataTable;
        //int selectedIndex;
        int amount;
        string fileName;
        Order order;

        private List<string> getProductsNames()
        {
            List<string> productsName = new List<string>() { "p1", "p2" };
            return productsName;
        }

        private Order GenerateOrder()
        {
            double price = 10;
            Customer Customer = new Customer("customer1", "customer1");
            Supplier supplier = new Supplier("SUp1", "sup2");
            ProductClass product = new ProductClass("P1");
            Order.OrderTypeEnum OrderType = Order.OrderTypeEnum.SupplierOrder;

            PriceTable priceTable = new PriceTable(product, amount, price);
            List<PriceTable> pricetableList = new List<PriceTable>() { priceTable };

            DateTime currentTime = new DateTime(2017, 10, 10);
            DateTime deliveryTime = new DateTime(2017, 10, 10);

            string orderId = "orderID";
            order = new Order(supplier, OrderType, orderId, currentTime, deliveryTime, pricetableList);

            return order;
        }

        public NewSupplierOrder(string filePath)
        {
            InitializeComponent();
            fileName = filePath;
            ProductsNameList = getProductsNames();
            FileManager fm = new FileManager();
            dataGridView1.DataSource = fm.GetCSV(fileName); ;
            order = GenerateOrder();
        }

        private void NewSupplierOrder_Load(object sender, EventArgs e)
        {
            initScreenSetings();
            setStructure();
            //   selectedIndex = -1;
            amount = 1;
        }


        private void initScreenSetings()
        {
        }

        private void setStructure()
        {
            this.Left = 800;
            comboBoxProductsList.Items.AddRange(ProductsNameList.ToArray());

        }







        private DataTable generateDataTableForProduct()
        {
            FileManager fm = new FileManager();
            return fm.GetCSV(fileName);
        }


        private string extractProductID(string productLable)
        { return productLable.Substring(productLable.IndexOf("(") + 1, productLable.IndexOf(")") - 1); }




        private string GenerateOrderID(DateTime currentTime)
        {

            return "Supp_Order_" + currentTime.ToShortDateString() + currentTime.ToLongTimeString();
        }

        private void Nextbutton_Click(object sender, EventArgs e)
        {
            if (order != null)
            {
                OrderViewForm orderViewForm = new OrderViewForm(order, OrderViewForm.OrderFormType.newOrder);
                orderViewForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Supplier not chosen", "error");
            }

        }

        //private void back()
        //{
        //    orderViewForm.Close();
        //}

        //private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs cell)
        //{
        //    markRow(cell);
        //}

        private void CencelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBoxProductsList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }//end form
}
