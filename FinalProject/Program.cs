using FinalProject;
using FinalProject.Controllers;
using FinalProject.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperationalTrainer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm mf = new MainForm();
            MainController.Initialize(mf);
            Application.Run(mf);
        }
    }

    //manager to deside what product will go to what machin
    //managment of products by name (same name- same machin type- soft codedd)
    //

}
