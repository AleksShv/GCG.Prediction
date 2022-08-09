using System.Collections.Generic;

using Microsoft.ML;
using Microsoft.ML.Data;

using GCG.Prediction.Core.Data;

namespace GCG.Prediction.Core.Contracts
{
    public interface IEvaluater
    {
        RegressionMetrics EvaluateModel(ITransformer model, IDataView testData);
        IEnumerable<ForecastedTest> TestModel(ITransformer model, IDataView testData);
    }
}
