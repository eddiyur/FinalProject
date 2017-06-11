using FinalProject.Data_Structures;
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
            //     SortedDictionary<DateTime, int> monthsSummary = new SortedDictionary<DateTime, int>();//= calculatemonthsummary(orderList);
            //   Dictionary<ProductClass, SortedDictionary<DateTime, int>> productMonthsSummary = new Dictionary<ProductClass, SortedDictionary<DateTime, int>>();
            Dictionary<ProductClass, SortedDictionary<DateTime, int>> productsMonthsSummary = calculatemonthsummary(orderList);

            Dictionary<ProductClass, int> result = SimpleAveragePrediction(productsMonthsSummary);

            //    string result = printResult(monthsSummary);
        }

        private Dictionary<ProductClass, SortedDictionary<DateTime, int>> calculatemonthsummary(List<Order> orderList)
        {
            Dictionary<ProductClass, SortedDictionary<DateTime, int>> productsMonthsSummary = new Dictionary<ProductClass, SortedDictionary<DateTime, int>>();


            foreach (Order order in orderList)
            {
                DateTime monthNumber = getMonthNumber(order.OrderDeliveryDate);

                foreach (PriceTable priceTableRow in order.ProductsList)
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

        //private SortedDictionary<DateTime, int> calculatemonthsummary(List<Order> orderList)
        //{
        //    SortedDictionary<DateTime, int> monthsSummary = new SortedDictionary<DateTime, int>();

        //    foreach (Order order in orderList)
        //    {
        //        DateTime monthNumber = getMonthNumber(order.OrderDeliveryDate);
        //        int temp;
        //        bool monthExist = monthsSummary.TryGetValue(monthNumber, out temp);

        //        if (!monthExist)
        //            monthsSummary.Add(monthNumber, 0);

        //        List<PriceTable> productsList = order.ProductsList;
        //        foreach (PriceTable priceTableRow in productsList)
        //        {
        //            ProductClass product = priceTableRow.Product;
        //            int amount = priceTableRow.Amount;
        //            monthsSummary[monthNumber] += amount;
        //        }
        //    }

        //    return monthsSummary;
        //    //string Result = printResult(monthsSummary);

        //    //int simpleAveragePrediction = SimpleAveragePrediction();
        //}

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

        private Dictionary<ProductClass, int> SimpleAveragePrediction(Dictionary<ProductClass, SortedDictionary<DateTime, int>> productsMonthsSummary)
        {
            Dictionary<ProductClass, int> SimpleAverageSummary = new Dictionary<ProductClass, int>();
            foreach (KeyValuePair<ProductClass, SortedDictionary<DateTime, int>> productMonthsSummary in productsMonthsSummary)
                SimpleAverageSummary.Add(productMonthsSummary.Key, SimpleAveragePrediction(productMonthsSummary.Value));

            return SimpleAverageSummary;
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
