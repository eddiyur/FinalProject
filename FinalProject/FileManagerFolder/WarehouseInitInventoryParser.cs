using OperationalTrainer.Data_Structures;
using OperationalTrainer.Logic.MainLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UtilitiesFileManager;

namespace FinalProject.FileManagerFolder
{
    public static class WarehouseInitInventoryParser
    {
        enum WarehouseInventoryFields
        {
            ProductBranch,
            ProductID,
            ProductInitInventory
        }


        public static Dictionary<ProductClass, double> Parse(XmlNodeList InventoryNodeList, InitDataLoad initDataLoad)
        {
            ProductClassList productsList = initDataLoad.MetaData.ProductsMetaData;
            Dictionary<ProductClass, double> Inventory = new Dictionary<ProductClass, double>();

            foreach (XmlNode inventoryParameter in InventoryNodeList)
            {
                WarehouseInventoryFields XMLWarehouseInventoryField = (WarehouseInventoryFields)Enum.Parse(typeof(WarehouseInventoryFields), inventoryParameter.Name, true);
                KeyValuePair<ProductClass, double> productInventory = new KeyValuePair<ProductClass, double>();
                switch (XMLWarehouseInventoryField)
                {
                    case WarehouseInventoryFields.ProductBranch:
                        productInventory = getProductInventory(inventoryParameter, productsList);
                        break;
                    default:
                        break;
                }
                Inventory.Add(productInventory.Key, productInventory.Value);
            }
            return Inventory;
        }

        private static KeyValuePair<ProductClass, double> getProductInventory(XmlNode inventoryParameter, ProductClassList productsList)
        {
            try
            {
                XmlNodeList inventoryBranchElements = inventoryParameter.ChildNodes;
                KeyValuePair<ProductClass, double> productInventory = new KeyValuePair<ProductClass, double>(
                productsList.GetProduct(inventoryBranchElements[0].InnerText),
                Convert.ToDouble(inventoryBranchElements[1].InnerText));
                return productInventory;
            }
            catch (Exception)
            { }
            return new KeyValuePair<ProductClass, double>();
        }

        public static XmlDocument WarehouseInitInventoryCSVToXML(XmlDocument doc, string SourcefileName)
        {

            FileManager fm = new FileManager();
            DataTable dt = fm.GetCSV(SourcefileName);

            var root = doc.GetElementsByTagName(LoadData.XMLMainCategories.dataset.ToString())[0];
            var initInventory = doc.CreateElement(LoadData.XMLMainCategories.WarehouseInitInventory.ToString());


            root.AppendChild(initInventory);

            foreach (DataRow row in dt.Rows)
            {
                var product = doc.CreateElement(WarehouseInventoryFields.ProductBranch.ToString());

                var ProductIDTagName = doc.CreateElement(WarehouseInventoryFields.ProductID.ToString());
                ProductIDTagName.InnerText = row[0].ToString();
                product.AppendChild(ProductIDTagName);

                var productInitInventory = doc.CreateElement(WarehouseInventoryFields.ProductInitInventory.ToString());
                productInitInventory.InnerText = row[1].ToString();
                product.AppendChild(productInitInventory);


                initInventory.AppendChild(product);
            }

            return doc;
        }



    }//end class

}
