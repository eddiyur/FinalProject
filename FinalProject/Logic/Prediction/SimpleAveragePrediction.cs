using FinalProject.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Logic.Prediction
{
    public class SimpleAveragePrediction
    {
        public int Months { get; set; }

        public SimpleAveragePrediction(int months)
        {
            Months = months;
        }

       
        public Dictionary<ProductClass, int> Predict(Dictionary<ProductClass, SortedDictionary<DateTime, int>> productsMonthsSummary)
        {
            Dictionary<ProductClass, int> SimpleAverageSummary = new Dictionary<ProductClass, int>();
            foreach (KeyValuePair<ProductClass, SortedDictionary<DateTime, int>> productMonthsSummary in productsMonthsSummary)
                SimpleAverageSummary.Add(productMonthsSummary.Key, calculateSimpleAverag(productMonthsSummary.Value));

            return SimpleAverageSummary;
        }

        private int calculateSimpleAverag(SortedDictionary<DateTime, int> monthsSummary)
        {
            int sum = 0;

            int numberOfMonths = Months;
            if (monthsSummary.Count < numberOfMonths)
                numberOfMonths = monthsSummary.Count;

            //  Dictionary < DateTime, int> temp = monthsSummary
           
               

            //for (int i = 0; i < numberOfMonths; i++)
            //{
            //    sum = sum + monthsSummary.ElementAt(i).Value;
            //}

            //    DateTime monthSummary1   = monthsSummary.Keys.ElementAt(monthsSummary.Count - 1);

            //foreach (KeyValuePair<DateTime, int> monthSummary in monthsSummary)
            //{
            //    sum = sum + monthSummary.Value;
            //}

            int result = 0;
            if (sum > 0)
                result = sum / monthsSummary.Count;

            return result;
        }


    }//end class
}
