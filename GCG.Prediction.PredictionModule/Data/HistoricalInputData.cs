using System;
using System.Collections.Generic;

namespace GCG.Prediction.PredictionModule.Data
{
    public class HistoricalInputData
    {
        public float Category { get; set; }
        public IEnumerable<TimePeriod> Periods { get; set; }

        public class TimePeriod
        {
            public float Value { get; set; }
            public float AverageValue { get; set; }
            public float MaxValue { get; set; }
            public float MinValue { get; set; }
            public float Count { get; set; }
            public float PrevValue { get; set; }
            public float NextValue { get; set; }
            public DateTime Date { get; set; }
        }
    }
}
