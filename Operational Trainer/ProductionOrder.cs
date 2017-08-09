using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Operational_Trainer
{
    public partial class ProductionOrder : Form
    {
        public ProductionOrder()
        {
            InitializeComponent();
        }

        private void ProductionOrder_Load(object sender, EventArgs e)
        {
            comboBoxProductsList.DataSource = getProductsNames();
        }

        private List<string> getProductsNames()
        {
            List<string> productsName = new List<string>() { "p1", "p2" };
            return productsName;
        }

        private void comboBoxProductsList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
