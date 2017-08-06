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

namespace FinalProject.FileManagerFolder
{
    public static class OrderParser
    {
        enum XMLOrderFields
        {
            Order,
            PersonName,
            PersonID,
            OrderID,
            OrderDate,
            OrderDeliveryDate,
            Order_Status,
            OrderProductsList,
            OrderProductsListBranch,
            OrderProductsListBranch_ProductID,
            OrderProductsListBranch_Amount,
            OrderProductsListBranch_Price
        };

        public static OrdersList Parse(XmlNodeList OrderNodeList, InitDataLoad initDataLoad, Order.OrderTypeEnum orderType)
        {
            ProductClassList productsList = initDataLoad.MetaData.ProductsMetaData;
            SuppliersList SuppliersMetaData = initDataLoad.MetaData.SuppliersMetaData;
            OrdersList orderList = new OrdersList();

            foreach (XmlNode customerOrderNode in OrderNodeList)//orders level
            {
                Order order = new Order(orderType);
                Customer customer = new Customer();

                foreach (XmlNode orderParameter in customerOrderNode)//order parts level
                {
                    XMLOrderFields XMLcustomerOrderField = (XMLOrderFields)Enum.Parse(typeof(XMLOrderFields), orderParameter.Name, true);

                    switch (XMLcustomerOrderField)
                    {
                        case XMLOrderFields.PersonName:
                            customer.Name = orderParameter.InnerText;
                            break;
                        case XMLOrderFields.PersonID:
                            customer.ID = orderParameter.InnerText;
                            break;
                        case XMLOrderFields.OrderID:
                            order.OrderID = orderParameter.InnerText;
                            break;
                        case XMLOrderFields.OrderDate:
                            order.OrderDate = DateTime.Parse(orderParameter.InnerText);
                            break;
                        case XMLOrderFields.OrderDeliveryDate:
                            order.OrderDeliveryDate = DateTime.Parse(orderParameter.InnerText);
                            break;
                        case XMLOrderFields.Order_Status:
                            order.OrderStatus = (Order.OrderStatusEnum)Enum.Parse(typeof(Order.OrderStatusEnum), orderParameter.InnerText, true);
                            break;
                        case XMLOrderFields.OrderProductsList:
                            order.OrderProductsList = getOrderProductsList(orderParameter, productsList);
                            break;
                        default:
                            break;
                    }//end switch
                }// end order parts level
                if (orderType == Order.OrderTypeEnum.CustomerOrder)
                    order.Person = customer;
                else
                    order.Person = SuppliersMetaData.GetSupplier(customer.ID);

                orderList.AddOrder(order);
            }//end orders level



            return orderList;
        }

        private static List<PriceTable> getOrderProductsList(XmlNode orderProductsListNode, ProductClassList productsList)
        {
            List<PriceTable> priceMatrix = new List<PriceTable>();
            try
            {
                foreach (XmlNode orderProductsListBranch in orderProductsListNode)//priceMatrix level
                {
                    PriceTable priceTable = new PriceTable();

                    XmlNodeList orderProductRowParametersList = orderProductsListBranch.ChildNodes;

                    foreach (XmlNode orderProductRowParameter in orderProductRowParametersList)//priceMatrix parameters level
                    {
                        XMLOrderFields XMLSuppliersListField = (XMLOrderFields)Enum.Parse(typeof(XMLOrderFields), orderProductRowParameter.Name, true);

                        switch (XMLSuppliersListField)
                        {
                            case XMLOrderFields.OrderProductsListBranch_ProductID:
                                {
                                    ProductClass product = productsList.GetProduct(orderProductRowParameter.InnerText);
                                    if (product == null)
                                        MessageBox.Show("Wrong Product in Supplier Matrix", "Error");
                                    priceTable.Product = product;
                                    break;
                                }
                            case XMLOrderFields.OrderProductsListBranch_Amount:
                                priceTable.Amount = int.Parse(orderProductRowParameter.InnerText);
                                break;
                            case XMLOrderFields.OrderProductsListBranch_Price:
                                priceTable.Price = double.Parse(orderProductRowParameter.InnerText);
                                break;
                            default:
                                break;
                        }//end switch
                    }//end piceMatrix parameters level
                    priceMatrix.Add(priceTable);
                }//end priceMatrix level
            }//end try
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
            return priceMatrix;
        }

        public static XmlDocument OrderCSVToXML(XmlDocument doc, string SourcefileName, LoadData.XMLMainCategories xMLMainCategories)
        {

            FileManager fm = new FileManager();
            DataTable dt = fm.GetCSV(SourcefileName);

            var root = doc.GetElementsByTagName(LoadData.XMLMainCategories.dataset.ToString())[0];
            var OrderList = doc.CreateElement(xMLMainCategories.ToString());

            root.AppendChild(OrderList);

            foreach (DataRow row in dt.Rows)
            {
                var order = doc.CreateElement(XMLOrderFields.Order.ToString());

                var personNameTagName = doc.CreateElement(XMLOrderFields.PersonName.ToString());
                personNameTagName.InnerText = row[0].ToString();
                order.AppendChild(personNameTagName);

                var personIdTagName = doc.CreateElement(XMLOrderFields.PersonID.ToString());
                personIdTagName.InnerText = row[1].ToString();
                order.AppendChild(personIdTagName);

                var OrderIDTagName = doc.CreateElement(XMLOrderFields.OrderID.ToString());
                OrderIDTagName.InnerText = row[2].ToString();
                order.AppendChild(OrderIDTagName);

                var OrderDateTagName = doc.CreateElement(XMLOrderFields.OrderDate.ToString());
                OrderDateTagName.InnerText = LoadData.transfareDate(row[3].ToString());
                order.AppendChild(OrderDateTagName);

                var OrderDeliveryDateTagName = doc.CreateElement(XMLOrderFields.OrderDeliveryDate.ToString());
                OrderDeliveryDateTagName.InnerText = LoadData.transfareDate(row[4].ToString());
                order.AppendChild(OrderDeliveryDateTagName);

                var Order_StatusTagName = doc.CreateElement(XMLOrderFields.Order_Status.ToString());
                Order_StatusTagName.InnerText = row[5].ToString();
                order.AppendChild(Order_StatusTagName);

                var ProductTreeTagName = doc.CreateElement(XMLOrderFields.OrderProductsList.ToString());

                int maxSons = 3;
                if (xMLMainCategories == LoadData.XMLMainCategories.SuppliersOrderList)
                    maxSons = 1;

                int columnIndex = 6;
                for (int i = 0; i < maxSons; i++)
                {
                    string ProductTree_ProductIDValue = row[columnIndex].ToString();
                    string ProductTree_AmountValue = row[columnIndex + 1].ToString();
                    string ProductTree_pricetValue = row[columnIndex + 2].ToString();

                    if (!string.IsNullOrEmpty(ProductTree_ProductIDValue))
                    {
                        var ProductTreeBranchTagName = doc.CreateElement(XMLOrderFields.OrderProductsListBranch.ToString());
                        var OrderProductsListBranch_ProductIDTagName = doc.CreateElement(XMLOrderFields.OrderProductsListBranch_ProductID.ToString());
                        OrderProductsListBranch_ProductIDTagName.InnerText = ProductTree_ProductIDValue;
                        ProductTreeBranchTagName.AppendChild(OrderProductsListBranch_ProductIDTagName);

                        var OrderProductsListBranch_AmountTagName = doc.CreateElement(XMLOrderFields.OrderProductsListBranch_Amount.ToString());
                        OrderProductsListBranch_AmountTagName.InnerText = ProductTree_AmountValue;
                        ProductTreeBranchTagName.AppendChild(OrderProductsListBranch_AmountTagName);

                        var OrderProductsListBranch_PriceTagName = doc.CreateElement(XMLOrderFields.OrderProductsListBranch_Price.ToString());
                        OrderProductsListBranch_PriceTagName.InnerText = ProductTree_pricetValue;
                        ProductTreeBranchTagName.AppendChild(OrderProductsListBranch_PriceTagName);

                        ProductTreeTagName.AppendChild(ProductTreeBranchTagName);
                    }

                    columnIndex = columnIndex + 3;
                }
                order.AppendChild(ProductTreeTagName);

                OrderList.AppendChild(order);
            }
            return doc;
        }

    }//end class
}
