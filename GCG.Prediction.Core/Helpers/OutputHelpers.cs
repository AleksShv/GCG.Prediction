using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.ML.Data;

using GCG.Prediction.Core.Data;

namespace GCG.Prediction.Core.Helpers
{
    public static class OutputHelpers
    { 
        public static string GetMetrics(RegressionMetrics metrics) =>
            $"MAE: {metrics.MeanSquaredError}, RMSE: {metrics.RootMeanSquaredError}, RSquared: {metrics.RSquared}";

        public static string GetTests(IEnumerable<ForecastedTest> tests)
        {
            var sb = new StringBuilder();
            tests
                .Select((test, idx) => (test, idx))
                .ToList()
                .ForEach(x => 
                {
                    sb
                        .AppendLine($"----- Test N{x.idx + 1} -----")
                        .AppendLine($"Actual value: {x.test.ActualAmount}")
                        .AppendLine($"Forecasted value: {x.test.ForecastedAmount}")
                        .AppendLine($"*Difference: {x.test.Difference}")
                        .AppendLine();
                });

            return sb.ToString();
        }
    }
}
