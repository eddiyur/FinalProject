using OperationalTrainer.GUI;
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

    public partial class OrdersMainForm : Form, IDataUpdatble
    {
        DataTable dataTable;
        GeneralDataGridForm dataGridForm;
        public OrdersMainForm(DataTable dataTable)
        {
            this.TopLevel = false;
            this.dataTable = dataTable;
            InitializeComponent();
        }

        public OrdersMainForm(DataTable dataTable, int width, int height)
        {
            this.TopLevel = false;
            this.Width = width;
            this.Height = height;
            dataGridForm = new GeneralDataGridForm(dataTable, Width, Height, new List<int>(), new List<ClickableDelegate>());
            this.dataTable = dataTable;
            InitializeComponent();
        }

        private void OrdersPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void OrdersForm_Load_1(object sender, EventArgs e)
        {
            dataGridForm.Left = 0;
            dataGridForm.Top = 0;
            dataGridForm.Show();
            OrdersPanel.Controls.Add(dataGridForm);
        }

        public void UpdateData()
        {
            dataGridForm.Update();
            throw new NotImplementedException();
        }
    }
}
