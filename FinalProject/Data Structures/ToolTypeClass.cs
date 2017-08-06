using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject.Data_Structures
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

        public ToolTypeClass Copy()
        { return new ToolTypeClass(ToolTypeID, ToolTypeName); }

        public ToolTypeClass(string toolTypeID)
        { ToolTypeID = toolTypeID; }

        public override bool Equals(object obj)
        {
            ToolTypeClass ToolType = (ToolTypeClass)obj;
            return ToolTypeID.Equals(ToolType.ToolTypeID);
        }

        public override int GetHashCode()
        { return ToolTypeID.GetHashCode(); }

    }//end ToolTypeClass


    public class ToolTypeClassList
    {
        public List<ToolTypeClass> ToolTypeList { get; set; }


        public ToolTypeClassList()
        { ToolTypeList = new List<ToolTypeClass>(); }

        public ToolTypeClassList(List<ToolTypeClass> toolTypeList)
        { ToolTypeList = toolTypeList; }

        public void AddToolType(ToolTypeClass toolType)
        { ToolTypeList.Add(toolType); }


        /// <summary>
        /// Delete toolType From ToolTypeList
        /// </summary>
        /// <param name="toolType"></param>
        public void DeletToolType(ToolTypeClass toolType)
        { DeletToolType(toolType.ToolTypeID); }

        /// <summary>
        /// Delete toolType From ToolTypeList
        /// </summary>
        /// <param name="toolTypeID"></param>
        public void DeletToolType(string toolTypeID)
        { ToolTypeList.Remove(GetToolType(toolTypeID)); }


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
            ToolTypeClassList NewToolTypeClassList = new ToolTypeClassList();
            foreach (ToolTypeClass tooltype in this.ToolTypeList)
                NewToolTypeClassList.AddToolType(tooltype.Copy());

            return NewToolTypeClassList;
        }


        /// <summary>
        /// Return First ToolType From the list
        /// </summary>
        /// <returns></returns>
        public ToolTypeClass GetFirstToolType()
        {
            try { return ToolTypeList[0]; }
            catch (Exception) { return null; }
        }

        /// <summary>
        /// Return First ToolType From the list and remove it from the list 
        /// </summary>
        /// <returns></returns>
        public ToolTypeClass ExtractFirstToolType()
        {
            try
            {
                ToolTypeClass result = GetFirstToolType();
                DeletToolType(result);
                return result;
            }
            catch (Exception) { return null; }
        }

    }//end class

}
