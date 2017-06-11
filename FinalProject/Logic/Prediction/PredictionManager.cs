using FinalProject.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Logic.Prediction
{
    public class PredictionManager
    {
        private Dictionary<DateTime, int> monthsSummary;

        public PredictionManager() { }

        public void predict(List<Order> orderList)
        {
            /////////////////
            // ignore product
            /////////////////////////////
            monthsSummary = new Dictionary<DateTime, int>();

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
                    monthsSummary[monthNumber] +=  amount;
                }
            }

            int simpleAveragePrediction = SimpleAveragePrediction();


        }

        private DateTime getMonthNumber(DateTime orderDeliveryDate)
        {
            return orderDeliveryDate;
        }

        private int SimpleAveragePrediction()
        {
            int sum = 0;
            foreach (KeyValuePair<DateTime,int> monthSummary in monthsSummary)
            {
                sum = sum + monthSummary.Value;
            }

            int result = 0;
            if (sum > 0)
                result = sum / monthsSummary.Count;

            return result;
        }


    }
}
