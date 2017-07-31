using FinalProject.Controllers;
using OperationalTrainer.GUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OperationalTrainer.Data_Structures;

namespace FinalProject.GUI
{
    public partial class MainForm : Form
    {
        GeneralDataGridForm bankMainForm;
        GeneralDataGridForm WarehouseMainForm;
        GeneralDataGridForm CustomerMainForm;
        GeneralDataGridForm SupplierMainForm;

        List<IDataUpdatble> updatebleForms;


        private FormParameters formParameters;//structure of form parameters
        private FormVisualElements formVisualElements;//structure of form visual elements


        struct FormParameters
        {
            public int FormWidth;
            public int FormHight;
            public int FormActiveHeight;
            public int FormActiveTop;
            public int FormMargins;
            public int MiddleBorder;
            public double SmallToBigRatio;
            public int SmallPanelsHight;
            public int BigPanelsHight;
        }

        struct FormVisualElements
        {
            public Panel CustomerOrderPanel;
            public Panel SupplierOrderPanel;
            public Panel BankPanel;
            public Label TimeLabel;
            public Panel WarehousePanel;
            public Panel ProductionPanel;
        }





        public MainForm()
        {
            InitializeComponent();
            formParameters = new FormParameters();
            formVisualElements = new FormVisualElements();
            updatebleForms = new List<IDataUpdatble>();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            initialFormSetings();

        }

        private void initialFormSetings()
        {
            setScreenSize();
            setPanelsPosition();
            UpdateGUI();
        }

        /// <summary>
        /// Set screen size and parameters
        /// </summary>
        private void setScreenSize()
        {
            this.WindowState = FormWindowState.Maximized;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            int minWidth = 1200;
            int minHight = 720;

            if (screenWidth > minWidth)
                Width = screenWidth;
            else
                Width = minWidth;

            if (screenHeight > minHight)
                Height = Screen.PrimaryScreen.WorkingArea.Bottom;
            else
                Height = minHight;

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            this.MaximizeBox = false;

            formParameters.FormHight = Height;
            formParameters.FormWidth = Width;
            formParameters.FormMargins = 5;
            formParameters.FormActiveTop = menuStrip.Bottom + formParameters.FormMargins;
            formParameters.FormActiveHeight = formParameters.FormHight - formParameters.FormActiveTop - 10 * formParameters.FormMargins;

            formParameters.SmallToBigRatio = 0.33;
            formParameters.MiddleBorder = (int)(formParameters.FormWidth * formParameters.SmallToBigRatio);

            formParameters.BigPanelsHight = (int)formParameters.FormActiveHeight / 2;
            formParameters.SmallPanelsHight = (int)formParameters.FormActiveHeight / 3;

        }//end setScreenSize

        public void GuiParameters()
        {
            nextTickToolStripMenuItem.Enabled = MainParameters.GameStarted;
        }

        public void UpdateGUI()
        {
            GuiParameters();
            formVisualElements.TimeLabel.Text = transferToTimeLabel(MainController.GetCurrentTime());

            foreach (var form in updatebleForms)
                form.UpdateData();

        }

        /// <summary>
        /// Set screen visual Elements
        /// </summary>
        public void setPanelsPosition()
        {
            this.AutoScroll = true;
            //TimeLabel
            formVisualElements.TimeLabel = new Label();
            formVisualElements.TimeLabel.Left = formParameters.FormWidth - 300;
            formVisualElements.TimeLabel.Top = formParameters.FormActiveTop;
            formVisualElements.TimeLabel.AutoSize = false;
            formVisualElements.TimeLabel.TextAlign = ContentAlignment.TopCenter;
            formVisualElements.TimeLabel.Width = 200;
            this.Controls.Add(formVisualElements.TimeLabel);

            //WarehousePanel
            formVisualElements.WarehousePanel = new Panel();
            formVisualElements.WarehousePanel.Top = formVisualElements.TimeLabel.Bottom;
            formVisualElements.WarehousePanel.Left = formParameters.MiddleBorder + formParameters.FormMargins;
            formVisualElements.WarehousePanel.Width = formParameters.FormWidth - formVisualElements.WarehousePanel.Left;
            formVisualElements.WarehousePanel.Height = formParameters.BigPanelsHight;
            formVisualElements.WarehousePanel.AutoScroll = true;
            this.Controls.Add(formVisualElements.WarehousePanel);

            //CustomerOrderPanel
            formVisualElements.CustomerOrderPanel = new Panel();
            formVisualElements.CustomerOrderPanel.Top = formParameters.FormActiveTop;
            formVisualElements.CustomerOrderPanel.Left = formParameters.FormMargins;
            formVisualElements.CustomerOrderPanel.Width = formParameters.MiddleBorder - 2 * formParameters.FormMargins;
            formVisualElements.CustomerOrderPanel.Height = formParameters.SmallPanelsHight;
            formVisualElements.CustomerOrderPanel.AutoScroll = true;
            this.Controls.Add(formVisualElements.CustomerOrderPanel);

            //SupplierOrderPanel
            formVisualElements.SupplierOrderPanel = new Panel();
            formVisualElements.SupplierOrderPanel.Top = formParameters.SmallPanelsHight + formParameters.FormMargins;
            formVisualElements.SupplierOrderPanel.Left = formParameters.FormMargins;
            formVisualElements.SupplierOrderPanel.Width = formParameters.MiddleBorder - 2 * formParameters.FormMargins;
            formVisualElements.SupplierOrderPanel.Height = formParameters.SmallPanelsHight;
            formVisualElements.SupplierOrderPanel.AutoScroll = true;
            this.Controls.Add(formVisualElements.SupplierOrderPanel);

            //BankPanel
            formVisualElements.BankPanel = new Panel();
            formVisualElements.BankPanel.Top = formParameters.SmallPanelsHight * 2 + formParameters.FormMargins;
            formVisualElements.BankPanel.Left = formParameters.FormMargins;
            formVisualElements.BankPanel.Width = formParameters.MiddleBorder - 2 * formParameters.FormMargins;
            formVisualElements.BankPanel.Height = formParameters.SmallPanelsHight;
            formVisualElements.BankPanel.AutoScroll = true;
            this.Controls.Add(formVisualElements.BankPanel);

            createSubForms();
        }//end setPanelsPosition

       
        private void createSubForms()
        {
            //  bankMainForm = new OrdersMainForm(MainController.GetBankDataTable(), formVisualElements.BankPanel.Width, formVisualElements.BankPanel.Height);
            bankMainForm = new GeneralDataGridForm(MainController.GetBankDataTable, formVisualElements.BankPanel.Width, formVisualElements.BankPanel.Height, new List<int>(), new List<ClickableDelegate>());
            formVisualElements.BankPanel.Controls.Add(bankMainForm);
            updatebleForms.Add(bankMainForm);
            bankMainForm.Show();

            //   WarehouseMainForm = new OrdersMainForm(MainController.GetWarehouseDataTable(), formVisualElements.WarehousePanel.Width, formVisualElements.WarehousePanel.Height);
            WarehouseMainForm = new GeneralDataGridForm(MainController.GetWarehouseDataTable, formVisualElements.WarehousePanel.Width, formVisualElements.WarehousePanel.Height, new List<int>(), new List<ClickableDelegate>());
            formVisualElements.WarehousePanel.Controls.Add(WarehouseMainForm);
            updatebleForms.Add(WarehouseMainForm);
            WarehouseMainForm.Show();

            //            CustomerMainForm = new OrdersMainForm(MainController.GetCustomerOrdersDataTable(), formVisualElements.CustomerOrderPanel.Width, formVisualElements.CustomerOrderPanel.Height);
            CustomerMainForm = new GeneralDataGridForm(MainController.GetCustomerOrdersDataTable, formVisualElements.CustomerOrderPanel.Width, formVisualElements.CustomerOrderPanel.Height, new List<int>(), new List<ClickableDelegate>());
            formVisualElements.CustomerOrderPanel.Controls.Add(CustomerMainForm);
            updatebleForms.Add(CustomerMainForm);
            CustomerMainForm.Show();

            // SupplierMainForm = new OrdersMainForm(MainController.GetSupplierOrdersDataTable(), formVisualElements.SupplierOrderPanel.Width, formVisualElements.SupplierOrderPanel.Height);
            SupplierMainForm = new GeneralDataGridForm(MainController.GetSupplierOrdersDataTable, formVisualElements.SupplierOrderPanel.Width, formVisualElements.SupplierOrderPanel.Height, new List<int>(), new List<ClickableDelegate>());
            formVisualElements.SupplierOrderPanel.Controls.Add(SupplierMainForm);
            updatebleForms.Add(SupplierMainForm);
            SupplierMainForm.Show();
        }

        private string transferToTimeLabel(DateTime currentTime)
        { return currentTime.ToShortDateString() + " " + currentTime.ToShortTimeString(); }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //test();
        }

        private void nextTickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainController.StartClock();
        }

        private void newSupplierOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewSupplierOrder newSupplierOrder = new NewSupplierOrder(MainController.GetProductsMetaData, MainController.GetSuppliersList);
            newSupplierOrder.ShowDialog();
        }

        private void loadScenarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainController.LoadScenario();
        }

        private void cSVScenarioToXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CSVScenarioToXMLForm f = new CSVScenarioToXMLForm();
            f.ShowDialog();
        }
    }//end form

}
