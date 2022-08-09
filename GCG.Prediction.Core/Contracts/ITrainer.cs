using Microsoft.ML;

namespace GCG.Prediction.Core.Contracts
{
    public interface ITrainer
    {
        ITransformer TrainModel(IDataView inputData);
    }
}
