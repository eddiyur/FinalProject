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

        public Dictionary<ProductClass, PredictionClass> Predict(Dictionary<ProductClass, SortedDictionary<DateTime, int>> productsMonthsSummary)
        {
            Dictionary<ProductClass, PredictionClass> WeightedAverageResult = new Dictionary<ProductClass, PredictionClass>();

            foreach (KeyValuePair<ProductClass, SortedDictionary<DateTime, int>> productMonthsSummary in productsMonthsSummary)
            {
                List<KeyValuePair<DateTime, int>> productMonthsSummaryList = productMonthsSummary.Value.ToList();
                PredictionClass predictuinClass = calculateWeightedAverage(productMonthsSummaryList);
                WeightedAverageResult.Add(productMonthsSummary.Key, predictuinClass);
            }
            return WeightedAverageResult;
        }

        private PredictionClass calculateWeightedAverage(List<KeyValuePair<DateTime, int>> monthsSummary)
        {
            if (monthsSummary.Count < WeightedFactor.Count)
                return null;

            PredictionClass predictionClass = new PredictionClass();

            for (int i = WeightedFactor.Count; i <= monthsSummary.Count; i++)
            {
                double sum = 0;

                for (int j = 0; j < WeightedFactor.Count; j++)
                    sum = sum + monthsSummary[i - j - 1].Value * WeightedFactor[j];

                predictionClass.PredictionResults.Add(monthsSummary[i - 1].Key.AddMonths(1), (double)sum);
            }

            return predictionClass;
        }
    }//end class
}
