using OperationalTrainer.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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


        public static Dictionary<ProductClass, double> Parse(XmlNodeList InventoryNodeList, ProductClassList productsList)
        {
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
    }
}
