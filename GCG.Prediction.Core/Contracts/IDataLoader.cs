using Microsoft.ML;

namespace GCG.Prediction.Core.Contracts
{
    public interface IDataLoader
    {
        IDataView LoadInputData();
        IDataView LoadTestData();
    }
}
