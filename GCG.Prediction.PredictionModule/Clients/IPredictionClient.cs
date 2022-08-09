using System.Collections.Generic;

using GCG.Prediction.PredictionModule.Data;

namespace GCG.Prediction.PredictionModule.Clients
{
    public interface IPredictionClient
    {    
        HistoricalData Predict(HistoricalData historicalData);
        HistoricalInputData ConvertToInputData(HistoricalData historicalData);
        IEnumerable<HistoricalData> CreateHistoricalData<T>(IEnumerable<T> items,
            string categoryPropertyName, string datePropertyName, string valuePropertyName);
    }
}
