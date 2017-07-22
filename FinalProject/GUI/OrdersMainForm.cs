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
    public partial class OrdersMainForm : Form
    {
        DataTable Dt;
        public OrdersMainForm(DataTable dt)
        {
            Dt = dt;
            InitializeComponent();
        }


        private void OrdersPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void OrdersForm_Load_1(object sender, EventArgs e)
        {
            GeneralDataGrid gdg = new GeneralDataGrid(Dt, Width, Height);
            gdg.Left = 0;
            gdg.Top = 0;
            gdg.Show();
            OrdersPanel.Controls.Add(gdg);
        }
    }
}
