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
    public partial class GeneralDataGrid : Form
    {
        DataTable DT;
        public GeneralDataGrid(DataTable dt)
        {
            InitializeComponent();
            DT = dt;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int a = e.RowIndex;
            var b = DT.Rows[a][2].ToString();
        }

        private void GeneralDataGrid_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DT;
        }
    }
}
