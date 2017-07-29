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

    public delegate void ClickableDelegate();

    public partial class GeneralDataGridForm : Form
    {
        DataTable dataTable;
        private readonly List<int> clickable_cols;
        private readonly List<ClickableDelegate> click_col_actions;

        public GeneralDataGridForm(DataTable dataTable, int width, int height, List<int> clickable_idx, List<ClickableDelegate> click_actions)
        {
            InitializeComponent();
            this.dataTable = dataTable;
            clickable_cols = new List<int>(clickable_idx);
            click_col_actions = new List<ClickableDelegate>(click_actions);

            Width = width;
            Height = height;

            dataGridView1.Top = 0;
            dataGridView1.Height = this.Height;
            dataGridView1.Width = this.Width;
            this.TopLevel = false;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs cell)
        {
            int cellRowIdx = cell.RowIndex;
            int cellColIdx = cell.ColumnIndex;
            if (clickable_cols.Contains(cellColIdx))
                click_col_actions[cellColIdx].Invoke();
        }

        private void GeneralDataGrid_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dataTable;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
