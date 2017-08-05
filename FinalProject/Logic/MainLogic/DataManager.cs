using FinalProject.Data_Structures;
using OperationalTrainer.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationalTrainer.Logic.MainLogic
{
    public class DataStructureClass
    {

        public ProductClassList ProductsMetaDataList { get; set; }
    //    public SuppliersList SuppliersList { get; set; }
        //public OrdersList CustomersOrderList { get; set; }
     //   public OrdersList SupplieOrderList { get; set; }
        //public OrdersList FutureCustomersOrderList { get; set; }

        public ToolTypeClassList ToolTypeMetaDataList { get; set; }
        public ToolsList ToolsMetaDataList { get; set; }
    }


    public class InitDataStructureClass
    {
        public DateTime startDate { get; set; }
        public double WarehouseMaxCapacity { get; set; }

        public SuppliersList InitSuppliersMetaData { get; set; }
        public OrdersList InitPurchaseOrders { get; set; }

        public OrdersList CustomersOrderList { get; set; }
        public OrdersList FutureCustomersOrderList { get; set; }


        public Dictionary<ProductClass, double> InitWarehouseInventory { get; set; }
        public double InitBankCurrentBalance { get; set; }
        public ProductionOrderList InitProductionOrderList { get; set; }
    }

    public class InitDataLoad
    {
        public InitDataStructureClass InitDataStructure { get; set; }
        public DataStructureClass DataStructure { get; set; }

        public InitDataLoad()
        { DataStructure = new DataStructureClass(); }
    }

    public class DataManager
    {
        public DataStructureClass DataSet { get; set; }
        public DateTime CurrnetTime { get; set; }

        public DataManager(DataStructureClass dataSet)
        { DataSet = dataSet; }

        public void UpdateTime(DateTime currnetTime)
        { CurrnetTime = currnetTime; }

        /// <summary>
        /// Extract Orders with given date from future Customers Order List
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public OrdersList getNewCustomerOrdersList(DateTime date)
        {
            OrdersList newCustomerOrdersList = DataSet.FutureCustomersOrderList.GetOrdersByOrderDate(date);
            DataSet.FutureCustomersOrderList.RemoveOrders(newCustomerOrdersList);
            return newCustomerOrdersList;
        }

        

    }
}
