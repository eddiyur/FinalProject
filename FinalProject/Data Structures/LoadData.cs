using FinalProject.Data_Structures;
using FinalProject.Logic.Prediction;
using FinalProject.Logic.Warehouse;
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
        public object WarehouseCalss { get; private set; }

        public LoadData() { }

        public void loadProducts()
        {
            UtilitiesFileManager.FileManager fileManager = new UtilitiesFileManager.FileManager();
            string folderPath = fileManager.ExePath() + "dataSets\\";

            ProductClassList productList = new ProductClassList();
            productList.ProductList = getProductList(folderPath);

            SuppliersList suppliersList = new SuppliersList();
            suppliersList = getSuppliersList(folderPath, productList);


            List<Customer> customerList = getCustomerList(folderPath);
            List<Order> orderList = getOrderList(folderPath);

            Customer p = customerList[0];
            var a = customerList.IndexOf(p);

            Prediction predictionManager = new Prediction();
            predictionManager.PredictionManager(orderList);
        }

        private List<Customer> getCustomerList(string folderPath)
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

        private SuppliersList getSuppliersList(string folderPath, ProductClassList productList)
        {
            UtilitiesFileManager.FileManager fileManager = new UtilitiesFileManager.FileManager();
            DataTable suppliersTable = fileManager.GetCSV(folderPath + "SuppliersTable.csv");
            SuppliersList suppliersList = new SuppliersList();

            foreach (DataRow row in suppliersTable.Rows)
            {
                Supplier supplier = new Supplier(row[0].ToString(), row[1].ToString(), Double.Parse(row[2].ToString()));
                suppliersList.AddSupploer(supplier);
            }

            suppliersList = getSuppliersMatrix(suppliersList, productList, folderPath);
            return suppliersList;
        }

        enum SuppliersPriceMatrixFileColumnsName
        {
            SupplierID,
            ProductID,
            Cost,
            amount,
            deliveryDate
        };

        private SuppliersList getSuppliersMatrix(SuppliersList suppliersList, ProductClassList productList, string folderPath)
        {
            UtilitiesFileManager.FileManager fileManager = new UtilitiesFileManager.FileManager();
            DataTable SuppliersPriceMatrix = fileManager.GetCSV(folderPath + "SuppliersPriceMatrix.csv");

            foreach (DataRow row in SuppliersPriceMatrix.Rows)
            {
                Supplier supplier = suppliersList.GetSupploer(row[SuppliersPriceMatrixFileColumnsName.SupplierID.ToString()].ToString());

                List<Supplier.PriceMatrixStruct> priceMatrixStructList = new List<Supplier.PriceMatrixStruct>();

                ProductClass product = productList.GetProduct(row[SuppliersPriceMatrixFileColumnsName.ProductID.ToString()].ToString());
                PriceTable pricetable = new PriceTable(product,
                   Int32.Parse( row[SuppliersPriceMatrixFileColumnsName.amount.ToString()].ToString()),
                   Double.Parse(row[SuppliersPriceMatrixFileColumnsName.Cost.ToString()].ToString()));

                Supplier.PriceMatrixStruct priceMatrixStruct = new Supplier.PriceMatrixStruct();
                priceMatrixStruct.DeliveryTime = Int32.Parse(row[SuppliersPriceMatrixFileColumnsName.deliveryDate.ToString()].ToString());
                priceMatrixStruct.priceTable = pricetable;

                supplier.PriceMatrix.Add(priceMatrixStruct);
            }
            return suppliersList;


        }//end getSuppliersMatrix

        private List<ProductClass> getProductList(string folderPath)
        {
            UtilitiesFileManager.FileManager fileManager = new UtilitiesFileManager.FileManager();
            DataTable productTable = fileManager.GetCSV(folderPath + "ProductTable.csv");
            List<ProductClass> productList = new List<ProductClass>();

            foreach (DataRow row in productTable.Rows)
            {
                ProductClass product = new ProductClass(row[0].ToString(), row[1].ToString(), Double.Parse(row[2].ToString()));
                productList.Add(product);
            }

            // int a = 0;
            //  ProductClass producttest = productList.Select;

            //List<ProductClass> query2 = productList.Where(product => product.ProductName== "Product_A").ToList();

            return productList;
        }

        enum OrderListHeders
        {
            OrderID,
            OrderDate,
            OrderDeliveryDate,
            Product,
            Amount,
            Cost
        };

        private List<Order> getOrderList(string folderPath)
        {

            PersonClass person = new PersonClass("CostumerTest", "ID1", PersonTypeEnum.Customer);

            UtilitiesFileManager.FileManager fileManager = new UtilitiesFileManager.FileManager();
            DataTable orderTable = fileManager.GetCSV(folderPath + "OrderList.csv");
            List<Order> orderList = new List<Order>();


            foreach (DataRow row in orderTable.Rows)
            {
                ProductClass product = new ProductClass(row[OrderListHeders.Product.ToString()].ToString(), row[OrderListHeders.Product.ToString()].ToString(), 1);
                List<PriceTable> priceTableList = new List<PriceTable>();
                PriceTable priceTable = new PriceTable(product, int.Parse(row[OrderListHeders.Amount.ToString()].ToString()), double.Parse(row[OrderListHeders.Cost.ToString()].ToString()));
                priceTableList.Add(priceTable);
                Order order = new Order(person,
                    Order.OrderTypeEnum.CustomerOrder,
                    row[OrderListHeders.OrderID.ToString()].ToString(),
                   DateTime.Parse(row[OrderListHeders.OrderDate.ToString()].ToString()),
                   DateTime.Parse(row[OrderListHeders.OrderDeliveryDate.ToString()].ToString()),
                   priceTableList);

                orderList.Add(order);
            }
            return orderList;
        }

    }//end class loadData
}
