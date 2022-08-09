using Microsoft.ML.Data;

namespace GCG.Prediction.Data
{
    public class InputData
    {
        [LoadColumn(0), LoadColumnName(nameof(Category))]
        public float Category;

        [LoadColumn(1), LoadColumnName(nameof(Value))]
        public float Value;

        [LoadColumn(2), LoadColumnName(nameof(AverageValue))]
        public float AverageValue;
        
        [LoadColumn(3), LoadColumnName(nameof(MaxValue))]
        public float MaxValue;
        
        [LoadColumn(4), LoadColumnName(nameof(MinValue))]
        public float MinValue;
        
        [LoadColumn(5), LoadColumnName(nameof(Count))]
        public float Count;
        
        [LoadColumn(6), LoadColumnName(nameof(PrevValue))]
        public float PrevValue;
        
        [LoadColumn(7), LoadColumnName(nameof(NextValue))]
        public float NextValue;

        [LoadColumn(8), LoadColumnName(nameof(Year))]
        public float Year;

        [LoadColumn(9), LoadColumnName(nameof(Month))]
        public float Month;
    }
}