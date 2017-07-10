using OperationalTrainer.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationalTrainer.Logic.Prediction
{
    public class SimpleAveragePrediction
    {
        public int NumberOfMonths { get; set; }

        public SimpleAveragePrediction(int months)
        {
            NumberOfMonths = months;
        }


        public Dictionary<ProductClass, PredictionClass> Predict(Dictionary<ProductClass, SortedDictionary<DateTime, int>> productsMonthsSummary)
        {
            Dictionary<ProductClass, PredictionClass> SimpleAverageResult = new Dictionary<ProductClass, PredictionClass>();

            foreach (KeyValuePair<ProductClass, SortedDictionary<DateTime, int>> productMonthsSummary in productsMonthsSummary)
            {
                List<KeyValuePair<DateTime, int>> productMonthsSummaryList = productMonthsSummary.Value.ToList();
                PredictionClass predictuinClass = calculateSimpleAverage(productMonthsSummaryList);
                SimpleAverageResult.Add(productMonthsSummary.Key, predictuinClass);
            }

            return SimpleAverageResult;
            // return SimpleAverageSummary;
        }

        // private int calculateSimpleAverag(SortedDictionary<DateTime, int> monthsSummary)
        private PredictionClass calculateSimpleAverage(List<KeyValuePair<DateTime, int>> monthsSummary)
        {
            if (monthsSummary.Count < NumberOfMonths)
                return null;

            PredictionClass predictionClass = new PredictionClass();

            for (int i = NumberOfMonths; i <= monthsSummary.Count; i++)
            {
                double sum = 0;
                for (int j = i - NumberOfMonths; j < i; j++)
                    sum = sum + monthsSummary[j].Value;
                predictionClass.PredictionResults.Add(monthsSummary[i - 1].Key.AddMonths(1), (double)sum / NumberOfMonths);
            }

            return predictionClass;

        }


    }//end class
}
