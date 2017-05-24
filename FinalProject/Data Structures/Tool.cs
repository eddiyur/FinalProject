using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data_Structures
{
    public class Tool
    {
        public struct SetupTimeStructure
        {
            ProductClass SourceProduct;
            ProductClass TargetProduct;
            double SetupTime;
        }

        public enum ToolStatuses
        {
            Production,
            Setup,
            WaitingForTech,
            Idle
        };

        public enum ProductionMethods
        {
            FIFO,
            EDD,
            CurrentJob,
            SPT
        }
        public string ToolName { get; set; }
        public int ToolID { get; set; }
        public ProductClass CorrectSetupProduct { get; set; }
        public ToolStatuses ToolStatus { get; set; }
        public ProductionMethods ProductionMethod { get; set; }
        public List<SetupTimeStructure> setupTime { get; set; }
        public Dictionary<ProductClass, int> ProductionTime { get; set; }

    }
}

