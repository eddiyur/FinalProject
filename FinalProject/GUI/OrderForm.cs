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

namespace OperationalTrainer.GUI
{
    public partial class OrderForm : Form
    {
        Order order { get; set; }
        public ScreenSettings screenSettings;

        public string ReturnValue1 { get; set; }

        public struct ScreenSettings
        {
            public int rowHeight;
        }

        public OrderForm(Order order)
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

            // order type
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
            orderTypeTextBox.Enabled = false;
            this.Controls.Add(orderTypeTextBox);

            ///person name

            Label personNameLable = new Label();
            personNameLable.Text = "Customer Name: ";
            personNameLable.Top = screenSettings.rowHeight;
            personNameLable.Width = 160;
            this.Controls.Add(personNameLable);

            TextBox personNameTextBox = new TextBox();
            personNameTextBox.Text = order.Person.Name;
            personNameTextBox.Left = personNameLable.Right;
            personNameTextBox.Top = screenSettings.rowHeight;
            personNameTextBox.Enabled = false;
            this.Controls.Add(personNameTextBox);


            ///person ID

            Label personIDLable = new Label();
            personIDLable.Text = "Customer ID: ";
            personIDLable.Top = screenSettings.rowHeight * 2;
            personIDLable.Width = 160;
            this.Controls.Add(personIDLable);

            TextBox personIDTextBox = new TextBox();
            personIDTextBox.Text = order.Person.ID;
            personIDTextBox.Left = personIDLable.Right;
            personIDTextBox.Top = screenSettings.rowHeight * 2;
            personIDTextBox.Enabled = false;
            this.Controls.Add(personIDTextBox);


            /// OrderID
            Label OrderIDLable = new Label();
            OrderIDLable.Text = "OrderID: ";
            OrderIDLable.Top = screenSettings.rowHeight * 3;
            OrderIDLable.Width = 160;
            this.Controls.Add(OrderIDLable);

            TextBox OrderIDTextBox = new TextBox();
            OrderIDTextBox.Text = order.OrderID;
            OrderIDTextBox.Left = OrderIDLable.Right;
            OrderIDTextBox.Top = screenSettings.rowHeight * 3;
            OrderIDTextBox.Enabled = false;
            this.Controls.Add(OrderIDTextBox);

            ///OrderDate
            Label OrderDateLable = new Label();
            OrderDateLable.Text = "Order Date: ";
            OrderDateLable.Top = screenSettings.rowHeight * 4;
            OrderDateLable.Width = 160;
            this.Controls.Add(OrderDateLable);

            TextBox OrderDateTextBox = new TextBox();
            OrderDateTextBox.Text = order.OrderDate.ToShortDateString();
            OrderDateTextBox.Left = OrderDateLable.Right;
            OrderDateTextBox.Top = screenSettings.rowHeight * 4;
            OrderDateTextBox.Enabled = false;
            this.Controls.Add(OrderDateTextBox);

            ///OrderDeliveryDate
            Label OrderDeliveryDateLable = new Label();
            OrderDeliveryDateLable.Text = "Order Delivery Date: ";
            OrderDeliveryDateLable.Top = screenSettings.rowHeight * 5;
            OrderDeliveryDateLable.Width = 160;
            this.Controls.Add(OrderDeliveryDateLable);

            TextBox OrderDeliveryDateTextBox = new TextBox();
            OrderDeliveryDateTextBox.Text = order.OrderDeliveryDate.ToShortDateString();
            OrderDeliveryDateTextBox.Left = OrderDeliveryDateLable.Right;
            OrderDeliveryDateTextBox.Top = screenSettings.rowHeight * 5;
            OrderDeliveryDateTextBox.Enabled = false;
            this.Controls.Add(OrderDeliveryDateTextBox);

            ///Order Status
            Label OrderStatusLable = new Label();
            OrderStatusLable.Text = "Order Status: ";
            OrderStatusLable.Top = screenSettings.rowHeight * 6;
            OrderStatusLable.Width = 160;
            this.Controls.Add(OrderStatusLable);

            TextBox OrderStatusTextBox = new TextBox();
            OrderStatusTextBox.Text = order.OrderStatus.ToString(); ;
            OrderStatusTextBox.Left = OrderStatusLable.Right;
            OrderStatusTextBox.Top = screenSettings.rowHeight * 6;
            OrderStatusTextBox.Enabled = false;
            this.Controls.Add(OrderStatusTextBox);

            DataTable dt = PriceTable.ToDataTable(order.OrderProductsList);

            DTPanel.Width = Width;
            DTPanel.Left = 0;
            DTPanel.Top = screenSettings.rowHeight * 7;
            DTPanel.Height = 100;

            GeneralDataGrid GDG = new GeneralDataGrid(dt, Width, DTPanel.Height);
            DTPanel.Controls.Add(GDG);
            GDG.Show();

            AddCloseButton();


        }

        void AddCloseButton()
        {
            Button b3 = new Button();
            b3.Left = 10;
            b3.Top = DTPanel.Bottom;
            b3.Text = "close";
            b3.Click += a;
            this.Controls.Add(b3);
        }


        private void a(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           MainController.NewOrderArrived(this.order);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainController.NewCustomerOrderEventEnd();
        }
    }
}
