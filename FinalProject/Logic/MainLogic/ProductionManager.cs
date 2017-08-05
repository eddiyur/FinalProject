using FinalProject.Data_Structures;
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
        private Dictionary<ToolTypeClass, ProductionOrderList> ProductionQueue;

        private ToolsList ToolList;

        public ProductionManager(ToolsList toolList, ToolTypeClassList ToolTypeList)
        {
            ToolList = toolList;
            ProductionQueue = new Dictionary<ToolTypeClass, ProductionOrderList>();

            foreach (var toolType in ToolTypeList.ToolTypeList)
                ProductionQueue.Add(toolType, new ProductionOrderList());

        }

    }
}
