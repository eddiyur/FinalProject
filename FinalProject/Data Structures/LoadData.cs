using FinalProject.Data_Structures;
using FinalProject.Logic.Prediction;
using FinalProject.Logic.Warehouse;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FinalProject.Data_Structures
{
    public class LoadData
    {
        public object WarehouseCalss { get; private set; }

        public LoadData() { }


        enum XMLProductFields
        {
            ProductsList,
            ProductID,
            PruductName,
            ProductCapacity,
            ProductTree,
            ProductTree_ProdactID,
            ProductTree_Amount

        }

        public void LoadLists()
        {
            ProductClassList productClassList = LoadProductClassList();
        }

        public ProductClassList LoadProductClassList()
        {
            ProductClassList productClassList = new ProductClassList();

            UtilitiesFileManager.FileManager fileManager = new UtilitiesFileManager.FileManager();
            string folderPath = fileManager.ExePath() + "dataSets\\";

            string fileName = folderPath + "ProductList.xml";
            XmlDocument xmldoc = new XmlDocument();
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);

            XmlNodeList productsNodeList = xmldoc.GetElementsByTagName(XMLProductFields.ProductsList.ToString())[0].ChildNodes;

            foreach (XmlNode productNode in productsNodeList)
            {
                ProductClass product = new ProductClass();

                foreach (XmlNode productparameters in productNode)
                {
                    XMLProductFields XMLProductField = (XMLProductFields)Enum.Parse(typeof(XMLProductFields), productparameters.Name, true);

                    switch (XMLProductField)
                    {
                        case XMLProductFields.ProductID:
                            product.ProductID = productparameters.InnerText;
                            break;
                        case XMLProductFields.PruductName:
                            product.ProductName = productparameters.InnerText;
                            break;
                        case XMLProductFields.ProductCapacity:
                            product.ProductCapacity = int.Parse(productparameters.InnerText);
                            break;
                        case XMLProductFields.ProductTree:
                            product.ProductTree = getProductTree(productparameters);
                            break;
                        default:
                            break;
                    }//end  switch (XMLProductField)

                }//end oreach (XmlNode productparameters in productNode)
                productClassList.AddProduct(product);
            }//end     foreach (XmlNode productNode in productsNodeList)

            return productClassList;


        }//end void loadTest()

        private Dictionary<string, int> getProductTree(XmlNode productTreeNode)
        {
            Dictionary<string, int> ProductTree = new Dictionary<string, int>();
            try
            {
                XmlNodeList ProductTreeBranchList = productTreeNode.ChildNodes;

                foreach (XmlNode productTreeBranch in ProductTreeBranchList)
                {
                    XmlNodeList productTreeBranchElements = productTreeBranch.ChildNodes;

                    string bb = productTreeBranchElements[0].InnerText;
                    ProductTree.Add(productTreeBranchElements[0].InnerText, int.Parse(productTreeBranchElements[1].InnerText));
                }
            }
            catch (Exception)
            {
                ProductTree = null; ;
            }

            return ProductTree;
        }

        public void loadProducts()
        {
            UtilitiesFileManager.FileManager fileManager = new UtilitiesFileManager.FileManager();
            string folderPath = fileManager.ExePath() + "dataSets\\";

            ProductClassList productList = new ProductClassList();
            productList.ProductList = getProductList(folderPath);

            SuppliersList suppliersList = new SuppliersList();
            suppliersList = getSuppliersList(folderPath, productList);


            List<Customer> customerList = getCustomerList(folderPath);
            OrdersList orderList = getOrderList(folderPath);

            Prediction predictionManager = new Prediction();
            predictionManager.PredictionManager(orderList);

            ///
            Order order = orderList.GetOrder("order4");
            order.OrderStatus = Order.OrderStatusEnum.Canceled;
            List<Order> or = orderList.GetOrders(Order.OrderStatusEnum.Canceled);

            ////
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
                   Int32.Parse(row[SuppliersPriceMatrixFileColumnsName.amount.ToString()].ToString()),
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

        private OrdersList getOrderList(string folderPath)
        {

            PersonClass person = new PersonClass("CostumerTest", "ID1", PersonTypeEnum.Customer);

            UtilitiesFileManager.FileManager fileManager = new UtilitiesFileManager.FileManager();
            DataTable orderTable = fileManager.GetCSV(folderPath + "OrderList.csv");
            // List<Order> orderList = new List<Order>();
            OrdersList ordersList = new OrdersList();

            foreach (DataRow row in orderTable.Rows)
            {
                ProductClass product = new ProductClass(row[OrderListHeders.Product.ToString()].ToString(), row[OrderListHeders.Product.ToString()].ToString(), 1);
                List<PriceTable> priceTableList = new List<PriceTable>();
                PriceTable priceTable = new PriceTable(product,
                    int.Parse(row[OrderListHeders.Amount.ToString()].ToString()),
                    double.Parse(row[OrderListHeders.Cost.ToString()].ToString()));

                priceTableList.Add(priceTable);

                Order order = new Order(person,
                    Order.OrderTypeEnum.CustomerOrder,
                    row[OrderListHeders.OrderID.ToString()].ToString(),
                   DateTime.Parse(row[OrderListHeders.OrderDate.ToString()].ToString()),
                   DateTime.Parse(row[OrderListHeders.OrderDeliveryDate.ToString()].ToString()),
                   priceTableList);

                ordersList.AddOrder(order);
            }
            return ordersList;
        }

    }//end class loadData
}
