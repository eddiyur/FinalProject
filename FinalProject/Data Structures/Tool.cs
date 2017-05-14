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
            Product SourceProduct;
            Product TargetProduct;
            double SetupTipe;
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
        public ToolStatuses ToolStatus { get; set; }
        public ProductionMethods ProductionMethod { get; set; }
        List<SetupTimeStructure> setupTime { get; set; }


    }
}

