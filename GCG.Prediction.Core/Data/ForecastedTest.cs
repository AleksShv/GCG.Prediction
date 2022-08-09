using System;

namespace GCG.Prediction.Core.Data
{
    public class ForecastedTest
    {
        public float ActualAmount { get; set; }
        public float ForecastedAmount { get; set; }
        public float Difference => Math.Abs(ActualAmount - ForecastedAmount);
    }
}
