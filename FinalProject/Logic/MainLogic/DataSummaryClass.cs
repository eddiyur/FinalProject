﻿using OperationalTrainer.Data_Structures;
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
        private WarehouseManager Warehouse;
        private DataManager dataManager;
        private DateTime CurrnetTime;
        public DataSummaryClass(WarehouseManager warehouse, DataManager dataManager, FinanceManager bank, DateTime currnetTime, MarketingManager marketingManager, PurchaseManager purchaseManager)
        {
            Warehouse = warehouse;
            this.dataManager = dataManager;

            CurrnetTime = currnetTime;
        }


        private DataTable sortDataTable(DataTable dt, string columnName)
        {
            DataView dv = new DataView(dt);
            dv.Sort = columnName + " ASC";
            return dv.ToTable();
        }
        enum CustomerOrderColumnsNames
        {
            Customer_Name,
            Customer_ID,
            Order_ID,
            Delivery_Date,
            Total_Order_Price,
            Possible_To_Deliver
        }


        public DataTable GenerateCustomerOrdersDataTable(OrdersList customerOrderList)
        {
            DataTable customerOrdersTable = new DataTable();
            foreach (CustomerOrderColumnsNames header in Enum.GetValues(typeof(CustomerOrderColumnsNames)))
                customerOrdersTable.Columns.Add(header.ToString());

            customerOrdersTable.Columns[CustomerOrderColumnsNames.Delivery_Date.ToString()].DataType = typeof(DateTime);

            foreach (Order order in customerOrderList.OrderList)
            {
                DataRow drow = customerOrdersTable.NewRow();
                drow[CustomerOrderColumnsNames.Customer_Name.ToString()] = order.Person.Name;
                drow[CustomerOrderColumnsNames.Customer_ID.ToString()] = order.Person.ID;
                drow[CustomerOrderColumnsNames.Order_ID.ToString()] = order.OrderID;
                drow[CustomerOrderColumnsNames.Delivery_Date.ToString()] = order.OrderDeliveryDate.ToShortDateString();
                drow[CustomerOrderColumnsNames.Total_Order_Price.ToString()] = order.GetOrderAmount().ToString();
                drow[CustomerOrderColumnsNames.Possible_To_Deliver.ToString()] = Warehouse.CanGetProducts(order).ToString();
                customerOrdersTable.Rows.Add(drow);
            }
            customerOrdersTable = sortDataTable(customerOrdersTable, CustomerOrderColumnsNames.Delivery_Date.ToString());
            return customerOrdersTable;
        }//end GenerateCustomerDataTable

        enum SupplierOrderColumnsNames
        {
            Supplier_Name,
            Supplier_ID,
            Delivery_Date,
            Total_Order_Price
        }


        public DataTable GenerateSupplierOrdersDataTable(OrdersList supplierOrderList)
        {
            DataTable supplierOrderListOrdersTable = new DataTable();
            foreach (SupplierOrderColumnsNames header in Enum.GetValues(typeof(SupplierOrderColumnsNames)))
                supplierOrderListOrdersTable.Columns.Add(header.ToString());

            supplierOrderListOrdersTable.Columns[SupplierOrderColumnsNames.Delivery_Date.ToString()].DataType = typeof(DateTime);

            foreach (Order order in supplierOrderList.OrderList)
            {
                DataRow drow = supplierOrderListOrdersTable.NewRow();
                drow[SupplierOrderColumnsNames.Supplier_Name.ToString()] = order.Person.Name;
                drow[SupplierOrderColumnsNames.Supplier_ID.ToString()] = order.Person.ID;
                drow[SupplierOrderColumnsNames.Delivery_Date.ToString()] = order.OrderDeliveryDate.ToShortDateString();
                drow[SupplierOrderColumnsNames.Total_Order_Price.ToString()] = order.GetOrderAmount().ToString();
                supplierOrderListOrdersTable.Rows.Add(drow);
            }
            supplierOrderListOrdersTable = sortDataTable(supplierOrderListOrdersTable, SupplierOrderColumnsNames.Delivery_Date.ToString());
            return supplierOrderListOrdersTable;
        }//end GenerateCustomerDataTable


        enum BankColumnNames
        {
            Date,
            Order_Type,
            Order_ID,
            Amount,
            Total
        }

        public DataTable GenerateBank(OrdersList purchaseOrders, OrdersList customersOrderList, double bankCurrentBalance)
        {

            DataTable BankDataTable = new DataTable();
            foreach (BankColumnNames header in Enum.GetValues(typeof(BankColumnNames)))
                BankDataTable.Columns.Add(header.ToString());

            BankDataTable.Columns[BankColumnNames.Date.ToString()].DataType = typeof(DateTime);

            BankDataTable = bankInitRow(BankDataTable, bankCurrentBalance);
            BankDataTable = BankOrdersToTable(BankDataTable, customersOrderList);
            BankDataTable = BankOrdersToTable(BankDataTable, purchaseOrders);

            BankDataTable = sortDataTable(BankDataTable, BankColumnNames.Date.ToString());

            for (int i = 1; i < BankDataTable.Rows.Count; i++)
            {
                BankDataTable.Rows[i].SetField(BankColumnNames.Total.ToString(),
                   (Convert.ToInt32(BankDataTable.Rows[i - 1][BankColumnNames.Total.ToString()]) + Convert.ToInt32(BankDataTable.Rows[i][BankColumnNames.Amount.ToString()])));
            }


            return BankDataTable;
        }

        private DataTable bankInitRow(DataTable dt, double currentBalance)
        {
            DataRow drow = dt.NewRow();
            drow[BankColumnNames.Date.ToString()] = CurrnetTime;
            drow[BankColumnNames.Total.ToString()] = currentBalance;
            dt.Rows.Add(drow);
            return dt;
        }

        private DataTable BankOrdersToTable(DataTable dt, OrdersList ordersList)
        {
            foreach (Order order in ordersList.OrderList)
            {
                int orderType = 1;
                if (order.OrderType == Order.OrderTypeEnum.SupplierOrder)
                    orderType = -1;

                DataRow drow = dt.NewRow();
                drow[BankColumnNames.Date.ToString()] = order.OrderDeliveryDate;
                drow[BankColumnNames.Order_ID.ToString()] = order.OrderID;
                drow[BankColumnNames.Amount.ToString()] = order.GetOrderAmount() * orderType;
                drow[BankColumnNames.Order_Type.ToString()] = order.OrderType;
                dt.Rows.Add(drow);
            }
            return dt;
        }


        enum WarehouseCulumnsNames
        {
            Date,
            Order_Type,
            Order_ID,
            Amount,
            Total,
            Total_Capacity
        }

        public DataTable GenerateWarehouse(OrdersList supplierOrderList, OrdersList customersOrderList)
        {
            DataTable WarehouseDataTable = new DataTable();

            WarehouseDataTable.Columns.Add(WarehouseCulumnsNames.Date.ToString());
            WarehouseDataTable.Columns.Add(WarehouseCulumnsNames.Order_Type.ToString());
            WarehouseDataTable.Columns.Add(WarehouseCulumnsNames.Order_ID.ToString());

            foreach (ProductClass Product in dataManager.MetaData.ProductsMetaData.ProductList)
            {
                WarehouseDataTable.Columns.Add(Product.ProductName + " " + WarehouseCulumnsNames.Amount.ToString());
                WarehouseDataTable.Columns.Add(Product.ProductName + " " + WarehouseCulumnsNames.Total.ToString());
            }
            WarehouseDataTable.Columns.Add(WarehouseCulumnsNames.Total_Capacity.ToString());
            WarehouseDataTable.Columns[WarehouseCulumnsNames.Date.ToString()].DataType = typeof(DateTime);

            WarehouseDataTable = WarehouseInitRow(WarehouseDataTable);

            WarehouseDataTable = WarehouseOrdersToTable(WarehouseDataTable, supplierOrderList);
            WarehouseDataTable = WarehouseOrdersToTable(WarehouseDataTable, customersOrderList);

            WarehouseDataTable = sortDataTable(WarehouseDataTable, WarehouseCulumnsNames.Date.ToString());
            WarehouseDataTable = WarehouseTotalAmount(WarehouseDataTable);
            WarehouseDataTable = WarehouseCalculateTotal(WarehouseDataTable);

            return WarehouseDataTable;
        }


        private DataTable WarehouseCalculateTotal(DataTable warehouseDataTable)
        {

            foreach (DataRow drow in warehouseDataTable.Rows)
            {
                double rowTotalCapacity = 0;
                foreach (ProductClass product in dataManager.MetaData.ProductsMetaData.ProductList)
                {
                    string totalColumnName = product.ProductName + " " + WarehouseCulumnsNames.Total.ToString();
                    int totalAmoun = Convert.ToInt32(drow[totalColumnName].ToString());
                    if (totalAmoun < 0)
                        totalAmoun = 0;

                    rowTotalCapacity = rowTotalCapacity + totalAmoun * product.ProductCapacity;
                }
                drow.SetField(WarehouseCulumnsNames.Total_Capacity.ToString(), rowTotalCapacity);
            }
            return warehouseDataTable;
        }

        private DataTable WarehouseTotalAmount(DataTable warehouseDataTable)
        {

            foreach (ProductClass product in dataManager.MetaData.ProductsMetaData.ProductList)
            {
                for (int i = 1; i < warehouseDataTable.Rows.Count; i++)
                {
                    string totalColumnName = product.ProductName + " " + WarehouseCulumnsNames.Total.ToString();
                    int previousTotal = Convert.ToInt32(warehouseDataTable.Rows[i - 1][product.ProductName + " " + WarehouseCulumnsNames.Total.ToString()]);
                    int currentAmount = 0;

                    string currentAmountSTR = warehouseDataTable.Rows[i][product.ProductName + " " + WarehouseCulumnsNames.Amount.ToString()].ToString();
                    if (!string.IsNullOrEmpty(currentAmountSTR))
                        currentAmount = Convert.ToInt32(warehouseDataTable.Rows[i][product.ProductName + " " + WarehouseCulumnsNames.Amount.ToString()]);

                    warehouseDataTable.Rows[i].SetField(totalColumnName, previousTotal + currentAmount);
                }
            }
            return warehouseDataTable;
        }

        private DataTable WarehouseOrdersToTable(DataTable warehouseDataTable, OrdersList ordersList)
        {
            foreach (Order order in ordersList.OrderList)
            {
                int orderType = -1;
                if (order.OrderType == Order.OrderTypeEnum.SupplierOrder)
                    orderType = 1;

                DataRow drow = warehouseDataTable.NewRow();
                drow[WarehouseCulumnsNames.Date.ToString()] = order.OrderDeliveryDate;
                drow[WarehouseCulumnsNames.Order_ID.ToString()] = order.OrderID;
                drow[WarehouseCulumnsNames.Order_Type.ToString()] = order.OrderType;

                foreach (PriceTable priceTable in order.OrderProductsList)
                    drow[(priceTable.Product.ProductName + " " + WarehouseCulumnsNames.Amount.ToString())] = priceTable.Amount * orderType;

                warehouseDataTable.Rows.Add(drow);
            }
            return warehouseDataTable;
        }

        private DataTable WarehouseInitRow(DataTable warehouseDataTable)
        {
            DataRow drow = warehouseDataTable.NewRow();
            drow[WarehouseCulumnsNames.Date.ToString()] = CurrnetTime;

            foreach (ProductClass product in dataManager.MetaData.ProductsMetaData.ProductList)
                drow[product.ProductName + " " + WarehouseCulumnsNames.Total] = Warehouse.GetAmount(product);

            warehouseDataTable.Rows.Add(drow);
            return warehouseDataTable;
        }



        enum productionColumnsNames
        {
            ToolName,
            ToolID,
            ToolType,
            CurrentStatus,
            ProductName,
            OrderID,
            TimeRemaining

        }


        public DataTable toolListToDT(ToolList toolsList)
        {
            DataTable toolListTable = new DataTable();
            foreach (productionColumnsNames header in Enum.GetValues(typeof(productionColumnsNames)))
                toolListTable.Columns.Add(header.ToString());

            foreach (Tool tool in toolsList.toolList)
            {
                DataRow dRow = toolListTable.NewRow();

                dRow[productionColumnsNames.ToolID.ToString()] = tool.ToolID;
                dRow[productionColumnsNames.ToolName.ToString()] = tool.ToolName;
                dRow[productionColumnsNames.ToolType.ToString()] = tool.ToolType.ToString();
                dRow[productionColumnsNames.CurrentStatus.ToString()] = tool.CurrentStatus.ToString();
                if (tool.CurrentProductionOrder != null)
                {
                    dRow[productionColumnsNames.ProductName.ToString()] = tool.CurrentProductionOrder.Product.ProductName;
                    dRow[productionColumnsNames.OrderID.ToString()] = tool.CurrentProductionOrder.OrderID;
                }
                dRow[productionColumnsNames.TimeRemaining.ToString()] = tool.ProcessingTimeRemaining.ToString();

                toolListTable.Rows.Add(dRow);
            }

            return toolListTable;
        }
    }//end DataSummaryClass
}
