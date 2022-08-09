using System.Linq;

using Microsoft.ML;
using static Microsoft.ML.Transforms.MissingValueReplacingEstimator;

using GCG.Prediction.Core.Contracts;
using GCG.Prediction.Data;
using GCG.Prediction.Core.Consts;

namespace GCG.Prediction.Core.Implementations.Trainers
{
    public class RegressionTrainer : ITrainer
    {
        private readonly MLContext _context;

        public RegressionTrainer(MLContext context)
        {
            _context = context;
        }

        public ITransformer TrainModel(IDataView inputData)
        {
            const float learningRate = 0.325f;
            const int folds = 18;

            var pipeline = _context.Transforms
                .CopyColumns(PredictionColumns.Label, nameof(InputData.NextValue))
                .Append(_context.Transforms.ReplaceMissingValues(nameof(InputData.Category), replacementMode: ReplacementMode.Mean))
                .Append(_context.Transforms.Concatenate(PredictionColumns.Categorical, new string[] 
                {
                    nameof(InputData.Category),
                    nameof(InputData.Year),
                    nameof(InputData.Month)

                }))
                .Append(_context.Transforms.Categorical.OneHotEncoding(PredictionColumns.Categorical, PredictionColumns.Categorical))
                .Append(_context.Transforms.Concatenate(PredictionColumns.Numeric, new[]
                {
                    nameof(InputData.Value),
                    nameof(InputData.AverageValue),
                    nameof(InputData.MaxValue),
                    nameof(InputData.MinValue),
                    nameof(InputData.Count),
                    nameof(InputData.PrevValue)
                }))
                .Append(_context.Transforms.NormalizeMinMax(PredictionColumns.Numeric, PredictionColumns.Numeric))
                .Append(_context.Transforms.Concatenate(PredictionColumns.Features, PredictionColumns.Categorical, PredictionColumns.Numeric))
                .Append(_context.Regression.Trainers.FastTreeTweedie(PredictionColumns.Label, PredictionColumns.Features, learningRate: learningRate))
                .AppendCacheCheckpoint(_context);

            var models = _context.Regression
                .CrossValidate(inputData, pipeline, folds, PredictionColumns.Label);
            var crossModel = models
                .OrderByDescending(x => x.Metrics.RSquared)
                .FirstOrDefault();
            
            return crossModel.Model;
        }
    }
}
