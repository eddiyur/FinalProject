using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Logic.Prediction
{
    public class PredictionClass
    {
        public SortedDictionary<DateTime, double> PredictionResults { get; set; }
        public double MAD { get; set; }
        public double MSE { get; set; }
        public double Mape { get; set; }


        public PredictionClass()
        {
            PredictionResults = new SortedDictionary<DateTime, double>();
        }

        public static List<KeyValuePair<DateTime, double>> PredictionResultsToList(SortedDictionary<DateTime, double> sortedDictionary)
        {
            List<KeyValuePair<DateTime, double>> result = new List<KeyValuePair<DateTime, double>>();
            foreach (KeyValuePair<DateTime, double> pair in sortedDictionary)
                result.Add(pair);
            return result;
        }

        public static List<KeyValuePair<DateTime, double>> PredictionResultsToList(SortedDictionary<DateTime, int> sortedDictionary)
        {
            List<KeyValuePair<DateTime, double>> result = new List<KeyValuePair<DateTime, double>>();
            foreach (KeyValuePair<DateTime, int> pair in sortedDictionary)
            {
                KeyValuePair<DateTime, double> Updatepair = new KeyValuePair<DateTime, double>(pair.Key, (double)pair.Value);
                result.Add(Updatepair);
            }
            return result;
        }
    }
}
