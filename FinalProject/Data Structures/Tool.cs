using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UtilitiesFileManager;

namespace OperationalTrainer.Data_Structures
{
     public class ToolTypeClass
    {
        public string ToolTypeID { get; set; }
        public string ToolTypeName { get; set; }

        public ToolTypeClass() { }
        public ToolTypeClass(string toolTypeID, string toolTypeName)
        {
            ToolTypeID = toolTypeID;
            ToolTypeName = toolTypeName;
        }

        public ToolTypeClass(string toolTypeID)
        { ToolTypeID = toolTypeID; }

        public override bool Equals(object obj)
        {
            ToolTypeClass toolClass = (ToolTypeClass)obj;
            return toolClass.ToolTypeID.Equals(toolClass.ToolTypeID);
        }

        public override int GetHashCode()
        { return ToolTypeID.GetHashCode(); }
    }//end ToolTypeClass


    public class ToolTypeClassList
    {
        public List<ToolTypeClass> ToolTypeList { get; set; }
        public ToolTypeClassList() { ToolTypeList = new List<ToolTypeClass>(); }
        public ToolTypeClassList(List<ToolTypeClass> toolTypeList)
        { ToolTypeList = toolTypeList; }

        public void AddToolType(ToolTypeClass toolType)
        { ToolTypeList.Add(toolType); }

        public ToolTypeClass GetToolType(string toolTypeID)
        {
            try
            {
                ToolTypeClass toolType = new ToolTypeClass(toolTypeID);
                return ToolTypeList[ToolTypeList.IndexOf(toolType)];
            }
            catch (Exception)
            {
                MessageBox.Show("ToolType ID " + toolTypeID + " not found");
                return null;
            }
        }

        public ToolTypeClassList copy()
        {
            ToolTypeClassList NewtoolTypeClassList = new ToolTypeClassList();
            foreach (ToolTypeClass tooltype in this.ToolTypeList)
            {
                ToolTypeClass newtoolType = new ToolTypeClass(tooltype.ToolTypeID);
                NewtoolTypeClassList.AddToolType(newtoolType);
            }
            return NewtoolTypeClassList;
        }
    }//end class

    public class SetupTimeStructure
    {
        public ProductClass SourceProduct { get; set; }
        public ProductClass TargetProduct { get; set; }
        public int SetupTime { get; set; }
    }

    public class ProductionStatistics
    {
        public int MaxTimeUnits;
        public int CurrentTimeUnits;
        public int ProdactionTime;
        public int SetupTime;
        public int IdleTime;
        public int TechCate;
        public int WaithingTime;
    }
    public class Tool
    {
        public enum ToolStatuses
        {
            Production,
            Setup,
            Service,
            Idle,
            OutOfShipt
        }

        public enum ProductionMethods
        {
            FIFO,
            EDD,
            CurrentJob,
            SPT
        }

        public string ToolID { get; set; }
        public string ToolName { get; set; }
        public ToolTypeClass ToolType { get; set; }
        public int ShiftStartTime { get; set; }
        public int ShiftEndTime { get; set; }
        public Dictionary<ProductClass, int> ProcessingTime { get; set; }
        public ToolStatuses ToolStatus { get; set; }
        public ProductionMethods ProductionMethod { get; set; }
        public List<SetupTimeStructure> SetUpTimes { get; set; }
        public ProductClass CurrentSetup { get; set; }
        public ProductionOrder CurrentProductionOrder { get; set; }
        public ProductionOrder NextProductionOrder { get; set; }
        public ProductionStatistics productionStatistics { get; set; }

        public Tool()
        {
            ProcessingTime = new Dictionary<ProductClass, int>();
            ToolStatus = ToolStatuses.Idle;
            ProductionMethod = ProductionMethods.FIFO;
            SetUpTimes = new List<SetupTimeStructure>();
            productionStatistics = new ProductionStatistics();
        }


    }//end class Tool

    public class ToolsList
    {
        List<Tool> toolList { get; set; }
        public ToolsList()
        {
            toolList = new List<Tool>();
        }

        public void AddTool(Tool tool)
        {
            toolList.Add(tool);
        }

    }
}

