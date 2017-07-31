using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using System.Windows.Forms;
using System.IO;

namespace UtilitiesFileManager
{
    /// <summary>
    /// The class handles outside file
    /// Read and write to CSV files.
    /// Extract the file path
    /// </summary>
    public class FileManager
    {

        /// <summary>
        /// Read from CSV file to dataTable
        /// </summary>
        /// <param name="CSVFilePath"></param>
        /// <returns></returns>
        public DataTable GetCSV(string CSVFilePath)
        {
            if (string.IsNullOrEmpty(CSVFilePath))
                return null;
            else
            {
                DataTable table = new DataTable();

                TextFieldParser parser = new TextFieldParser(CSVFilePath);
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                string[] fields = parser.ReadFields();
                for (int i = 0; i < fields.Length; i++)
                    table.Columns.Add(fields[i]);

                while (!parser.EndOfData)
                {
                    try
                    {
                        fields = parser.ReadFields();
                        table.Rows.Add(fields);
                    }
                    catch (Exception)
                    {
                        string temp = parser.ReadLine();
                    }
                }
                parser.Close();

                return table;
            }
        }


        /// <summary>
        /// Write fo CSV file from datatable
        /// </summary>
        /// <param name="Dtable"></param>
        /// <param name="CSVFilePath"></param>
        public void SetCSV(DataTable Dtable, string CSVFilePath)
        {
            try
            {
                StringBuilder fileContent = new StringBuilder();

                foreach (var col in Dtable.Columns)
                {
                    fileContent.Append(col.ToString() + ",");
                }

                fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);

                foreach (DataRow dr in Dtable.Rows)
                {
                    foreach (var column in dr.ItemArray)
                    {
                        fileContent.Append("\"" + column.ToString().Replace("\"", "\"\"") + "\",");
                    }

                    fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);
                }
                System.IO.File.WriteAllText(CSVFilePath, fileContent.ToString());

            }        //end exportDataTableToCSV

            catch (Exception)
            { }
        }//end  exportDataTableToCSV(DataTable Dtable, string CSVFilePath)

        /// <summary>
        /// Append data from Datatable to CSV file
        /// </summary>
        /// <param name="Dtable"></param>
        /// <param name="CSVFilePath"></param>
        public void appendToCSV(DataTable Dtable, string CSVFilePath)
        {
            StringBuilder fileContent = new StringBuilder();


            foreach (DataRow dr in Dtable.Rows)
            {
                foreach (var column in dr.ItemArray)
                {
                    fileContent.Append("\"" + column.ToString().Replace("\"", "\"\"") + "\",");
                }

                fileContent.Replace(",", System.Environment.NewLine, fileContent.Length - 1, 1);
            }
            System.IO.File.AppendAllText(CSVFilePath, fileContent.ToString());

        }        //end exportDataTableToCSV


        /// <summary>
        /// Return the path of the EXE file of the application
        /// </summary>
        /// <returns></returns>
        public string ExePath()
        {
            string exePath = Application.ExecutablePath;
            string[] cells2 = exePath.Split('\\');
            exePath = exePath.Replace(cells2[cells2.Length - 1], "");
            return exePath;
        }

        //public string oneLevelUpExePath()
        //{
        //    string exePath = this.ExePath();
        //    return oneLevelUpExePath(exePath);
        //}

        //public string oneLevelUpExePath(string path)
        //{
        //    string exePath = path;
        //    string[] separator = { "\\" };
        //    string[] exePathArr = exePath.Split(separator, StringSplitOptions.None);
        //    string[] ShortPath = new string[exePathArr.Length - 2];
        //    Array.Copy(exePathArr, ShortPath, exePathArr.Length - 2);
        //    string Result = string.Join("\\", ShortPath) + separator[0];
        //    return Result;
        //}


        public string openFilePathXML()
        {
            OpenFileDialog openFileDialogWindow = new OpenFileDialog();
            openFileDialogWindow.FileName = "*.XML";
            openFileDialogWindow.Filter = "XML files(*.XML) |*.XML| All files(*.*) |*.*";
            openFileDialogWindow.FilterIndex = 1;
            openFileDialogWindow.ShowDialog();

            string fileName = openFileDialogWindow.FileName;
            if (fileName.Equals("*.XML"))
                return null;
            return fileName;
        }

        public string openFilePathCSV()
        {
            OpenFileDialog openFileDialogWindow = new OpenFileDialog();
            openFileDialogWindow.FileName = "*.csv";
            openFileDialogWindow.Filter = "csv files(*.csv) |*.csv| All files(*.*) |*.*";
            openFileDialogWindow.FilterIndex = 1;
            openFileDialogWindow.ShowDialog();

            string fileName = openFileDialogWindow.FileName;
            if (fileName.Equals("*.csv"))
                return null;
            return fileName;
        }

        public string saveFilePath()
        {
            return saveFilePathCSV("");
        }

        public string saveFilePathCSV(string fileNameExm)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "*.csv";
            saveFileDialog.Filter = "csv files(*.csv) |*.csv| All files(*.*) |*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.FileName = fileNameExm;
            saveFileDialog.ShowDialog();


            string fileName = "";
            fileName = saveFileDialog.FileName;

            if (fileName.Equals("*.csv"))
                return null;
            return fileName;
        }

        public string saveFilePathXML()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "*.XML";
            saveFileDialog.Filter = "XML files(*.XML) |*.XML| All files(*.*) |*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.ShowDialog();


            string fileName = "";
            fileName = saveFileDialog.FileName;

            if (fileName.Equals("*.XML"))
                return null;
            return fileName;
        }

    }//end class

}
