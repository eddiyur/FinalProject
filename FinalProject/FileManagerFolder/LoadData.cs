﻿using FinalProject.FileManagerFolder;
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
    public class CSVScenarioFilePath
    {
        public string InitData;
        public string ProductsList;
        public string WarehouseInitInventory;
        public string SuppliersList;
        public string CustomersOrderList;
        public string FutureCustomersOrderList;
        public string SuppliersOrderList;
    }

    public class LoadData
    {

        public LoadData() { }

        public enum XMLMainCategories
        {
            dataset,
            InitData,
            ProductsList,
            WarehouseInitInventory,
            SuppliersList,
            CustomersOrderList,
            FutureCustomersOrderList,
            SuppliersOrderList
        }

        public InitOperationalTrainerDataSet LoadInitData(string filePath)
        {
            InitOperationalTrainerDataSet operationalTrainerData = new InitOperationalTrainerDataSet();
            XmlDocument xmldoc = getXmldoc(filePath);
            //    XmlDocument xmldoc = getXMlFile("TestScenario1.xml");

            //    XmlNodeList productsNodeList = getXmlNodeList("ProductList.xml", XMLMainCategories.ProductsList);
            //XmlNodeList suppliersNodeList = getXmlNodeList("SuppliersList.xml", XMLMainCategories.SuppliersList);
            //XmlNodeList customerOrderNodeList = getXmlNodeList("CustomerOrderList.xml", XMLMainCategories.CustomersOrderList);
            //XmlNodeList fucureCustomerOrderNodeList = getXmlNodeList("futureCustomersOrderList.xml", XMLMainCategories.FutureCustomersOrderList);
            //XmlNodeList supploersOrderNodeList = getXmlNodeList("SuppliersOrderList.xml", XMLMainCategories.SuppliersOrderList);

            XmlNodeList initNodeList = getXmlNodeList(xmldoc, XMLMainCategories.InitData);
            XmlNodeList productsNodeList = getXmlNodeList(xmldoc, XMLMainCategories.ProductsList);
            XmlNodeList suppliersNodeList = getXmlNodeList(xmldoc, XMLMainCategories.SuppliersList);
            XmlNodeList customerOrderNodeList = getXmlNodeList(xmldoc, XMLMainCategories.CustomersOrderList);
            XmlNodeList fucureCustomerOrderNodeList = getXmlNodeList(xmldoc, XMLMainCategories.FutureCustomersOrderList);
            XmlNodeList supploersOrderNodeList = getXmlNodeList(xmldoc, XMLMainCategories.SuppliersOrderList);
            XmlNodeList WarehouseInitInventoryNodeList = getXmlNodeList(xmldoc, XMLMainCategories.WarehouseInitInventory);

            operationalTrainerData.OperationalTrainerInitDataSet = InitDataParser.Parse(initNodeList);
            operationalTrainerData.OperationalTrainerDataSet.ProductsMetaDataList = ProductParser.Parse(productsNodeList);
            operationalTrainerData.OperationalTrainerDataSet.SuppliersList = SuppliersParser.Parse(suppliersNodeList, operationalTrainerData.OperationalTrainerDataSet.ProductsMetaDataList);

            operationalTrainerData.OperationalTrainerDataSet.CustomersOrderList = OrderParser.Parse(customerOrderNodeList, operationalTrainerData.OperationalTrainerDataSet.ProductsMetaDataList, Order.OrderTypeEnum.CustomerOrder);
            operationalTrainerData.OperationalTrainerDataSet.futureCustomersOrderList = OrderParser.Parse(fucureCustomerOrderNodeList, operationalTrainerData.OperationalTrainerDataSet.ProductsMetaDataList, Order.OrderTypeEnum.CustomerOrder);
            operationalTrainerData.OperationalTrainerDataSet.SupplieOrderList = OrderParser.Parse(supploersOrderNodeList, operationalTrainerData.OperationalTrainerDataSet.ProductsMetaDataList, Order.OrderTypeEnum.SupplierOrder, operationalTrainerData.OperationalTrainerDataSet.SuppliersList);
            operationalTrainerData.OperationalTrainerInitDataSet.WarehouseInitInventory = WarehouseInitInventoryParser.Parse(WarehouseInitInventoryNodeList, operationalTrainerData.OperationalTrainerDataSet.ProductsMetaDataList);
            return operationalTrainerData;
        }

        public void CreateXMLScenario(CSVScenarioFilePath cSVScenarioFilePath)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.CreateElement(LoadData.XMLMainCategories.dataset.ToString());
            doc.InsertBefore(xmlDeclaration, doc.DocumentElement);
            doc.AppendChild(root);

            doc = InitDataParser.InitDataCSVToXML(doc, cSVScenarioFilePath.InitData);
            doc = ProductParser.ProductClassCSVToXML(doc, cSVScenarioFilePath.ProductsList);


            string fileResultpath = @"C:\Users\eyurkovs\Desktop\final progect\FinalProject\FinalProject\FinalProject\dataSets\testresult.xml";

            using (var writer = new XmlTextWriter(fileResultpath, Encoding.UTF8) { Formatting = Formatting.Indented })
            {
                doc.WriteTo(writer);
            }
        }

        private XmlDocument getXmldoc(string filePath)
        {
            XmlDocument xmldoc = new XmlDocument();
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            return xmldoc;
        }


        //public void LoadLists()
        //{
        //    //XmlNodeList productsNodeList = getXmlNodeList("ProductList.xml", XMLMainCategories.ProductsList);
        //    //XmlNodeList suppliersNodeList = getXmlNodeList("SuppliersList.xml", XMLMainCategories.SuppliersList);
        //    //XmlNodeList CustomerOrderNodeList = getXmlNodeList("CustomerOrderList.xml", XMLMainCategories.CustomerOrderList);

        //    //productsList = ProductParser.Parse(productsNodeList);
        //    //suppliersList = SuppliersParser.Parse(suppliersNodeList, productsList);
        //    //customerOrderList = CustomerOrderParser.Parse(CustomerOrderNodeList, productsList);

        //    //////csv to xml test
        //    ////XmlNodeList edduNodeList = getXmlNodeList("eddi.xml", XMLMainCategories.ProductsList);
        //    ////productsList = ProductParser.Parse(edduNodeList);

        //}


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

        private XmlNodeList getXmlNodeList(XmlDocument xmldoc, XMLMainCategories XMLMainField)
        { return xmldoc.GetElementsByTagName(XMLMainField.ToString())[0].ChildNodes; }

        public XmlDocument getXMlFile(string fileName)
        {
            string folderPath = getTempFolderPath();

            string filePath = folderPath + fileName;
            XmlDocument xmldoc = new XmlDocument();
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            return xmldoc;
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
