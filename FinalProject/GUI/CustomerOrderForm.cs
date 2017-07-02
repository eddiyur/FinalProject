using FinalProject.Data_Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject.GUI
{
    public partial class CustomerOrderForm : Form
    {
        Order order { get; set; }
        public ScreenSettings screenSettings;
        public string ReturnValue1 { get; set; }

        public struct ScreenSettings
        {
            public int rowHeight;
        }

        public CustomerOrderForm(Order order)
        {
            this.order = order;
            InitializeComponent();
        }

        private void CustomerOrderForm_Load(object sender, EventArgs e)
        {
            initScreenSetings();
            setStructure();

        }

        private void initScreenSetings()
        {
            Width = 450;
            screenSettings.rowHeight = 25;
        }
        private void setStructure()
        {
            this.Left = 800;
            
            // orred type
             Label orderTypeLable = new Label();
            orderTypeLable.Top = 0;
            orderTypeLable.Left = 0;
            orderTypeLable.Text = "Order Type: ";
            orderTypeLable.Width = 160;
            this.Controls.Add(orderTypeLable);

            TextBox orderTypeTextBox = new TextBox();
            orderTypeTextBox.Text = order.OrderType.ToString();
            orderTypeTextBox.Left = orderTypeLable.Right;
            orderTypeTextBox.Top = 0;
            this.Controls.Add(orderTypeTextBox);

            ///customer name

            Label customerNameLable = new Label();
            customerNameLable.Text = "Customer Name: ";
            customerNameLable.Top = screenSettings.rowHeight;
            customerNameLable.Width = 160;
            this.Controls.Add(customerNameLable);

            TextBox customerNameTextBox = new TextBox();
            customerNameTextBox.Text = order.Person.Name;
            customerNameTextBox.Left = customerNameLable.Right;
            customerNameTextBox.Top= screenSettings.rowHeight;
            this.Controls.Add(customerNameTextBox);


            ///customer ID

            Label customerIDLable = new Label();
            customerIDLable.Text = "Customer ID: ";
            customerIDLable.Top = screenSettings.rowHeight*2;
            customerIDLable.Width = 160;
            this.Controls.Add(customerIDLable);

            TextBox customerIDTextBox = new TextBox();
            customerIDTextBox.Text = order.Person.ID;
            customerIDTextBox.Left = customerIDLable.Right;
            customerIDTextBox.Top = screenSettings.rowHeight*2;
            this.Controls.Add(customerIDTextBox);


            ///customer OrderID
            Label customerOrderIDLable = new Label();
            customerOrderIDLable.Text = "Customer OrderID: ";
            customerOrderIDLable.Top = screenSettings.rowHeight*3;
            customerOrderIDLable.Width = 160;
            this.Controls.Add(customerOrderIDLable);

            TextBox customerOrderIDTextBox = new TextBox();
            customerOrderIDTextBox.Text = order.OrderID;
            customerOrderIDTextBox.Left = customerOrderIDLable.Right;
            customerOrderIDTextBox.Top = screenSettings.rowHeight*3;
            this.Controls.Add(customerOrderIDTextBox);

            ///customer OrderDate
            Label customerOrderDateLable = new Label();
            customerOrderDateLable.Text = "Customer OrderDate: ";
            customerOrderDateLable.Top = screenSettings.rowHeight*4;
            customerOrderDateLable.Width = 160;
            this.Controls.Add(customerOrderDateLable);

            TextBox customerOrderDateTextBox = new TextBox();
            customerOrderDateTextBox.Text = order.OrderDate.ToShortDateString();
            customerOrderDateTextBox.Left = customerOrderDateLable.Right;
            customerOrderDateTextBox.Top = screenSettings.rowHeight*4;
            this.Controls.Add(customerOrderDateTextBox);
            
            ///customer OrderDeliveryDate
            Label customerOrderDeliveryDateLable = new Label();
            customerOrderDeliveryDateLable.Text = "Customer OrderDeliveryDate: ";
            customerOrderDeliveryDateLable.Top = screenSettings.rowHeight * 5;
            customerOrderDeliveryDateLable.Width = 160;
            this.Controls.Add(customerOrderDeliveryDateLable);

            TextBox customerOrderDeliveryDateTextBox = new TextBox();
            customerOrderDeliveryDateTextBox.Text = order.OrderDeliveryDate.ToShortDateString();
            customerOrderDeliveryDateTextBox.Left = customerOrderDeliveryDateLable.Right;
            customerOrderDeliveryDateTextBox.Top = screenSettings.rowHeight * 5;
            this.Controls.Add(customerOrderDeliveryDateTextBox);

            ///customer OrderStatus
            Label customerOrderStatusLable = new Label();
            customerOrderStatusLable.Text = "Customer OrderStatus: ";
            customerOrderStatusLable.Top = screenSettings.rowHeight * 6;
            customerOrderStatusLable.Width = 160;
            this.Controls.Add(customerOrderStatusLable);

            TextBox customerOrderStatusTextBox = new TextBox();
            customerOrderStatusTextBox.Text = order.OrderStatus.ToString(); ;
            customerOrderStatusTextBox.Left = customerOrderStatusLable.Right;
            customerOrderStatusTextBox.Top = screenSettings.rowHeight * 6;
            this.Controls.Add(customerOrderStatusTextBox);

            DataTable dt = PriceTable.ToDataTable(order.OrderProductsList);

            DataGridView orderProductsListDataGridView = new DataGridView();
            orderProductsListDataGridView.DataSource = dt;
            orderProductsListDataGridView.Left = 0;
            orderProductsListDataGridView.Top = screenSettings.rowHeight * 7;
            orderProductsListDataGridView.Width = Width;
            this.Controls.Add(orderProductsListDataGridView);

            Button OkButton = new Button();
            OkButton.Text = "return order ID";
            OkButton.Left = 300;
            this.Controls.Add(OkButton);
            OkButton.Click += okButton_ClicK;
        }

        void okButton_ClicK(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.ReturnValue1 =order.OrderID;
            this.Close();
        }

    }
}
