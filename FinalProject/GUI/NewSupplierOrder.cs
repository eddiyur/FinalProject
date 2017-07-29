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
using static OperationalTrainer.Data_Structures.Supplier;

namespace FinalProject.GUI
{
    public delegate ProductClassList GetProductClassListDelegate();
    public delegate SuppliersList GetSuppliersListDelegate();

    public partial class NewSupplierOrder : Form
    {
        enum SuppliersListForProductTableColumnsNames
        {
            Supplier_Name,
            Supplier_ID,
            Supplier_Reliability,
            Price,
            Lead_Time
        }

        ProductClassList productsMetaData;
        SuppliersList suppliersList;
        List<string> ProductsNameList;
        DataTable PossibleSuppliersdataTable;
        int selectedIndex;

        public NewSupplierOrder(GetProductClassListDelegate getProductClassListDelegate, GetSuppliersListDelegate getSuppliersListDelegate)
        {
            productsMetaData = getProductClassListDelegate();
            suppliersList = getSuppliersListDelegate();
            InitializeComponent();
            ProductsNameList = getProductsNames();
        }

        private void NewSupplierOrder_Load(object sender, EventArgs e)
        {
            initScreenSetings();
            setStructure();
            selectedIndex = -1;
        }


        private void initScreenSetings()
        {
            Width = 450;
        }

        private void setStructure()
        {
            this.Left = 800;
            comboBoxProductsList.Items.AddRange(ProductsNameList.ToArray());

        }



        private List<string> getProductsNames()
        {
            List<string> productsName = new List<string>();
            foreach (ProductClass product in productsMetaData.ProductList)
                productsName.Add("(" + product.ProductID + ") " + product.ProductName);

            return productsName;
        }

        private ProductClass getProductFromcomboBoxProductsList(string productLable)
        { return productsMetaData.GetProduct(extractProductID(productLable)); }



        private DataTable generateDataTableForProduct(ProductClass product)
        {
            DataTable suppliersListForProduct = new DataTable();

            foreach (SuppliersListForProductTableColumnsNames columnName in Enum.GetValues(typeof(SuppliersListForProductTableColumnsNames)))
                suppliersListForProduct.Columns.Add(columnName.ToString());

            foreach (Supplier supplier in suppliersList.SupplierList)
            {
                foreach (PriceMatrixStruct ps in supplier.PriceMatrix)
                {
                    if (ps.product.Equals(product))
                    {
                        DataRow dRow = suppliersListForProduct.NewRow();
                        dRow[SuppliersListForProductTableColumnsNames.Supplier_Name.ToString()] = supplier.Name;
                        dRow[SuppliersListForProductTableColumnsNames.Supplier_ID.ToString()] = supplier.ID;
                        dRow[SuppliersListForProductTableColumnsNames.Supplier_Reliability.ToString()] = supplier.Reliability;
                        dRow[SuppliersListForProductTableColumnsNames.Price.ToString()] = ps.UnitPrice;
                        dRow[SuppliersListForProductTableColumnsNames.Lead_Time.ToString()] = ps.LeadTime;
                        suppliersListForProduct.Rows.Add(dRow);
                    }
                }
            }
            return suppliersListForProduct;
        }//end generateDataTableForProduct

        private string extractProductID(string productLable)
        { return productLable.Substring(productLable.IndexOf("(") + 1, productLable.IndexOf(")") - 1); }

        private void comboBoxProductsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProductClass selectectProduct = getProductFromcomboBoxProductsList(comboBoxProductsList.SelectedItem.ToString());
            PossibleSuppliersdataTable = generateDataTableForProduct(selectectProduct);
            dataGridView1.DataSource = PossibleSuppliersdataTable;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string textboxtText = textBox1.Text;

            foreach (char ch in textboxtText.ToCharArray())
            {
                if (!char.IsDigit(ch))
                {
                    MessageBox.Show("Digits only", "Error");
                    textBox1.Text = "";
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs cell)
        {
            dataGridView1.ClearSelection();
            selectedIndex = cell.RowIndex;
            dataGridView1.Rows[selectedIndex].Selected = true;

            dataGridView1.Rows[selectedIndex].DefaultCellStyle.BackColor = Color.Red;

                }
    }//end form
}
