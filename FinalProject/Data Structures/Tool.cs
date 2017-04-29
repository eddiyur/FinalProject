using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data_Structures
{
    public class Tool
    {
       public enum ToolStatuses
        {
            work,
            notWork
        };
        public string ToolName { get; set; }
        public ToolStatuses ToolStatus { get; set; }

    }
}
