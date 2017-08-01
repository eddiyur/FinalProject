using OperationalTrainer.Data_Structures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UtilitiesFileManager;

namespace FinalProject.FileManagerFolder
{
    public static class ToolTypeParser
    {
        enum XMlToolTypeClassFields
        {
            ToolTypeBranch,
            ToolTypeID,
            ToolTypeName
        }
        public static ToolTypeClassList parse(XmlNodeList ToolTypeClassNodeList)
        {
            ToolTypeClassList toolTypeClassList = new ToolTypeClassList();

            foreach (XmlNode toolTypeParameter in ToolTypeClassNodeList)
            {
                ToolTypeClass toolType = new ToolTypeClass();

                XMlToolTypeClassFields XMLToolTypeField = (XMlToolTypeClassFields)Enum.Parse(typeof(XMlToolTypeClassFields), toolTypeParameter.Name, true);

                if (XMLToolTypeField == XMlToolTypeClassFields.ToolTypeBranch)
                    toolType = getToolType(toolTypeParameter);
                toolTypeClassList.AddToolType(toolType);
            }
            return toolTypeClassList;
        }

        private static ToolTypeClass getToolType(XmlNode toolTypeParameter)
        {
            XmlNodeList toolTypeBranchList = toolTypeParameter.ChildNodes;
            string toolTypeID = toolTypeBranchList[0].InnerText;
            string toolTypeName = toolTypeBranchList[1].InnerText;
            return new ToolTypeClass(toolTypeID, toolTypeName);
        }

        internal static XmlDocument ToolTypeCSVToXML(XmlDocument doc, string SourcefileName)
        {
            FileManager fm = new FileManager();
            DataTable dt = fm.GetCSV(SourcefileName);

            var root = doc.GetElementsByTagName(LoadData.XMLMainCategories.dataset.ToString())[0];
            var toolTypeList = doc.CreateElement(LoadData.XMLMainCategories.ToolTypeList.ToString());

            root.AppendChild(toolTypeList);

            foreach (DataRow row in dt.Rows)
            {
                var toolTypeBranch = doc.CreateElement(XMlToolTypeClassFields.ToolTypeBranch.ToString());

                var ToolTypeIDTagName = doc.CreateElement(XMlToolTypeClassFields.ToolTypeID.ToString());
                ToolTypeIDTagName.InnerText = row[0].ToString();
                toolTypeBranch.AppendChild(ToolTypeIDTagName);

                var ToolTypeNameTagName = doc.CreateElement(XMlToolTypeClassFields.ToolTypeName.ToString());
                ToolTypeNameTagName.InnerText = row[1].ToString();
                toolTypeBranch.AppendChild(ToolTypeNameTagName);

                toolTypeList.AppendChild(toolTypeBranch);
            }
            return doc;
        }
    }//end class
}
