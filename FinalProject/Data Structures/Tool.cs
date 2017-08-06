using FinalProject.Data_Structures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UtilitiesFileManager;

namespace OperationalTrainer.Data_Structures
{

    public class SetupTimeStructure
    {
        public ProductClass SourceProduct { get; set; }
        public ProductClass TargetProduct { get; set; }
        public int SetupTime { get; set; }
    }

    public class ProductionStatistics
    {
        public int MaxTimeUnits;
        public int CurrentTimeUnits;
        public int ProdactionTime;
        public int SetupTime;
        public int IdleTime;
        public int TechCate;
        public int WaithingTime;
    }

    public class Tool
    {
        public enum ToolStatuses
        {
            Production,
            Setup,
            Service,
            Idle,
            OutOfShipt
        }

        public enum ProductionMethods
        {
            FIFO,
            EDD,
            CurrentJob,
            SPT
        }

        public string ToolID { get; set; }
        public string ToolName { get; set; }
        public ToolTypeClass ToolType { get; set; }
        public int ShiftStartTime { get; set; }
        public int ShiftEndTime { get; set; }
        public List<SetupTimeStructure> SetUpTimes { get; set; }
        public Dictionary<ProductClass, int> ProductsProcessingTime { get; set; }
        public int ProcessingTimeRemaining { get; set; }
        public ToolStatuses CurrentStatus { get; set; }
        public ProductionMethods ProductionMethod { get; set; }
        public ProductClass CurrentSetup { get; set; }
        public ProductionOrder CurrentProductionOrder { get; set; }
        //        public ProductionOrder NextProductionOrder { get; set; }
        public ProductionStatistics productionStatistics { get; set; }

        public Tool()
        {
            ProductsProcessingTime = new Dictionary<ProductClass, int>();
            SetUpTimes = new List<SetupTimeStructure>();
            CurrentStatus = ToolStatuses.Idle;
            ProductionMethod = ProductionMethods.FIFO;
            productionStatistics = new ProductionStatistics();
        }


        public void TimeTick()
        {
            switch (CurrentStatus)
            {
                case ToolStatuses.Production:
                    tampNameProduction();
                    break;
                case ToolStatuses.Setup:
                    break;
                case ToolStatuses.Service:
                    break;
                case ToolStatuses.Idle:
                    break;
                case ToolStatuses.OutOfShipt:
                    break;
                default:
                    break;
            }
        }

        private void tampNameProduction()
        {
            ProcessingTimeRemaining--;

            if (ProcessingTimeRemaining < 1)
                FinishProduction();
        }

        public void FinishProduction()
        {
            MessageBox.Show("FinishProduction");
            CurrentStatus = ToolStatuses.Idle;
        }

        public void StartProduction(ProductionOrder productionOrder)
        {
            CurrentProductionOrder = productionOrder;
            ProcessingTimeRemaining = getProductionTime(productionOrder.Product);
            CurrentStatus = ToolStatuses.Production;
        }


        public void ContinueProduction()
        {
            ProcessingTimeRemaining = ProcessingTimeRemaining - 1;
        }


        /// <summary>
        /// Return the prosesing time of a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        private int getProductionTime(ProductClass product)
        { return ProductsProcessingTime[product]; }



    }//end class Tool




    public class ToolList
    {
        public List<Tool> toolList { get; set; }
        public ToolList()
        { toolList = new List<Tool>(); }

        public void AddTool(Tool tool)
        { toolList.Add(tool); }

    }
}

