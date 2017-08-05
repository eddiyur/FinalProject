using FinalProject.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OperationalTrainer.Data_Structures.Order;

namespace OperationalTrainer.Data_Structures
{
    public class ProductionOrder
    {
        public string OrderID { get; set; }
        public ProductClass Product { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime orderPullTime { get; set; }
        public DateTime OrderDeliveryDate { get; set; }
        public ToolTypeClassList ProductionProgress { get; set; }

        public ProductionOrder()
        {
        }


        public ProductionOrder(string orderID, ProductClass product, DateTime orderDate, DateTime orderDeliveryDate)
        {
            OrderID = orderID;
            Product = product;
            OrderDate = orderDate;
            OrderDeliveryDate = orderDeliveryDate;
            ProductionProgress = Product.GetCopyToolTypeList();

        }

        public override bool Equals(object obj)
        {
            ProductionOrder productionOrder = (ProductionOrder)obj;
            return OrderID.Equals(productionOrder.OrderID);
        }


        public override int GetHashCode()
        { return OrderID.GetHashCode(); }
    }//end class ProductionOrder

    public class ProductionOrderList
    {
        List<ProductionOrder> productionOrderList { get; set; }

        public ProductionOrderList()
        {
            productionOrderList = new List<ProductionOrder>();
        }

        public ProductionOrder getTopOrder()
        {
            try
            {
                ProductionOrder productionOrder = productionOrderList[0];
                productionOrderList.Remove(productionOrder);
                return productionOrder;
            }
            catch { return null; }
        }


        public void AddOrder(ProductionOrder ProductionOrder)
        {
            productionOrderList.Add(ProductionOrder);
        }
    }//end class production order

}//end nameSpace
