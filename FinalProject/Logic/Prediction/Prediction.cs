﻿using FinalProject.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Logic.Prediction
{
    public class Prediction
    {
        //  private SortedDictionary<DateTime, int> monthsSummary;


        public Prediction()
        {

        }

        public void PredictionManager(List<Order> orderList)
        {
            SortedDictionary<DateTime, int> monthsSummary = calculatemonthsummary(orderList);
            string result = printResult(monthsSummary);
        }

        private SortedDictionary<DateTime, int> calculatemonthsummary(List<Order> orderList)
        {
            /////////////////
            // ignore product
            /////////////////////////////
            SortedDictionary<DateTime, int> monthsSummary = new SortedDictionary<DateTime, int>();

            foreach (Order order in orderList)
            {
                DateTime monthNumber = getMonthNumber(order.OrderDeliveryDate);
                int temp;
                bool monthExist = monthsSummary.TryGetValue(monthNumber, out temp);

                if (!monthExist)
                    monthsSummary.Add(monthNumber, 0);

                List<PriceTable> productsList = order.ProductsList;
                foreach (PriceTable priceTableRow in productsList)
                {
                    ProductClass product = priceTableRow.Product;
                    int amount = priceTableRow.Amount;
                    monthsSummary[monthNumber] += amount;
                }
            }

            return monthsSummary;
            //string Result = printResult(monthsSummary);

            //int simpleAveragePrediction = SimpleAveragePrediction();
        }

        private DateTime getMonthNumber(DateTime date)
        {
            int month = date.Month;
            int year = date.Year;
            return new DateTime(year, month, 1);
        }

        private int SimpleAveragePrediction(SortedDictionary<DateTime, int> monthsSummary)
        {
            int sum = 0;
            foreach (KeyValuePair<DateTime, int> monthSummary in monthsSummary)
            {
                sum = sum + monthSummary.Value;
            }

            int result = 0;
            if (sum > 0)
                result = sum / monthsSummary.Count;

            return result;
        }


        private string printResult(SortedDictionary<DateTime, int> monthsSummary)
        {
            string result = "";

            foreach (KeyValuePair<DateTime, int> monthSummary in monthsSummary)
            {
                result = result + monthSummary.Key.ToShortDateString() + ": " + monthSummary.Value.ToString() + "\n";
            }

            return result;
        }

    }
}
