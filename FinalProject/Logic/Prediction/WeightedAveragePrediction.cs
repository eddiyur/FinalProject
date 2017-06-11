using FinalProject.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Logic.Prediction
{

    public class WeightedAveragePrediction
    {
        public List<double> WeightedFactor;

        public WeightedAveragePrediction(List<double> weightedFactor)
        {
            WeightedFactor = weightedFactor;
        }

        public Dictionary<ProductClass, int> Predict(Dictionary<ProductClass, SortedDictionary<DateTime, int>> productsMonthsSummary)
        {
            Dictionary<ProductClass, int> WeightedAverageSummary = new Dictionary<ProductClass, int>();
            foreach (KeyValuePair<ProductClass, SortedDictionary<DateTime, int>> productMonthsSummary in productsMonthsSummary)
            {
                List<KeyValuePair<DateTime, int>> productMonthsSummaryList = productMonthsSummary.Value.Reverse().ToList();
                WeightedAverageSummary.Add(productMonthsSummary.Key, calculateWeightedAverage(productMonthsSummaryList));
            }
            return WeightedAverageSummary;
        }

        private int calculateWeightedAverage(List<KeyValuePair<DateTime, int>> productMonthsSummaryList)
        {
            int numberOfMonths = WeightedFactor.Count;
            if (productMonthsSummaryList.Count < numberOfMonths)
                numberOfMonths = productMonthsSummaryList.Count;

            double sum=0;
            for (int i = 0; i < numberOfMonths; i++)
            {
                sum = sum + productMonthsSummaryList[i].Value * WeightedFactor[i];
            }

            return (int)sum;
        }
    }//end class
}
