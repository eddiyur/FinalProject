using OperationalTrainer.Data_Structures;
using OperationalTrainer.Logic.MainLogic;
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
    public static class InitDataParser
    {

        enum XMLiInitFields
        {
            Bank,
            WarehouseMaxCapacity,
            StartDate
        }

        public static InitDataStructureClass Parse(XmlNodeList initNodeList)
        {
            InitDataStructureClass OperationalTrainerInitDataSet = new InitDataStructureClass();

            foreach (XmlNode initParameter in initNodeList)
            {
                XMLiInitFields XMLProductField = (XMLiInitFields)Enum.Parse(typeof(XMLiInitFields), initParameter.Name, true);
                switch (XMLProductField)
                {
                    case XMLiInitFields.Bank:
                        OperationalTrainerInitDataSet.InitBankCurrentBalance = Convert.ToDouble(initParameter.InnerText);
                        break;
                    case XMLiInitFields.WarehouseMaxCapacity:
                        OperationalTrainerInitDataSet.WarehouseMaxCapacity = Convert.ToDouble(initParameter.InnerText);
                        break;
                    case XMLiInitFields.StartDate:
                        string a = initParameter.InnerText;
                        OperationalTrainerInitDataSet.startDate = DateTime.Parse(initParameter.InnerText);
                        break;
                    default:
                        break;
                }
            }
            return OperationalTrainerInitDataSet;
        }


        public static XmlDocument InitDataCSVToXML(XmlDocument doc, string SourcefileName)
        {

            FileManager fm = new FileManager();
            DataTable dt = fm.GetCSV(SourcefileName);

            var root = doc.GetElementsByTagName(LoadData.XMLMainCategories.dataset.ToString())[0];
            var initData = doc.CreateElement(LoadData.XMLMainCategories.InitData.ToString());

            root.AppendChild(initData);

            foreach (DataRow row in dt.Rows)
            {
                var bankTagName = doc.CreateElement(XMLiInitFields.Bank.ToString());
                bankTagName.InnerText = row[0].ToString();
                initData.AppendChild(bankTagName);

                var WarehouseMaxCapacityTagName = doc.CreateElement(XMLiInitFields.WarehouseMaxCapacity.ToString());
                WarehouseMaxCapacityTagName.InnerText = row[1].ToString();
                initData.AppendChild(WarehouseMaxCapacityTagName);

                var StartDateTagName = doc.CreateElement(XMLiInitFields.StartDate.ToString());
                StartDateTagName.InnerText = LoadData.transfareDate(row[2].ToString());
                initData.AppendChild(StartDateTagName);
            }
            return doc;
        }
    }//end class
}
