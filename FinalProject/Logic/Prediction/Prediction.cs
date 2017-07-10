using OperationalTrainer.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperationalTrainer.Logic.Prediction
{
    public class Prediction
    {
        public enum PredictionTypes
        {
            SimpleAverage,
            WeightedAverage,
            ExponentialSmooting
        }

     
        public PredictionTypes PredictionType { get; set; }

        public Prediction()
        {
            init();
        }

        private void init()
        {
            PredictionType = PredictionTypes.SimpleAverage;
            //PredictionType = PredictionTypes.WeightedAverage;

        }
        public void PredictionManager(List<Order> orderList)
        {
            Dictionary<ProductClass, SortedDictionary<DateTime, int>> productsMonthsSummary = calculateMonthsSummary(orderList);

            SimpleAveragePrediction simpleAveragePrediction = new SimpleAveragePrediction(3);
            Dictionary<ProductClass, PredictionClass> simpleAverage = simpleAveragePrediction.Predict(productsMonthsSummary);

            List<double> weights = new List<double> { 0.5, 0.3, 0.2 };
            WeightedAveragePrediction weightedAveragePrediction = new WeightedAveragePrediction(weights);
            Dictionary<ProductClass, PredictionClass> weightedAverage = weightedAveragePrediction.Predict(productsMonthsSummary);

            double smootingFactor = 0.8;
            ExponentialSmootingPrediction exponentialSmootingPrediction = new ExponentialSmootingPrediction(smootingFactor);
            Dictionary<ProductClass, PredictionClass> exponentialSmooting = exponentialSmootingPrediction.Predict(productsMonthsSummary);

        }

        public void PredictionManager(OrdersList orderList)
        {
            Dictionary<ProductClass, SortedDictionary<DateTime, int>> productsMonthsSummary = calculateMonthsSummary(orderList.OrderList);

            SimpleAveragePrediction simpleAveragePrediction = new SimpleAveragePrediction(3);
            Dictionary<ProductClass, PredictionClass> simpleAverage = simpleAveragePrediction.Predict(productsMonthsSummary);

            List<double> weights = new List<double> { 0.5, 0.3, 0.2 };
            WeightedAveragePrediction weightedAveragePrediction = new WeightedAveragePrediction(weights);
            Dictionary<ProductClass, PredictionClass> weightedAverage = weightedAveragePrediction.Predict(productsMonthsSummary);

            double smootingFactor = 0.8;
            ExponentialSmootingPrediction exponentialSmootingPrediction = new ExponentialSmootingPrediction(smootingFactor);
            Dictionary<ProductClass, PredictionClass> exponentialSmooting = exponentialSmootingPrediction.Predict(productsMonthsSummary);

        }

        private Dictionary<ProductClass, SortedDictionary<DateTime, int>> calculateMonthsSummary(List<Order> orderList)
        {
            Dictionary<ProductClass, SortedDictionary<DateTime, int>> productsMonthsSummary = new Dictionary<ProductClass, SortedDictionary<DateTime, int>>();


            foreach (Order order in orderList)
            {
                DateTime monthNumber = getMonthNumber(order.OrderDeliveryDate);

                foreach (PriceTable priceTableRow in order.OrderProductsList)
                {
                    ProductClass product = priceTableRow.Product;
                    SortedDictionary<DateTime, int> monthsSummary;

                    bool productExist = productsMonthsSummary.TryGetValue(priceTableRow.Product, out monthsSummary);

                    if (!productExist)
                        productsMonthsSummary.Add(product, monthsSummary = new SortedDictionary<DateTime, int>());

                    monthsSummary = updateMonthsSummry(monthsSummary, monthNumber, priceTableRow.Amount);
                    productsMonthsSummary[product] = monthsSummary;
                }
            }
            return productsMonthsSummary;
        }



        private SortedDictionary<DateTime, int> updateMonthsSummry(SortedDictionary<DateTime, int> monthsSummary, DateTime monthNumber, int amount)
        {
            if (monthsSummary == null)
                monthsSummary = new SortedDictionary<DateTime, int>();

            int temp;
            bool monthExist = monthsSummary.TryGetValue(monthNumber, out temp);

            if (!monthExist)
                monthsSummary.Add(monthNumber, 0);

            monthsSummary[monthNumber] += amount;
            return monthsSummary;
        }


        private DateTime getMonthNumber(DateTime date)
        {
            int month = date.Month;
            int year = date.Year;
            return new DateTime(year, month, 1);
        }

        private string printResult(Dictionary<ProductClass, int> predictionResultList)
        {
            string result = "";
            foreach (KeyValuePair<ProductClass, int> predictionResult in predictionResultList)
                result = result + predictionResult.Key.ProductName + ": " + predictionResult.Value.ToString() + "\n";
            return result;
        }

    }
}
