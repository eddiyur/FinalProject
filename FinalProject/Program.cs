using FinalProject;
using FinalProject.Controllers;
using FinalProject.GUI;
using Operational_Trainer;
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
          //  Application.Run(mf);

            Form1 form1 = new Form1();
            Application.Run(form1);
        }
    }

}
