using FinalProject.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FinalProject.FileManager
{
    public class ProductParser
    {
        enum XMLProductFields
        {
            ProductID,
            ProductName,
            ProductCapacity,
            ProductTree,
            ProductTree_ProductID,
            ProductTree_Amount
        }

        public static ProductClassList Parse(XmlNodeList productsNodeList)
        {
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
                            product.ProductTree = getProductTree(productparameter);
                            break;
                        default:
                            break;
                    }
                }
                productClassList.AddProduct(product);
            }
            return productClassList;
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


    }//end class
}
