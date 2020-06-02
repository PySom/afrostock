using Microsoft.ML.Data;

namespace AfrroStock.Models.ML
{
    public class ImageNetPrediction
    {
        [ColumnName("grid")]
        public float[] PredictedLabels;
    }
}
