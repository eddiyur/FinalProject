using OperationalTrainer.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperationalTrainer.Logic.MainLogic
{
    public class MainManager
    {
      

        public DataManager dataManager { get; set; }
        private Clock clock { get; set; }

        public MainManager(DateTime StartDate)
        {
            
            dataManager = new DataManager();

            
            clock = new Clock(StartDate);
            clock.Tick += nextHour;
        }


        public void StartClock()
        {
            clock.nextHour();
        }



        public void test(Clock clock)
        {
            clock.Tick += nextHour;
        }

        private void nextHour(object sender, ClockTimeEventArgs e)
        {
           

            clock.nextHour();
        }
    }//end class MainManager
}
