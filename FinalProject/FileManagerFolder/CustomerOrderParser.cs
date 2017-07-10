using OperationalTrainer.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace OperationalTrainer.FileManagerFolder
{
    public static class CustomerOrderParser
    {
        enum XMLCustomerOrderFields
        {
            CustomerName,
            CustomerID,
            CustomerOrderID,
            CustomerOrderDate,
            CustomerOrderDeliveryDate,
            CustomerOrder_Status,
            CustomerOrderProductsList,
            CustomerOrderProductsListBranch,
            CustomerOrderProductsListBranch_ProductID,
            CustomerOrderProductsListBranch_Amount,
            CustomerOrderProductsListBranch_Price
        };

        public static OrdersList Parse(XmlNodeList customerOrderNodeList, ProductClassList productsList)
        {
            OrdersList customerOrderList = new OrdersList();

            foreach (XmlNode customerOrderNode in customerOrderNodeList)//orders level
            {
                Order order = new Order(Order.OrderTypeEnum.CustomerOrder);
                Customer customer = new Customer();


                foreach (XmlNode customerOrderParameter in customerOrderNode)//order parts level
                {
                    XMLCustomerOrderFields XMLcustomerOrderField = (XMLCustomerOrderFields)Enum.Parse(typeof(XMLCustomerOrderFields), customerOrderParameter.Name, true);

                    switch (XMLcustomerOrderField)
                    {
                        case XMLCustomerOrderFields.CustomerName:
                            customer.Name = customerOrderParameter.InnerText;
                            break;
                        case XMLCustomerOrderFields.CustomerID:
                            customer.ID = customerOrderParameter.InnerText;
                            break;
                        case XMLCustomerOrderFields.CustomerOrderID:
                            order.OrderID = customerOrderParameter.InnerText;
                            break;
                        case XMLCustomerOrderFields.CustomerOrderDate:
                            order.OrderDate = DateTime.Parse(customerOrderParameter.InnerText);
                            break;
                        case XMLCustomerOrderFields.CustomerOrderDeliveryDate:
                            order.OrderDeliveryDate = DateTime.Parse(customerOrderParameter.InnerText);
                            break;
                        case XMLCustomerOrderFields.CustomerOrder_Status:
                            order.OrderStatus = (Order.OrderStatusEnum)Enum.Parse(typeof(Order.OrderStatusEnum), customerOrderParameter.InnerText, true);
                            break;
                        case XMLCustomerOrderFields.CustomerOrderProductsList:
                            order.OrderProductsList = getOrderProductsList(customerOrderParameter, productsList);
                            break;
                        default:
                            break;
                    }//end switch

                }// end order parts level
                order.Person = customer;
                customerOrderList.AddOrder(order);
            }//end orders level



            return customerOrderList;
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
                        XMLCustomerOrderFields XMLSuppliersListField = (XMLCustomerOrderFields)Enum.Parse(typeof(XMLCustomerOrderFields), orderProductRowParameter.Name, true);

                        switch (XMLSuppliersListField)
                        {
                            case XMLCustomerOrderFields.CustomerOrderProductsListBranch_ProductID:
                                {
                                    ProductClass product = productsList.GetProduct(orderProductRowParameter.InnerText);
                                    if (product == null)
                                        MessageBox.Show("Wrong Product in Supplier Matrix", "Error");
                                    priceTable.Product = product;
                                    break;
                                }
                            case XMLCustomerOrderFields.CustomerOrderProductsListBranch_Amount:
                                priceTable.Amount = int.Parse(orderProductRowParameter.InnerText);
                                break;
                            case XMLCustomerOrderFields.CustomerOrderProductsListBranch_Price:
                                priceTable.Price = double.Parse(orderProductRowParameter.InnerText);
                                break;
                            default:
                                break;
                        }//end switch
                    }//end piceMatrix parameters level
                    priceMatrix.Add(priceTable);
                }//end priceMatrix level
            }//end try
            catch (Exception)
            {
                return null;
            }
            return priceMatrix;
        }

    }
}
    
