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
        public Dictionary<ProductClass, int> ProcessingTime { get; set; }
        public ToolStatuses ToolStatus { get; set; }
        public ProductionMethods ProductionMethod { get; set; }
        public List<SetupTimeStructure> SetUpTimes { get; set; }
        public ProductClass CurrentSetup { get; set; }
        public ProductionOrder CurrentProductionOrder { get; set; }
        public ProductionOrder NextProductionOrder { get; set; }
        public ProductionStatistics productionStatistics { get; set; }

        public Tool()
        {
            ProcessingTime = new Dictionary<ProductClass, int>();
            ToolStatus = ToolStatuses.Idle;
            ProductionMethod = ProductionMethods.FIFO;
            SetUpTimes = new List<SetupTimeStructure>();
            productionStatistics = new ProductionStatistics();
        }


    }//end class Tool

    public class ToolsList
    {
        public List<Tool> toolList { get; set; }
        public ToolsList()
        {
            toolList = new List<Tool>();
        }

        public void AddTool(Tool tool)
        {
            toolList.Add(tool);
        }

    }
}

