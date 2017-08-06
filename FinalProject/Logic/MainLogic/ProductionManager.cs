using FinalProject.Data_Structures;
using OperationalTrainer.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject.Logic.MainLogic
{
    public class ProductionManager
    {
        private Dictionary<ToolTypeClass, ProductionOrderList> ProductionQueue;

        private ToolList ToolsList;

        public ProductionManager(ToolList toolList, ToolTypeClassList ToolTypeList)
        {
            ToolsList = toolList;
            ProductionQueue = new Dictionary<ToolTypeClass, ProductionOrderList>();

            foreach (var toolType in ToolTypeList.ToolTypeList)
                ProductionQueue.Add(toolType, new ProductionOrderList());

        }

        public ToolList GetToolsList()
        { return ToolsList; }

        public void AddProductionOrder(ProductionOrder productionOrder)
        {
            ToolTypeClass toolType = productionOrder.ProductionProgress.ExtractFirstToolType();
            ProductionQueue[toolType].AddOrder(productionOrder);
        }

        public void tempStartProduction()
        {
            foreach (var tool in ToolsList.toolList)
            {
                if (tool.CurrentStatus == Tool.ToolStatuses.Idle)
                {
                    ProductionOrder productionOrder = ProductionQueue[tool.ToolType].getTopOrder();
                    if (productionOrder != null)
                    {
                        tool.StartProduction(productionOrder);
                    }



                }
            }
        }


        public void tempNextTick(DateTime currentTime)
        {
            foreach (Tool tool in ToolsList.toolList)
            {
                tool.TimeTick();

                if (tool.CurrentStatus == Tool.ToolStatuses.Idle)
                {
                    ProductionOrder productionOrder = ProductionQueue[tool.ToolType].getTopOrder();
                    if (productionOrder != null)
                    {
                        tool.StartProduction(productionOrder);
                    }
                }

                string info = tool.ToolName + "\n" +
                                       tool.CurrentStatus.ToString() + "\n" +
                                                                              tool.ProcessingTimeRemaining.ToString() + "\n";
                if (tool.CurrentProductionOrder != null)
                    info = info + tool.CurrentProductionOrder.OrderID + "\n";
                else
                    info = info + "no order";

                MessageBox.Show(info);
            }
        }


    }
}
