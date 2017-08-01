using OperationalTrainer.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Logic.MainLogic
{
    public class ProductionManager
    {
        public Dictionary<ToolTypeClass, ProductionOrderList> ProductionQueue;

        public ToolsList ToolList;

        public ProductionManager(ToolsList toolList)
        {
            ToolList = new ToolsList();
            ProductionQueue = new Dictionary<ToolTypeClass, ProductionOrderList>();
        }

    }
}
