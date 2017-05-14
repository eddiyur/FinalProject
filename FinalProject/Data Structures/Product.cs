using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data_Structures
{
    public class Product
    {
        
        public string ProductID { get; set; }
        public string PruductName { get; set; }
        public double ProductCapacity { get; set; }
        public Dictionary<Product,int> ProductTree { get; set; }

        public Dictionary<Tool, int> ProductProcess { get; set; } // רשימת פעולות עיבוד ומשכן: מכונה-משך עיבוד במכונה
        public Dictionary<Tool, int> ProductSetup { get; set; } //רשימת פעולות כוונון למוצר: מכונה-משך כוונון של מכונה למוצר





    }
}
