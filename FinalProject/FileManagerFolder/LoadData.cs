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

        public InitOperationalTrainerDataSet LoadInitData(string filePath)
        {
            InitOperationalTrainerDataSet operationalTrainerData = new InitOperationalTrainerDataSet();
            XmlDocument xmldoc = getXmldoc(filePath);

            XmlNodeList initNodeList = getXmlNodeList(xmldoc, XMLMainCategories.InitData);
            XmlNodeList productsNodeList = getXmlNodeList(xmldoc, XMLMainCategories.ProductsList);
            XmlNodeList suppliersNodeList = getXmlNodeList(xmldoc, XMLMainCategories.SuppliersList);
            XmlNodeList customerOrderNodeList = getXmlNodeList(xmldoc, XMLMainCategories.CustomersOrderList);
            XmlNodeList fucureCustomerOrderNodeList = getXmlNodeList(xmldoc, XMLMainCategories.FutureCustomersOrderList);
            XmlNodeList supploersOrderNodeList = getXmlNodeList(xmldoc, XMLMainCategories.SuppliersOrderList);
            XmlNodeList WarehouseInitInventoryNodeList = getXmlNodeList(xmldoc, XMLMainCategories.WarehouseInitInventory);
            XmlNodeList tooltypeyNodeList = getXmlNodeList(xmldoc, XMLMainCategories.ToolTypeList);


            operationalTrainerData.OperationalTrainerInitDataSet = InitDataParser.Parse(initNodeList);
            ToolTypeClassList toolTypelist = ToolTypeParser.parse(tooltypeyNodeList);
            operationalTrainerData.OperationalTrainerDataSet.ProductsMetaDataList = ProductParser.Parse(productsNodeList, toolTypelist);
            operationalTrainerData.OperationalTrainerDataSet.SuppliersList = SuppliersParser.Parse(suppliersNodeList, operationalTrainerData.OperationalTrainerDataSet.ProductsMetaDataList);

            operationalTrainerData.OperationalTrainerDataSet.CustomersOrderList = OrderParser.Parse(customerOrderNodeList, operationalTrainerData.OperationalTrainerDataSet.ProductsMetaDataList, Order.OrderTypeEnum.CustomerOrder);
            operationalTrainerData.OperationalTrainerDataSet.futureCustomersOrderList = OrderParser.Parse(fucureCustomerOrderNodeList, operationalTrainerData.OperationalTrainerDataSet.ProductsMetaDataList, Order.OrderTypeEnum.CustomerOrder);
            operationalTrainerData.OperationalTrainerDataSet.SupplieOrderList = OrderParser.Parse(supploersOrderNodeList, operationalTrainerData.OperationalTrainerDataSet.ProductsMetaDataList, Order.OrderTypeEnum.SupplierOrder, operationalTrainerData.OperationalTrainerDataSet.SuppliersList);
            operationalTrainerData.OperationalTrainerInitDataSet.WarehouseInitInventory = WarehouseInitInventoryParser.Parse(WarehouseInitInventoryNodeList, operationalTrainerData.OperationalTrainerDataSet.ProductsMetaDataList);


            /////test




            ////



            return operationalTrainerData;
        }

        public ToolsList loadTool(ToolTypeClassList toolTypeClassList, ProductClassList productslist)
        {
            FileManager fileManger = new FileManager();
            string filePath = @"C:\Users\eyurkovs\Desktop\final progect\FinalProject\FinalProject\FinalProject\dataSets\Scenario1\ToolList.csv";
            DataTable toolTable = fileManger.GetCSV(filePath);
            
            ToolsList toollist = new ToolsList();

            foreach (DataRow row in toolTable.Rows)
            {
                Tool tool = new Tool();
                tool.ToolID = row[0].ToString();
                tool.ToolName = row[1].ToString();
                tool.ToolType = toolTypeClassList.GetToolType(row[2].ToString());
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
