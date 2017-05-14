using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data_Structures
{
    public class ProductClass
    {
        public enum ProductionStatusEnum
        {
            NotStarted,
            InProgress,
            Ready
        }

        public string ProductID { get; set; }
        public string PruductName { get; set; }
        public double ProductCapacity { get; set; }
        public Dictionary<ProductClass, int> ProductTree { get; set; }
        public List<Tool> ProductionToolList { get; set; }
        public Dictionary<Tool, ProductionStatusEnum> ProductionProgress { get; set; }

        private void setInitProductionProgress()
        {
            ProductionProgress = new Dictionary<Tool, ProductionStatusEnum>();
            foreach (Tool tool in ProductionToolList)
                ProductionProgress.Add(tool, ProductionStatusEnum.NotStarted);
        }


    }//end Product class
}
