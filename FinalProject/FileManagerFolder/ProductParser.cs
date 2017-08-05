using FinalProject.Data_Structures;
using OperationalTrainer.Data_Structures;
using OperationalTrainer.Logic.MainLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using UtilitiesFileManager;

namespace OperationalTrainer.FileManagerFolder
{
    public class ProductParser
    {
        enum XMLProductFields
        {
            Product,
            ProductID,
            ProductName,
            ProductCapacity,
            ProductTree,
            ProductTreeBranch,
            ProductTree_ProductID,
            ProductTree_Amount,
            ToolList,
            ToolBranchID
        }

        public static ProductClassList Parse(XmlNodeList productsNodeList, InitDataLoad initDataLoad)
        {
            ToolTypeClassList toolTypeClassList = initDataLoad.DataStructure.ToolTypeMetaDataList;
            ProductClassList productClassList = new ProductClassList();
            foreach (XmlNode productNode in productsNodeList)
            {
                ProductClass product = new ProductClass();

                foreach (XmlNode productparameter in productNode)
                {
                    XMLProductFields XMLProductField = (XMLProductFields)Enum.Parse(typeof(XMLProductFields), productparameter.Name, true);

                    switch (XMLProductField)
                    {
                        case XMLProductFields.ProductID:
                            product.ProductID = productparameter.InnerText;
                            break;
                        case XMLProductFields.ProductName:
                            product.ProductName = productparameter.InnerText;
                            break;
                        case XMLProductFields.ProductCapacity:
                            product.ProductCapacity = int.Parse(productparameter.InnerText);
                            break;
                        case XMLProductFields.ProductTree:
                            product.InitProductTree = getProductTree(productparameter);
                            break;
                        case XMLProductFields.ToolList:
                            product.ToolsTypeList = getToolTypeList(productparameter, toolTypeClassList);
                            break;
                        default:
                            break;
                    }
                }
                productClassList.AddProduct(product);
            }

            productClassList = GenerateProductTree(productClassList);

            return productClassList;
        }

        private static ToolTypeClassList getToolTypeList(XmlNode productparameter, ToolTypeClassList toolTypeClassList)
        {
            ToolTypeClassList toolTypeList = new ToolTypeClassList();
            XmlNodeList toolTypes = productparameter.ChildNodes;
            foreach (XmlNode tooType in toolTypes)
            {
                string toolTypeID = tooType.InnerText;
                ToolTypeClass toolType = toolTypeClassList.GetToolType(tooType.InnerText);
                toolTypeList.AddToolType(toolType);
            }
            return toolTypeList;
        }

        private static ProductClassList GenerateProductTree(ProductClassList productClassList)
        {
            ProductClassList productList = new ProductClassList();
            foreach (ProductClass product in productClassList.ProductList)
            {
                Dictionary<string, int> initProductTree = product.InitProductTree;
                Dictionary<ProductClass, int> productTree = new Dictionary<ProductClass, int>();

                foreach (KeyValuePair<string, int> initTreeBranch in initProductTree)
                {
                    ProductClass productSon = productClassList.GetProduct(initTreeBranch.Key);
                    if (productSon == null)
                    {
                        MessageBox.Show("Error: Product tree Incorrect", "Error");
                        return null;
                    }
                    else
                        productTree.Add(productSon, initTreeBranch.Value);
                }
                product.ProductTree = productTree;
                productList.AddProduct(product);
            }
            return productList;
        }

        private static Dictionary<string, int> getProductTree(XmlNode productTreeNode)
        {
            Dictionary<string, int> ProductTree = new Dictionary<string, int>();
            try
            {
                XmlNodeList ProductTreeBranchList = productTreeNode.ChildNodes;

                foreach (XmlNode productTreeBranch in ProductTreeBranchList)
                {
                    XmlNodeList productTreeBranchElements = productTreeBranch.ChildNodes;
                    ProductTree.Add(productTreeBranchElements[0].InnerText, int.Parse(productTreeBranchElements[1].InnerText));
                }
            }
            catch (Exception)
            {
                ProductTree = null; ;
            }
            return ProductTree;
        }



        public static XmlDocument ProductClassCSVToXML(XmlDocument doc, string SourcefileName)
        {
            FileManager fm = new FileManager();
            DataTable dt = fm.GetCSV(SourcefileName);

            var root = doc.GetElementsByTagName(LoadData.XMLMainCategories.dataset.ToString())[0];
            var productsList = doc.CreateElement(LoadData.XMLMainCategories.ProductsList.ToString());

            root.AppendChild(productsList);

            foreach (DataRow row in dt.Rows)
            {
                var product = doc.CreateElement(XMLProductFields.Product.ToString());

                var ProductIDTagName = doc.CreateElement(XMLProductFields.ProductID.ToString());
                ProductIDTagName.InnerText = row[0].ToString();
                product.AppendChild(ProductIDTagName);

                var ProductNameTagName = doc.CreateElement(XMLProductFields.ProductName.ToString());
                ProductNameTagName.InnerText = row[1].ToString();
                product.AppendChild(ProductNameTagName);

                var ProductCapacityTagName = doc.CreateElement(XMLProductFields.ProductCapacity.ToString());
                ProductCapacityTagName.InnerText = row[2].ToString();
                product.AppendChild(ProductCapacityTagName);


                //ProductTree
                var ProductTreeTagName = doc.CreateElement(XMLProductFields.ProductTree.ToString());

                int columnIndex = 3;
                for (int i = 0; i < 3; i++)
                {
                    string ProductTree_ProductIDValue = row[columnIndex].ToString();
                    string ProductTree_AmountValue = row[columnIndex + 1].ToString();

                    if (!string.IsNullOrEmpty(ProductTree_ProductIDValue))
                    {
                        var ProductTreeBranchTagName = doc.CreateElement(XMLProductFields.ProductTreeBranch.ToString());

                        var ProductTree_ProductIDTagName = doc.CreateElement(XMLProductFields.ProductTree_ProductID.ToString());
                        ProductTree_ProductIDTagName.InnerText = ProductTree_ProductIDValue;
                        ProductTreeBranchTagName.AppendChild(ProductTree_ProductIDTagName);

                        var ProductTree_AmountTagName = doc.CreateElement(XMLProductFields.ProductTree_Amount.ToString());
                        ProductTree_AmountTagName.InnerText = ProductTree_AmountValue;
                        ProductTreeBranchTagName.AppendChild(ProductTree_AmountTagName);

                        ProductTreeTagName.AppendChild(ProductTreeBranchTagName);
                    }
                    columnIndex = columnIndex + 2;
                }
                product.AppendChild(ProductTreeTagName);

                //ToolList
                var ToolListTagName = doc.CreateElement(XMLProductFields.ToolList.ToString());

                columnIndex = 9;
                for (int i = 0; i < 3; i++)
                {
                    string ToolID = row[columnIndex + i].ToString();

                    if (!string.IsNullOrEmpty(ToolID))
                    {
                        var ToolBranchIDTagName = doc.CreateElement(XMLProductFields.ToolBranchID.ToString());
                        ToolBranchIDTagName.InnerText = ToolID;
                        ToolListTagName.AppendChild(ToolBranchIDTagName);

                    }
                }
                product.AppendChild(ToolListTagName);
                productsList.AppendChild(product);
            }
            return doc;
        }
        //public static void ProductClassCSVToXML(string SourcefileName, string targetFileName)
        //{
        //    string FolderPath = LoadData.getTempFolderPath();
        //    string filePath = FolderPath + SourcefileName;
        //    FileManager fm = new FileManager();
        //    DataTable dt = fm.GetCSV(filePath);

        //    XmlDocument doc = new XmlDocument();
        //    XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
        //    XmlElement root = doc.CreateElement(LoadData.XMLMainCategories.dataset.ToString());
        //    doc.InsertBefore(xmlDeclaration, doc.DocumentElement);
        //    doc.AppendChild(root);

        //    var productsList = doc.CreateElement(LoadData.XMLMainCategories.ProductsList.ToString());
        //    root.AppendChild(productsList);

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        var product = doc.CreateElement(XMLProductFields.Product.ToString());

        //        var ProductIDTagName = doc.CreateElement(XMLProductFields.ProductID.ToString());
        //        ProductIDTagName.InnerText = row[0].ToString();
        //        product.AppendChild(ProductIDTagName);

        //        var ProductNameTagName = doc.CreateElement(XMLProductFields.ProductName.ToString());
        //        ProductNameTagName.InnerText = row[1].ToString();
        //        product.AppendChild(ProductNameTagName);

        //        var ProductCapacityTagName = doc.CreateElement(XMLProductFields.ProductCapacity.ToString());
        //        ProductCapacityTagName.InnerText = row[2].ToString();
        //        product.AppendChild(ProductCapacityTagName);

        //        var ProductTreeTagName = doc.CreateElement(XMLProductFields.ProductTree.ToString());

        //        int columnIndex = 3;
        //        for (int i = 0; i < 3; i++)
        //        {
        //            string ProductTree_ProductIDValue = row[columnIndex].ToString();
        //            string ProductTree_AmountValue = row[columnIndex + 1].ToString();

        //            if (!string.IsNullOrEmpty(ProductTree_ProductIDValue))
        //            {
        //                var ProductTreeBranchTagName = doc.CreateElement(XMLProductFields.ProductTreeBranch.ToString());
        //                var ProductTree_ProductIDTagName = doc.CreateElement(XMLProductFields.ProductTree_ProductID.ToString());
        //                ProductTree_ProductIDTagName.InnerText = ProductTree_ProductIDValue;
        //                ProductTreeBranchTagName.AppendChild(ProductTree_ProductIDTagName);
        //                var ProductTree_AmountTagName = doc.CreateElement(XMLProductFields.ProductTree_Amount.ToString());
        //                ProductTree_AmountTagName.InnerText = ProductTree_AmountValue;
        //                ProductTreeBranchTagName.AppendChild(ProductTree_AmountTagName);

        //                ProductTreeTagName.AppendChild(ProductTreeBranchTagName);
        //            }

        //            columnIndex = columnIndex + 2;

        //        }
        //        product.AppendChild(ProductTreeTagName);

        //        productsList.AppendChild(product);
        //    }

        //    using (var writer = new XmlTextWriter(FolderPath + targetFileName, Encoding.UTF8) { Formatting = Formatting.Indented })
        //    {
        //        doc.WriteTo(writer);
        //    }

        //}
    }//end class
}
