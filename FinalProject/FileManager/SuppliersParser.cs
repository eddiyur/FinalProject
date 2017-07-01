using FinalProject.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FinalProject.FileManager
{
    public static class  SuppliersParser
    {
        enum XMLSuppliersListFields
        {
            SuppliersList,
            SupplierID,
            SupplierName,
            SupplierReliability,
            SupplierPriceMatrix,
            SupplierPriceMatrixBranch,
            SupplierPriceMatrixBranch_ProductID,
            SupplierPriceMatrixBranch_UnitPrice,
            SupplierPriceMatrixBranch_LeadTime
        }

        public static  SuppliersList Parse(XmlNodeList suppliersNodeList, ProductClassList productsList)
        {
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
                                ProductClass product = productsList.GetProduct(priceMatrixRowParameter.InnerText);
                                break;
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



    }
}
