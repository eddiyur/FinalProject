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
    public delegate void ClickDelegate();

    public partial class OrderViewForm : Form
    {
        Order order { get; set; }

        public ScreenSettings screenSettings;
        OrderFormType orderFormType;
        private List<ClickableDelegate> click_actions;
        //private ClickDelegate FirstButtonClick;
        //private ClickDelegate SecondButtonClick;


        public enum OrderFormType
        {
            ShowOrder,
            newOrder,
            SupplierOrderDelivered
        }

        public struct ScreenSettings
        {
            public int rowHeight;
        }

        public OrderViewForm(Order order)
        {
            this.orderFormType = OrderFormType.ShowOrder;
            this.order = order;
            InitializeComponent();
        }


        public OrderViewForm(Order order, OrderFormType orderFormType)
        {
            this.orderFormType = orderFormType;
            this.order = order;
            InitializeComponent();
        }



        //public OrderViewForm(Order order, ClickDelegate firstButtoClick, ClickDelegate secondButtonClick)
        //{
        //    this.orderFormType = OrderFormType.newOrder;
        //    this.order = order;
        //    FirstButtonClick = firstButtoClick;
        //    SecondButtonClick = secondButtonClick;
        //    InitializeComponent();
        //}


        private void CustomerOrderForm_Load(object sender, EventArgs e)
        {
            initScreenSetings();
            setStructure();
        }

        internal DataTable getPriceDataTable()
        { return PriceTable.ToDataTable(order.OrderProductsList); }

        private void initScreenSetings()
        {
            Width = 550;
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

            //DataTable dt = PriceTable.ToDataTable(order.OrderProductsList);

            DTPanel.Width = Width;
            DTPanel.Left = 0;
            DTPanel.Top = screenSettings.rowHeight * 7;
            DTPanel.Height = 100;


            GeneralDataGridForm GDG = new GeneralDataGridForm(getPriceDataTable, Width, DTPanel.Height, new List<int>(), new List<ClickableDelegate>());
            DTPanel.Controls.Add(GDG);
            GDG.Show();


            switch (orderFormType)
            {
                case OrderFormType.ShowOrder:
                    ShowCloseButton();
                    break;
                case OrderFormType.newOrder:
                    addNewOrderButtons();
                    break;
                case OrderFormType.SupplierOrderDelivered:
                    SupplierOrderDeliveredButtons();
                    break;
                default:
                    break;
            }
        }

        private void SupplierOrderDeliveredButtons()
        {
            Button closeButton = new Button();
            closeButton.Left = 10;
            closeButton.Top = DTPanel.Bottom + 10;
            closeButton.Text = "Aproved";
            closeButton.Click += SupplierOrderDeliveredAproved;
            this.Controls.Add(closeButton);

            this.ControlBox = true;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

        private void addNewOrderButtons()
        {
            Button approveButton = new Button();
            approveButton.Left = 10;
            approveButton.Top = DTPanel.Bottom + 10;
            approveButton.Text = "Approve";

            this.Controls.Add(approveButton);

            Button declineButton = new Button();
            declineButton.Left = approveButton.Right + 10;
            declineButton.Top = DTPanel.Bottom + 10;
            declineButton.Text = "Decline";
            this.Controls.Add(declineButton);

            if (order.OrderType == Order.OrderTypeEnum.CustomerOrder)
            {
                approveButton.Click += CustomerNewOrderApproved;
                declineButton.Click += CustomerNewOrderDecline;
            }
            else
            {
                approveButton.Click += SupplierNewOrderApproved;
                declineButton.Click += SupplierNewOrderDecline;
            }

        }

        private void ShowCloseButton()
        {
            Button closeButton = new Button();
            closeButton.Left = 10;
            closeButton.Top = DTPanel.Bottom + 10;
            closeButton.Text = "close";
            closeButton.Click += closeForm;
            this.Controls.Add(closeButton);

            this.ControlBox = true;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }



        private void newOrderButtons()
        {
            Button approveButton = new Button();
            approveButton.Left = 10;
            approveButton.Top = DTPanel.Bottom + 10;
            approveButton.Text = "Approve";
            this.Controls.Add(approveButton);

            Button declineButton = new Button();
            declineButton.Left = approveButton.Right + 10;
            declineButton.Top = DTPanel.Bottom + 10;
            declineButton.Text = "Decline";
            this.Controls.Add(declineButton);
        }

        private void SupplierOrderDeliveredAproved(object sender, EventArgs e)
        {
            MainController.SupplierOrderDeliveredAproved(order);
            Close();
        }
        private void CustomerNewOrderDecline(object sender, EventArgs e)
        {
            MainController.NewCustomerOrderDecline(order);
            Close();
        }

        private void CustomerNewOrderApproved(object sender, EventArgs e)
        {
            MainController.NewCustomerOrderApproved(order);
            Close();
        }

        private void SupplierNewOrderDecline(object sender, EventArgs e)
        {
            //MainController.NewCustomerOrderDecline(order);
            Close();
        }

        private void SupplierNewOrderApproved(object sender, EventArgs e)
        {
            MainController.NewSupplierOrderApproved(order);
            Close();
        }


        private void closeForm(object sender, EventArgs e)
        { Close(); }



        private void DTPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
