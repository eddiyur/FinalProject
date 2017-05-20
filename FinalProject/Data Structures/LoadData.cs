using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data_Structures
{
    public class LoadData
    {
        public LoadData() { }

        public void loadProducts()
        {
            UtilitiesFileManager.FileManager fileManager = new UtilitiesFileManager.FileManager();
            string folderPath = fileManager.ExePath() + "dataSets\\";

            List<Customer> customerList = getCustumerList(folderPath);
            List<Supplier> supplierList = getSuppliersList(folderPath);
            List<ProductClass> productList = getProductList(folderPath);
        }

        public List<Customer> getCustumerList(string folderPath)
        {
            UtilitiesFileManager.FileManager fileManager = new UtilitiesFileManager.FileManager();
            DataTable customersTable = fileManager.GetCSV(folderPath + "CustomersTable.csv");
            List<Customer> customerList = new List<Customer>();

            foreach (DataRow row in customersTable.Rows)
            {
                Customer customer = new Customer(row[0].ToString(), row[1].ToString());
                customerList.Add(customer);
            }
            return customerList;
        }

        public List<Supplier> getSuppliersList(string folderPath)
        {
            UtilitiesFileManager.FileManager fileManager = new UtilitiesFileManager.FileManager();
            DataTable suppliersTable = fileManager.GetCSV(folderPath + "SuppliersTable.csv");
            List<Supplier> suppliersList = new List<Supplier>();

            foreach (DataRow row in suppliersTable.Rows)
            {
                Supplier supplier = new Supplier(row[0].ToString(), row[1].ToString(), Double.Parse(row[2].ToString()));
                suppliersList.Add(supplier);
            }
            return suppliersList;
        }

        public List<ProductClass> getProductList(string folderPath)
        {
            UtilitiesFileManager.FileManager fileManager = new UtilitiesFileManager.FileManager();
            DataTable productTable = fileManager.GetCSV(folderPath + "ProductTable.csv");
            List<ProductClass> productList = new List<ProductClass>();

            foreach (DataRow row in productTable.Rows)
            {
                ProductClass product = new ProductClass(row[0].ToString(), row[1].ToString(), Double.Parse(row[2].ToString()));
                productList.Add(product);
            }
            int a=0;
          //  ProductClass producttest = productList.Select;



            return productList;
        }

    }//end class loadData
}
