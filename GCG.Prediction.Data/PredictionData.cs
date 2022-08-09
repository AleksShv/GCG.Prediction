using Microsoft.ML.Data;

namespace GCG.Prediction.Data
{
    public class PredictionData
    {
        [ColumnName("Score")]
        public float Score;
    }
}
