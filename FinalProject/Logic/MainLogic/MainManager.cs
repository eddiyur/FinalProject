using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject.Logic.MainLogic
{
    public class MainManager
    {
        public MainManager() { }


        public void test(Clock clock)
        {
            clock.Tick += nextHour;
        }

        private void nextHour(object sender, ClockTimeEventArgs e)
        {
            MessageBox.Show(string.Format("time: {0}", e.Time.ToShortTimeString()));
        }
    }
}
