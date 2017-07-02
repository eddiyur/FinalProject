using FinalProject.Data_Structures;
using FinalProject.FileManager;
using FinalProject.Logic.Prediction;
using FinalProject.Logic.Warehouse;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace FinalProject.Data_Structures
{
    public class LoadData
    {
        public ProductClassList productsList;
        public SuppliersList suppliersList;
        public OrdersList customerOrderList;
        public LoadData() { }

        enum XMLMainCategories
        {
            ProductsList,
            SuppliersList,
            CustomerOrderList,
        }





        public void LoadLists()
        {
            XmlNodeList productsNodeList = getXmlNodeList("ProductList.xml", XMLMainCategories.ProductsList);
            XmlNodeList suppliersNodeList = getXmlNodeList("SuppliersList.xml", XMLMainCategories.SuppliersList);
            XmlNodeList CustomerOrderNodeList = getXmlNodeList("CustomerOrderList.xml", XMLMainCategories.CustomerOrderList);

            productsList = ProductParser.Parse(productsNodeList);
            suppliersList = SuppliersParser.Parse(suppliersNodeList, productsList);
            customerOrderList = CustomerOrderParser.Parse(CustomerOrderNodeList, productsList);

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


        private string getTempFolderPath()
        {
            UtilitiesFileManager.FileManager fileManager = new UtilitiesFileManager.FileManager();
            string folderPath = fileManager.ExePath();
            string[] pathParts = folderPath.Split('\\');

            folderPath = pathParts[0];
            for (int i = 1; i < 8; i++)
            {
                folderPath = folderPath + "\\" + pathParts[i];
            }
            return folderPath + "\\dataSets\\";
        }











    }//end class loadData
}
