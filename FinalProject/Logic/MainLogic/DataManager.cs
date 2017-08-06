using FinalProject.Data_Structures;
using OperationalTrainer.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationalTrainer.Logic.MainLogic
{
    /// <summary>
    /// Store MetaData parameters
    /// </summary>
    public class MetaDataStructure
    {
        public ProductClassList ProductsMetaData;
        public ToolTypeClassList ToolTypeMetaData;
        public SuppliersList SuppliersMetaData;
    }

    /// <summary>
    /// Store initial parameters of the system
    /// </summary>
    public class InitParametersClass
    {
        public InitParametersClass() { }
        public DateTime startDate;
        public double WarehouseMaxCapacity;
        public Dictionary<ProductClass, double> InitWarehouseInventory;
        public double InitBankStartBalance;
    }

    /// <summary>
    /// Store initial simulator lists 
    /// </summary>
    public class InitListsClass
    {
        public InitListsClass() { }

        // Purchese lists
        public OrdersList InitPurchaseOrders;

        //Marketing Lists
        public OrdersList InitCustomersOrderList;
        public OrdersList InitFutureCustomersOrderList;

        //Production Lists
        public ProductionOrderList InitProductionOrderList;
        public ToolList InitToolsList;
    }



    public class InitDataLoad
    {
        public MetaDataStructure MetaData { get; set; }
        public InitParametersClass InitParameters { get; set; }
        public InitListsClass InitLists { get; set; }

        public InitDataLoad()
        {
            MetaData = new MetaDataStructure();
            InitParameters = new InitParametersClass();
            InitLists = new InitListsClass();
        }
    }

    public class DataManager
    {
        public MetaDataStructure MetaData { get; set; }
        public DateTime CurrnetTime { get; set; }

        public DataManager(MetaDataStructure metaDataSet)
        { MetaData = metaDataSet; }

        public void UpdateTime(DateTime currnetTime)
        { CurrnetTime = currnetTime; }

        /// <summary>
        /// Rerutn Suppliers Metadata
        /// </summary>
        /// <returns></returns>
        public SuppliersList getSuppliersMetaData()
        { return MetaData.SuppliersMetaData; }


        /// <summary>
        /// Return Product MetaData
        /// </summary>
        /// <returns></returns>
        public ProductClassList getProductsMetaData()
        { return MetaData.ProductsMetaData; }
    }
}
