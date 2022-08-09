using Microsoft.ML;

namespace GCG.Prediction.Core.Contracts
{
    public interface ISaver
    {
        void SaveModel(ITransformer model, DataViewSchema schema);
    }
}
