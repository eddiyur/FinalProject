using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data_Structures
{
    public class ProductClass
    {
        //struct production progress


        public ProductClass(string productID,string productName, double productCapacity)
        {
            ProductID = productID;
            ProductName = productName;
            ProductCapacity = productCapacity;
            ProductTree = new Dictionary<ProductClass, int>();
            ProductionToolList = new List<Tool>();
            setInitProductionProgress();
            //ProductionProgress = new Dictionary<Tool, ProductionStatusEnum>();
        }
        public enum ProductionStatusEnum
        {
            NotStarted,
            InProgress,
            Ready
        }

        public string ProductID { get; set; }
        public string ProductName { get; set; }
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
