using FinalProject.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Logic.Prediction
{
    public class ExponentialSmootingPrediction
    {
        public double SmootingFactor { get; set; }

        public ExponentialSmootingPrediction(double smootingFactor)
        {
            SmootingFactor = smootingFactor;
        }

        public Dictionary<ProductClass, PredictionClass> Predict(Dictionary<ProductClass, SortedDictionary<DateTime, int>> productsMonthsSummary)
        {
            Dictionary<ProductClass, PredictionClass> predictionResult = new Dictionary<ProductClass, PredictionClass>();

            foreach (KeyValuePair<ProductClass, SortedDictionary<DateTime, int>> productMonthsSummary in productsMonthsSummary)
            {
                List<KeyValuePair<DateTime, int>> productMonthsSummaryList = productMonthsSummary.Value.ToList();
                PredictionClass predictuinClass = calculateExponentialSmooting(productMonthsSummaryList);
                predictionResult.Add(productMonthsSummary.Key, predictuinClass);
            }
            return predictionResult;
        }

        private PredictionClass calculateExponentialSmooting(List<KeyValuePair<DateTime, int>> monthsSummary)
        {
            if (monthsSummary.Count < 1)
                return null;

            PredictionClass predictionClass = new PredictionClass();

            predictionClass.PredictionResults.Add(monthsSummary.First().Key, monthsSummary.First().Value);

            for (int i = 0; i < monthsSummary.Count; i++)
            {
                int observation = monthsSummary[i].Value;
                double previousPrediction;
                bool test = predictionClass.PredictionResults.TryGetValue(monthsSummary[i].Key, out previousPrediction);
                predictionClass.PredictionResults.Add(monthsSummary[i].Key.AddMonths(1), observation * SmootingFactor + previousPrediction * (1 - SmootingFactor));
            }


            return predictionClass;
        }
    }
}
