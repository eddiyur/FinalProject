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
    public partial class OrdersTable : Form
    {
        DataTable DT;
        public OrdersTable()
        {
            InitializeComponent();
        }

        public OrdersTable(DataTable dt)
        {
            InitializeComponent();
            DT = dt;
        }
        private void OrdersTable_Load(object sender, EventArgs e)
        {
            Width = 500;
            

            DataGridView dataGridView = new DataGridView();
            dataGridView.DataSource = DT;
            dataGridView.Width = Width;
            this.Controls.Add(dataGridView);

            

        }
    }
}
