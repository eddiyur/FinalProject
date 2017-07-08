using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Logic.MainLogic
{
    public class ClockTimeEventArgs : EventArgs
    {
        public DateTime Time { get; set; }
    }

    public class Clock
    {
        public EventHandler<ClockTimeEventArgs> Tick;
        public ClockTimeEventArgs ClockTime { get; set; }


        public Clock(DateTime startTime) {
            ClockTime = new ClockTimeEventArgs();
            ClockTime.Time = startTime;
        }

        public void nextHour()
        {
            ClockTime.Time = ClockTime.Time.AddHours(1);
            Tick(this, ClockTime);
        }

    }//end clock
}
