using OperationalTrainer.Data_Structures;
using OperationalTrainer.Logic.MainLogic;
using OperationalTrainer.Logic.Warehouse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Logic.MainLogic
{
    public class DataSummaryClass
    {
        private WarehouseClass Warehouse;
        private DataManager dataManager;
        public DataSummaryClass(WarehouseClass warehouse, DataManager dataManager)
        {
            Warehouse = warehouse;
            this.dataManager = dataManager;
        }

        enum CustomerOrderColumnsNames
        {
            Customer_Name,
            Customer_ID,
            Delivery_Date,
            Total_Order_Price,
            Possible_To_Deliver
        }
        private string replaceUnderLine(string str)
        {
            return str.Replace('_', ' ');
        }

        public DataTable GenerateCustomerOrdersDataTable()
        {
            return GenerateCustomerOrdersDataTable(dataManager.DataSet.CustomersOrderList);
        }

        public DataTable GenerateCustomerOrdersDataTable(OrdersList CustomerorderList)
        {
            DataTable CustomerOrdersTable = new DataTable();
            foreach (CustomerOrderColumnsNames header in Enum.GetValues(typeof(CustomerOrderColumnsNames)))
                CustomerOrdersTable.Columns.Add(replaceUnderLine(header.ToString()));


            foreach (Order order in CustomerorderList.OrderList)
            {
                DataRow drow = CustomerOrdersTable.NewRow();
                drow = ConvertOrderToDataRow(drow, order);
                drow[replaceUnderLine(CustomerOrderColumnsNames.Possible_To_Deliver.ToString())] = Warehouse.CanGetOrder(order).ToString();
                CustomerOrdersTable.Rows.Add(drow);

            }

            return CustomerOrdersTable;
        }//end GenerateCustomerDataTable

        private DataRow ConvertOrderToDataRow(DataRow drow, Order order)
        {
            drow[replaceUnderLine(CustomerOrderColumnsNames.Customer_Name.ToString())] = order.Person.Name;
            drow[replaceUnderLine(CustomerOrderColumnsNames.Customer_ID.ToString())] = order.Person.ID;
            drow[replaceUnderLine(CustomerOrderColumnsNames.Delivery_Date.ToString())] = order.OrderDeliveryDate.ToShortDateString();
            drow[replaceUnderLine(CustomerOrderColumnsNames.Total_Order_Price.ToString())] = order.GetOrderAmount().ToString();
            return drow;
        }

    }//end DataSummaryClass
}
