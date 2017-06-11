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
            {
                List<KeyValuePair<DateTime, int>> productMonthsSummaryList = productMonthsSummary.Value.Reverse().ToList();
                SimpleAverageSummary.Add(productMonthsSummary.Key, calculateSimpleAverage(productMonthsSummaryList));
            }
            return SimpleAverageSummary;
        }

        // private int calculateSimpleAverag(SortedDictionary<DateTime, int> monthsSummary)
        private int calculateSimpleAverage(List<KeyValuePair<DateTime, int>> monthsSummary)
        {

            int numberOfMonths = Months;
            if (monthsSummary.Count < numberOfMonths)
                numberOfMonths = monthsSummary.Count;

            int sum = 0;
            for (int i = 0; i < numberOfMonths; i++)
                         sum = sum + monthsSummary[i].Value;
         
            int result = 0;
            if (sum > 0)
                result = sum / numberOfMonths;

            return result;
        }


    }//end class
}
