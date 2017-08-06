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
    public static class SuppliersParser
    {
        enum XMLSuppliersListFields
        {
            Supplier,
            SupplierID,
            SupplierName,
            SupplierReliability,
            SupplierPriceMatrix,
            SupplierPriceMatrixBranch,
            SupplierPriceMatrixBranch_ProductID,
            SupplierPriceMatrixBranch_UnitPrice,
            SupplierPriceMatrixBranch_LeadTime
        }

        public static SuppliersList Parse(XmlNodeList suppliersNodeList, InitDataLoad initdataLoad)
        {
            ProductClassList productsList = initdataLoad.MetaData.ProductsMetaData;
            SuppliersList suppliersList = new SuppliersList();

            foreach (XmlNode supplierNode in suppliersNodeList)
            {
                Supplier supplier = new Supplier();

                foreach (XmlNode supplierParameter in supplierNode)
                {
                    XMLSuppliersListFields XMLSuppliersListField = (XMLSuppliersListFields)Enum.Parse(typeof(XMLSuppliersListFields), supplierParameter.Name, true);

                    switch (XMLSuppliersListField)
                    {
                        case XMLSuppliersListFields.SupplierID:
                            supplier.ID = supplierParameter.InnerText;
                            break;
                        case XMLSuppliersListFields.SupplierName:
                            supplier.Name = supplierParameter.InnerText;
                            break;
                        case XMLSuppliersListFields.SupplierReliability:
                            supplier.Reliability = double.Parse(supplierParameter.InnerText);
                            break;
                        case XMLSuppliersListFields.SupplierPriceMatrix:
                            supplier.PriceMatrix = getSupplierPriceMatrix(supplierParameter, productsList);
                            break;
                        default:
                            break;
                    }
                }
                suppliersList.AddSupplier(supplier);
            }
            return suppliersList;
        }

        private static List<Supplier.PriceMatrixStruct> getSupplierPriceMatrix(XmlNode priceMatrixNode, ProductClassList productsList)
        {
            List<Supplier.PriceMatrixStruct> priceMatrix = new List<Supplier.PriceMatrixStruct>();
            try
            {
                foreach (XmlNode supplierPriceMatrixBranch in priceMatrixNode)
                {
                    Supplier.PriceMatrixStruct priceMatrixRow = new Supplier.PriceMatrixStruct();

                    XmlNodeList priceMatrixRowParametersList = supplierPriceMatrixBranch.ChildNodes;

                    foreach (XmlNode priceMatrixRowParameter in priceMatrixRowParametersList)
                    {
                        XMLSuppliersListFields XMLSuppliersListField = (XMLSuppliersListFields)Enum.Parse(typeof(XMLSuppliersListFields), priceMatrixRowParameter.Name, true);

                        switch (XMLSuppliersListField)
                        {
                            case XMLSuppliersListFields.SupplierPriceMatrixBranch_ProductID:
                                {
                                    ProductClass product = productsList.GetProduct(priceMatrixRowParameter.InnerText);
                                    if (product == null)
                                        MessageBox.Show("Wrong Product in Supplier Matrix", "Error");
                                    priceMatrixRow.product = product;
                                    break;
                                }
                            case XMLSuppliersListFields.SupplierPriceMatrixBranch_UnitPrice:
                                priceMatrixRow.UnitPrice = double.Parse(priceMatrixRowParameter.InnerText);
                                break;
                            case XMLSuppliersListFields.SupplierPriceMatrixBranch_LeadTime:
                                priceMatrixRow.LeadTime = int.Parse(priceMatrixRowParameter.InnerText);
                                break;
                            default:
                                break;
                        }
                    }
                    priceMatrix.Add(priceMatrixRow);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return priceMatrix;
        }

        public static XmlDocument SuppliersCSVToXML(XmlDocument doc, string SourcefileName)
        {
            FileManager fm = new FileManager();
            DataTable dt = fm.GetCSV(SourcefileName);

            var root = doc.GetElementsByTagName(LoadData.XMLMainCategories.dataset.ToString())[0];
            var suppliersList = doc.CreateElement(LoadData.XMLMainCategories.SuppliersList.ToString());

            root.AppendChild(suppliersList);

            foreach (DataRow row in dt.Rows)
            {
                var supplier = doc.CreateElement(XMLSuppliersListFields.Supplier.ToString());

                var supplierIDTagName = doc.CreateElement(XMLSuppliersListFields.SupplierID.ToString());
                supplierIDTagName.InnerText = row[0].ToString();
                supplier.AppendChild(supplierIDTagName);

                var supplierNameTagName = doc.CreateElement(XMLSuppliersListFields.SupplierName.ToString());
                supplierNameTagName.InnerText = row[1].ToString();
                supplier.AppendChild(supplierNameTagName);

                var SupplierReliabilityTagName = doc.CreateElement(XMLSuppliersListFields.SupplierReliability.ToString());
                SupplierReliabilityTagName.InnerText = row[2].ToString();
                supplier.AppendChild(SupplierReliabilityTagName);

                var SupplierPriceMatrix = doc.CreateElement(XMLSuppliersListFields.SupplierPriceMatrix.ToString());

                int columnIndex = 3;
                int maxSons = 3;

                for (int i = 0; i < maxSons; i++)
                {
                    string SupplierPriceMatrixBranch_ProductIDValue = row[columnIndex].ToString();
                    string ProductTree_UnitPriceValue = row[columnIndex + 1].ToString();
                    string ProductTree_LeadTimeValue = row[columnIndex + 2].ToString();

                    if (!string.IsNullOrEmpty(SupplierPriceMatrixBranch_ProductIDValue))
                    {
                        var SupplierPriceMatrixBranch = doc.CreateElement(XMLSuppliersListFields.SupplierPriceMatrixBranch.ToString());

                        var SupplierPriceMatrixBranch_ProductIDTagName = doc.CreateElement(XMLSuppliersListFields.SupplierPriceMatrixBranch_ProductID.ToString());
                        SupplierPriceMatrixBranch_ProductIDTagName.InnerText = SupplierPriceMatrixBranch_ProductIDValue;
                        SupplierPriceMatrixBranch.AppendChild(SupplierPriceMatrixBranch_ProductIDTagName);

                        var SupplierPriceMatrixBranch_UnitPriceTagName = doc.CreateElement(XMLSuppliersListFields.SupplierPriceMatrixBranch_UnitPrice.ToString());
                        SupplierPriceMatrixBranch_UnitPriceTagName.InnerText = ProductTree_UnitPriceValue;
                        SupplierPriceMatrixBranch.AppendChild(SupplierPriceMatrixBranch_UnitPriceTagName);

                        var SupplierPriceMatrixBranch_LeadTimeTagName = doc.CreateElement(XMLSuppliersListFields.SupplierPriceMatrixBranch_LeadTime.ToString());
                        SupplierPriceMatrixBranch_LeadTimeTagName.InnerText = ProductTree_LeadTimeValue;
                        SupplierPriceMatrixBranch.AppendChild(SupplierPriceMatrixBranch_LeadTimeTagName);

                        SupplierPriceMatrix.AppendChild(SupplierPriceMatrixBranch);
                    }

                    columnIndex = columnIndex + 3;
                }

                supplier.AppendChild(SupplierPriceMatrix);

                suppliersList.AppendChild(supplier);
            }
            return doc;
        }

    }//end class
}
