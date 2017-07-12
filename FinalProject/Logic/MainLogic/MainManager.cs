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
        private DateTime CurrnetTime { get; set; }
        private ProcessesSchedule CurrentProcesses;

        enum ProcessesSchedule
        {
            BeginningOfTheTimeTick,
            NewCustomerOrdersArrived,
            SupplierOrdersDelivered
        }

        public MainManager()
        {
            LoadData ld = new LoadData();
            InitOperationalTrainerDataSet initOperationalTrainerDataSet = ld.LoadInitData();

            CurrnetTime = initOperationalTrainerDataSet.startDate;

            dataManager = new DataManager(initOperationalTrainerDataSet.OperationalTrainerDataSet);

            clock = new Clock(CurrnetTime);
            clock.Tick += ClockTick;

            Warehouse = new WarehouseClass(dataManager.DataSet.ProductsMetaDataList, initOperationalTrainerDataSet.WarehouseMaxCapacity);
            bank = new Bank(initOperationalTrainerDataSet.BankCurrentBalance);
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
            CurrnetTime = e.Time;
            CurrentProcesses = ProcessesSchedule.NewCustomerOrdersArrived;
            mainLogic();
        }

        
        private void mainLogic()
        {

            ProcessesScheduleParser();

            //when all loghic finish
            clock.nextHour();
        }

        private void ProcessesScheduleParser()
        {
            var a = (int)CurrentProcesses;
                }

        private void NewCustomerOrder()
        {
            OrdersList newOrders = dataManager.getNewCustomerOrdersList(CurrnetTime);
            if (newOrders.OrderList.Count > 0)
            {
                MessageBox.Show("new order Araive");
                bank.UpdateBalance(newOrders.OrderList[0]);
            }
        }

    }//end class MainManager
}
