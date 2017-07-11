using FinalProject.Logic;
using OperationalTrainer.Data_Structures;
using OperationalTrainer.Logic.Warehouse;
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
        private WarehouseClass Warehouse { get; set; }
        private Bank bank { get; set; }

        public MainManager()
        {
            dataManager = new DataManager();

            clock = new Clock(dataManager.DataSet.startDate);
            clock.Tick += ClockTick;

            dataManager.ConnectToClock(clock);

            Warehouse = new WarehouseClass(dataManager.DataSet.ProductsMetaDataList, dataManager.DataSet.WarehouseMaxCapacity);
        }


        public void StartClock()
        {
            clock.nextHour();
        }


        /// <summary>
        /// Listener  to the Clock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClockTick(object sender, ClockTimeEventArgs e)
        {
            mainLogic();
        }



        private void mainLogic()
        {
            OrdersList newOrders = dataManager.getNewCustomerOrdersList();


            //when all loghic finish
            clock.nextHour();
        }

    }//end class MainManager
}
