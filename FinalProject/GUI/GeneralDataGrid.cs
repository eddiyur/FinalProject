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
    public partial class GeneralDataGrid : Form
    {
        DataTable DT;
        public GeneralDataGrid(DataTable dt,int width, int height)
        {
            InitializeComponent();
            DT = dt;

            Width = width;
            Height = height;

            dataGridView1.Top = 0;
            dataGridView1.Height = this.Height;
            dataGridView1.Width = this.Width;
            this.TopLevel = false;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int a = e.RowIndex;
            int c = e.ColumnIndex;
            var b = DT.Rows[a][2].ToString();
        }

        private void GeneralDataGrid_Load(object sender, EventArgs e)
        {
            
            dataGridView1.DataSource = DT;
        }
    }
}
