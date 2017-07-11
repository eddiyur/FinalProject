using OperationalTrainer.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace FinalProject.FileManagerFolder
{
    public static class OrderParser
    {
        enum XMLOrderFields
        {
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

        public static OrdersList Parse(XmlNodeList OrderNodeList, ProductClassList productsList, Order.OrderTypeEnum orderType)
        {
            return Parse(OrderNodeList, productsList, orderType, null);
        }

        public static OrdersList Parse(XmlNodeList OrderNodeList, ProductClassList productsList, Order.OrderTypeEnum orderType, SuppliersList suppliersList)
        {
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
                    order.Person = suppliersList.GetSupplier(customer.ID);
                
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

    }
}
