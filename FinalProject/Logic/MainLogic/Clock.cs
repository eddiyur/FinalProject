using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationalTrainer.Logic.MainLogic
{
    public class ClockTimeEventArgs : EventArgs
    {
        public DateTime Time { get; set; }
    }

    public class Clock
    {
        public EventHandler<ClockTimeEventArgs> Tick;
        public ClockTimeEventArgs ClockTime { get; set; }


        private const int TickSize = 1;


        public Clock(DateTime startTime) {
            ClockTime = new ClockTimeEventArgs();
            ClockTime.Time = startTime;
        }

        public void nextHour()
        {
            ClockTime.Time = ClockTime.Time.AddHours(TickSize);
            Tick(this, ClockTime);
        }

    }//end clock
}
