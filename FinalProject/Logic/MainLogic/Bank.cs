using OperationalTrainer.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Logic
{
    public class Bank
    {
        // how update data manager- direct or thru main manager

        public double CurrentBalance { get; set; }

        public Bank()
        { CurrentBalance = 0; }

        public Bank(double currentBalance)
        { CurrentBalance = currentBalance; }

        public double UpdateBalance(Order order)
        {
            int factor;
            if (order.OrderType == Order.OrderTypeEnum.CustomerOrder)
                factor = 1;
            else
                factor = -1;

            double amount = CalculateOrderAmount(order) * factor;
            CurrentBalance = CurrentBalance + amount;

            return CurrentBalance;
        }

        private double CalculateOrderAmount(Order order)
        {
            List<PriceTable> PriceTablelist = order.OrderProductsList;
            double sum = 0;

            foreach (PriceTable priceTable in PriceTablelist)
                sum = sum + priceTable.getTotalCalculation();
            return sum;
        }
    }
}
