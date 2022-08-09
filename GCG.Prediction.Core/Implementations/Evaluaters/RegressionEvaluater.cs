using System.Collections.Generic;
using System.Linq;

using Microsoft.ML;
using Microsoft.ML.Data;

using GCG.Prediction.Core.Contracts;
using GCG.Prediction.Core.Data;
using GCG.Prediction.Data;
using GCG.Prediction.Core.Consts;

namespace GCG.Prediction.Core.Implementations.Evaluaters
{
    public class RegressionEvaluater : IEvaluater
    {
        private readonly MLContext _context;

        public RegressionEvaluater(MLContext context)
        {
            _context = context;
        }

        public RegressionMetrics EvaluateModel(ITransformer model, IDataView testData)
        {
            var predictions = model.Transform(testData);
            return _context.Regression
                .Evaluate(predictions, PredictionColumns.Label, PredictionColumns.Score);
        }

        public IEnumerable<ForecastedTest> TestModel(ITransformer model, IDataView testData)
        {
            var forecaster = _context.Model.CreatePredictionEngine<InputData, PredictionData>(model);
            var actual = _context.Data.CreateEnumerable<InputData>(testData, true);

            var forecasts = actual
                .Select(x => forecaster.Predict(x))
                .ToList();

            return actual.Select((x, idx) => new ForecastedTest
            {
                ActualAmount = x.NextValue,
                ForecastedAmount = forecasts[idx].Score
            });
        }
    }
}
