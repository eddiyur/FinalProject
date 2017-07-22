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
    public partial class CustomerOrdersForm : Form
    {
        DataTable Dt;
        public CustomerOrdersForm(DataTable dt)
        {
            Dt = dt;
        }

        private void CustomerOrdersForm_Load(object sender, EventArgs e)
        {
            GeneralDataGrid gdg = new GeneralDataGrid(Dt, Width, Height);
            gdg.Left = 0;
            gdg.Top = 0;
            gdg.Show();
            OrdersPanel.Controls.Add(gdg);
           

        }

        private void OrdersPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
