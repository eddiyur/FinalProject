using FinalProject.Data_Structures;
using FinalProject.FileManagerFolder;
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
using UtilitiesFileManager;
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
        public string ToolTypeList;
        public string ToolList;
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
            SuppliersOrderList,
            ToolTypeList
        }

        public InitDataLoad LoadInitData(string filePath)
        {
            InitDataLoad initDataLoad = new InitDataLoad();
            XmlDocument xmldoc = getXmldoc(filePath);

            XmlNodeList initNodeList = getXmlNodeList(xmldoc, XMLMainCategories.InitData);
            XmlNodeList productsNodeList = getXmlNodeList(xmldoc, XMLMainCategories.ProductsList);
            XmlNodeList suppliersNodeList = getXmlNodeList(xmldoc, XMLMainCategories.SuppliersList);
            XmlNodeList customerOrderNodeList = getXmlNodeList(xmldoc, XMLMainCategories.CustomersOrderList);
            XmlNodeList fucureCustomerOrderNodeList = getXmlNodeList(xmldoc, XMLMainCategories.FutureCustomersOrderList);
            XmlNodeList supploersOrderNodeList = getXmlNodeList(xmldoc, XMLMainCategories.SuppliersOrderList);
            XmlNodeList WarehouseInitInventoryNodeList = getXmlNodeList(xmldoc, XMLMainCategories.WarehouseInitInventory);
            XmlNodeList tooltypeyNodeList = getXmlNodeList(xmldoc, XMLMainCategories.ToolTypeList);


            initDataLoad.InitParameters = InitDataParser.Parse(initNodeList);
            initDataLoad.MetaData.ToolTypeMetaData = ToolTypeParser.parse(tooltypeyNodeList);
            initDataLoad.MetaData.ProductsMetaData = ProductParser.Parse(productsNodeList, initDataLoad);
            initDataLoad.MetaData.SuppliersMetaData = SuppliersParser.Parse(suppliersNodeList, initDataLoad);

            initDataLoad.InitLists.InitCustomersOrderList = OrderParser.Parse(customerOrderNodeList, initDataLoad, Order.OrderTypeEnum.CustomerOrder);
            initDataLoad.InitLists.InitFutureCustomersOrderList = OrderParser.Parse(fucureCustomerOrderNodeList, initDataLoad, Order.OrderTypeEnum.CustomerOrder);
            initDataLoad.InitLists.InitPurchaseOrders = OrderParser.Parse(supploersOrderNodeList, initDataLoad, Order.OrderTypeEnum.SupplierOrder);
            initDataLoad.InitParameters.InitWarehouseInventory = WarehouseInitInventoryParser.Parse(WarehouseInitInventoryNodeList, initDataLoad);


            ///rebuild parser
            initDataLoad.InitLists.InitToolsList = loadTool(initDataLoad);
            initDataLoad.InitLists.InitProductionOrderList = generateProductionOrderList(initDataLoad);
            ////




            return initDataLoad;
        }

        private ProductionOrderList generateProductionOrderList(InitDataLoad initDataLoad)
        {
            ProductionOrderList productionOrderList = new ProductionOrderList();

            for (int i = 0; i < 5; i++)
            {
                ProductionOrder productionOrder = generateProductionOrder(initDataLoad, "orderID" + i.ToString());
                productionOrderList.AddOrder(productionOrder);
            }


            return productionOrderList;
        }

        private ProductionOrder generateProductionOrder(InitDataLoad initDataLoad, string orderid)
        {
            ProductionOrder productionOrder = new ProductionOrder(orderid,
                initDataLoad.MetaData.ProductsMetaData.GetProduct("Product_01"),
                new DateTime(2017, 01, 01),
               new DateTime(2017, 01, 01));

            return productionOrder;
        }

        public ToolList loadTool(InitDataLoad initDataLoad)
        {
            ToolTypeClassList toolTypeClassList = initDataLoad.MetaData.ToolTypeMetaData;
            ProductClassList productslist = initDataLoad.MetaData.ProductsMetaData;

            FileManager fileManger = new FileManager();
            string filePath;
            filePath = @"C:\Users\eyurkovs\Desktop\final progect\FinalProject\FinalProject\FinalProject\dataSets\Scenario1\ToolList.csv";
            DataTable toolTable;
            try
            {
                toolTable = fileManger.GetCSV(filePath);
            }
            catch (Exception)
            {
                MessageBox.Show("Choose toollist file path");
                filePath = fileManger.openFilePathCSV();
                toolTable = fileManger.GetCSV(filePath);
            }
            ToolList toollist = new ToolList();

            foreach (DataRow row in toolTable.Rows)
            {
                Tool tool = new Tool();
                tool.ToolID = row[0].ToString();
                tool.ToolName = row[1].ToString();
                tool.ToolType = toolTypeClassList.GetToolType(row[2].ToString());
                tool.ShiftStartTime = Convert.ToInt16(row[3].ToString());
                tool.ShiftStartTime = Convert.ToInt16(row[4].ToString());
                ProductClass product = productslist.GetProduct(row[5].ToString());
                int productionTime = Convert.ToInt16(row[6].ToString());
                tool.ProductsProcessingTime.Add(product, productionTime);
                toollist.AddTool(tool);
            }

            return toollist;
        }


        public void CreateXMLScenario(CSVScenarioFilePath cSVScenarioFilePath)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.CreateElement(LoadData.XMLMainCategories.dataset.ToString());
            doc.InsertBefore(xmlDeclaration, doc.DocumentElement);
            doc.AppendChild(root);

            doc = InitDataParser.InitDataCSVToXML(doc, cSVScenarioFilePath.InitData);
            doc = ToolTypeParser.ToolTypeCSVToXML(doc, cSVScenarioFilePath.ToolTypeList);
            doc = ProductParser.ProductClassCSVToXML(doc, cSVScenarioFilePath.ProductsList);
            doc = WarehouseInitInventoryParser.WarehouseInitInventoryCSVToXML(doc, cSVScenarioFilePath.WarehouseInitInventory);
            doc = SuppliersParser.SuppliersCSVToXML(doc, cSVScenarioFilePath.SuppliersList);
            doc = OrderParser.OrderCSVToXML(doc, cSVScenarioFilePath.CustomersOrderList, XMLMainCategories.CustomersOrderList);
            doc = OrderParser.OrderCSVToXML(doc, cSVScenarioFilePath.FutureCustomersOrderList, XMLMainCategories.FutureCustomersOrderList);
            doc = OrderParser.OrderCSVToXML(doc, cSVScenarioFilePath.SuppliersOrderList, XMLMainCategories.SuppliersOrderList);



            FileManager fileManager = new FileManager();
            string fileResultpath = fileManager.saveFilePathXML();

            if (!string.IsNullOrEmpty(fileResultpath))
            {
                using (var writer = new XmlTextWriter(fileResultpath, Encoding.UTF8) { Formatting = Formatting.Indented })
                {
                    doc.WriteTo(writer);
                }
            }

        }

        private XmlDocument getXmldoc(string filePath)
        {
            XmlDocument xmldoc = new XmlDocument();
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            return xmldoc;
        }



        private XmlNodeList getXmlNodeList(XmlDocument xmldoc, XMLMainCategories XMLMainField)
        { return xmldoc.GetElementsByTagName(XMLMainField.ToString())[0].ChildNodes; }

        public static string transfareDate(string dateString)
        {
            //string date;

            //string year = dateString.Substring(0, 4);
            //string month = dateString.Substring(5, 2);
            //string day = dateString.Substring(8, 2);

            //date = @"" + month + "/" + day + "/" + year;
            DateTime result;

            if (!DateTime.TryParse(dateString, out result))
            {
                MessageBox.Show(dateString + "wrong date", "error");
            }
            return dateString;
        }


        public XmlDocument getXMlFile(string fileName)
        {
            string folderPath = getTempFolderPath();

            string filePath = folderPath + fileName;
            XmlDocument xmldoc = new XmlDocument();
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            return xmldoc;
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
