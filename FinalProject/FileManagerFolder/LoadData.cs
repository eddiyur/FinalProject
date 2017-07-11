using OperationalTrainer.Data_Structures;
using OperationalTrainer.FileManagerFolder;
using OperationalTrainer.Logic.MainLogic;
using OperationalTrainer.Logic.Prediction;
using OperationalTrainer.Logic.Warehouse;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static OperationalTrainer.Logic.MainLogic.DataManager;
using static OperationalTrainer.Logic.MainLogic.MainManager;

namespace OperationalTrainer.Data_Structures
{
    public class LoadData
    {
        //private ProductClassList productsList;
        //private SuppliersList suppliersList;
        //private OrdersList customerOrderList;
        public LoadData() { }

        public enum XMLMainCategories
        {
            dataset,
            ProductsList,
            SuppliersList,
            CustomerOrderList,
        }

        public OperationalTrainerDataSet LoadInitData()
        {
            OperationalTrainerDataSet operationalTrainerData = new OperationalTrainerDataSet();

            //init data
            operationalTrainerData.startDate = new DateTime(2017, 01, 31);
            operationalTrainerData.WarehouseMaxCapacity = 100;
            operationalTrainerData.BankCurrentBalance = 100;
            //load from file

            XmlNodeList productsNodeList = getXmlNodeList("ProductList.xml", XMLMainCategories.ProductsList);
            XmlNodeList suppliersNodeList = getXmlNodeList("SuppliersList.xml", XMLMainCategories.SuppliersList);
            XmlNodeList customerOrderNodeList = getXmlNodeList("CustomerOrderList.xml", XMLMainCategories.CustomerOrderList);
            XmlNodeList fucureCustomerOrderNodeList = getXmlNodeList("futureCustomersOrderList.xml", XMLMainCategories.CustomerOrderList);


            operationalTrainerData.ProductsMetaDataList = ProductParser.Parse(productsNodeList);
            operationalTrainerData.SuppliersList = SuppliersParser.Parse(suppliersNodeList, operationalTrainerData.ProductsMetaDataList);
            operationalTrainerData.CustomersOrderList= CustomerOrderParser.Parse(customerOrderNodeList, operationalTrainerData.ProductsMetaDataList);
            operationalTrainerData.futureCustomersOrderList = CustomerOrderParser.Parse(fucureCustomerOrderNodeList, operationalTrainerData.ProductsMetaDataList);
            return operationalTrainerData;


        }


        public void LoadLists()
        {
            //XmlNodeList productsNodeList = getXmlNodeList("ProductList.xml", XMLMainCategories.ProductsList);
            //XmlNodeList suppliersNodeList = getXmlNodeList("SuppliersList.xml", XMLMainCategories.SuppliersList);
            //XmlNodeList CustomerOrderNodeList = getXmlNodeList("CustomerOrderList.xml", XMLMainCategories.CustomerOrderList);

            //productsList = ProductParser.Parse(productsNodeList);
            //suppliersList = SuppliersParser.Parse(suppliersNodeList, productsList);
            //customerOrderList = CustomerOrderParser.Parse(CustomerOrderNodeList, productsList);

            //////csv to xml test
            ////XmlNodeList edduNodeList = getXmlNodeList("eddi.xml", XMLMainCategories.ProductsList);
            ////productsList = ProductParser.Parse(edduNodeList);

        }


        private XmlNodeList getXmlNodeList(string fileName, XMLMainCategories XMLMainField)
        {
            string folderPath = getTempFolderPath();

            string filePath = folderPath + fileName;
            XmlDocument xmldoc = new XmlDocument();
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);

            XmlNodeList xmlNodeList = xmldoc.GetElementsByTagName(XMLMainField.ToString())[0].ChildNodes;
            return xmlNodeList;
        }


        public static string getTempFolderPath()
        {
            UtilitiesFileManager.FileManager fileManager = new UtilitiesFileManager.FileManager();
            string folderPath = fileManager.ExePath();
            string[] pathParts = folderPath.Split('\\');

            folderPath = pathParts[0];
            for (int i = 1; i < pathParts.Count() - 3; i++)
            {
                folderPath = folderPath + "\\" + pathParts[i];
            }
            return folderPath + "\\dataSets\\";
        }











    }//end class loadData
}
