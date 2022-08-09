using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.ML;

using GCG.Prediction.Data;
using GCG.Prediction.PredictionModule.Data;
using static GCG.Prediction.PredictionModule.Data.HistoricalData;

namespace GCG.Prediction.PredictionModule.Clients
{
    internal class PredictionClient : IPredictionClient
    {
        private readonly PredictionEnginePool<InputData, PredictionData> _forecaster;

        public PredictionClient(
            PredictionEnginePool<InputData, PredictionData> forecaster)
        {
            _forecaster = forecaster;
        }

        public HistoricalData Predict(HistoricalData historicalData)
        {
            var toPredict = ConvertToPeriodStatisticData(historicalData);
            var predicts = toPredict.Select(x => _forecaster.Predict(x));

            var result = new HistoricalData
            {
                Category = historicalData.Category,
                Periods = predicts.Select((x, idx) => new TimePeriod
                {
                    Value = x.Score,
                    Date = new DateTime((int)toPredict.ElementAt(idx).Year, (int)toPredict.ElementAt(idx).Month, 1)
                        .AddMonths(1)
                })
            };

            return result;
        }

        public HistoricalInputData ConvertToInputData(HistoricalData historicalData)
        {
            var data = ConvertToPeriodStatisticData(historicalData);
            var statistics = new HistoricalInputData
            {
                Category = historicalData.Category,
                Periods = data.Select(x => new HistoricalInputData.TimePeriod
                {
                    Value = x.Value,
                    AverageValue = x.AverageValue,
                    MaxValue = x.MaxValue,
                    MinValue = x.MinValue,
                    Count = x.Count,
                    NextValue = x.NextValue,
                    PrevValue = x.PrevValue,
                    Date = new DateTime((int)x.Year, (int)x.Month, 1)
                })
                .OrderBy(x => x.Date)
            };
            return statistics;
        }

        public IEnumerable<HistoricalData> CreateHistoricalData<T>(IEnumerable<T> items, 
            string categoryPropertyName, string datePropertyName, string predictedValuePropertyName)
        {
            static object GetPropertyValue(T item, string propertyName) =>
                typeof(T).GetProperty(propertyName).GetValue(item);

            var historicalData = items
                .GroupBy(x => GetPropertyValue(x, categoryPropertyName))
                .Select(x => new HistoricalData
                {
                    Category = x.Key == null ? float.NaN : Convert.ToSingle(x.Key),
                    Periods = x.Select(y => new TimePeriod
                    {
                        Date = Convert.ToDateTime(GetPropertyValue(y, datePropertyName)),
                        Value = Convert.ToSingle(GetPropertyValue(y, predictedValuePropertyName))
                    })
                    .OrderBy(x => x.Date)
                })
                .OrderBy(x => x.Category);

            return historicalData;
        }

        private static IEnumerable<InputData> ConvertToPeriodStatisticData(HistoricalData historicalData)
        {
            var statisticData = historicalData.Periods
                .GroupBy(x => new
                {
                    x.Date.Year,
                    x.Date.Month
                })
                .OrderBy(x => x.Key.Year)
                    .ThenBy(x => x.Key.Month)
                .Select(x => new InputData
                {
                    Category = historicalData.Category,
                    Value = x.Sum(y => y.Value),
                    AverageValue = x.Average(y => y.Value),
                    MaxValue = x.Max(y => y.Value),
                    MinValue = x.Min(y => y.Value),
                    Count = x.Count(),
                    PrevValue = 0,
                    NextValue = 0,
                    Year = x.Key.Year,
                    Month = x.Key.Month,
                })
                .ToList();

            foreach (var item in statisticData)
            {
                var idx = statisticData.IndexOf(item);

                var prev = statisticData.ElementAtOrDefault(idx - 1);
                var next = statisticData.ElementAtOrDefault(idx + 1);

                item.NextValue = next != null ? next.Value : 0;
                item.PrevValue = prev != null ? prev.Value : 0;
            }

            return statisticData;
        }
    }
}
