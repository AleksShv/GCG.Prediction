using System;
using System.Collections.Generic;

namespace GCG.Prediction.PredictionModule.Data
{
    public class HistoricalData
    {
        public float Category { get; set; }
        public IEnumerable<TimePeriod> Periods { get; set; }

        public class TimePeriod
        {
            public DateTime Date { get; set; }
            public float Value { get; set; }
        }
    }
}
