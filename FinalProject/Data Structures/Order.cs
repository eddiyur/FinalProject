using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data_Structures
{


    public class Order
    {
        public string Name { get; set; }
        public string OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime deliveryDate { get; set; }
        public List<PriceTable> ProductsList { get; set; }


        public Order()
        {

        }
    }
}
