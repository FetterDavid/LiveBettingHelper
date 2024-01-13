﻿// This file was auto-generated by ML.NET Model Builder.
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
namespace PredictModel
{
    public partial class OverModel
    {
        /// <summary>
        /// model input class for OverModel.
        /// </summary>
        #region model input class
        public class ModelInput
        {
            [LoadColumn(0)]
            [ColumnName(@"col0")]
            public float Col0 { get; set; }

            [LoadColumn(1)]
            [ColumnName(@"col1")]
            public float Col1 { get; set; }

            [LoadColumn(2)]
            [ColumnName(@"col2")]
            public float Col2 { get; set; }

            [LoadColumn(3)]
            [ColumnName(@"col3")]
            public float Col3 { get; set; }

            [LoadColumn(4)]
            [ColumnName(@"col4")]
            public float Col4 { get; set; }

            [LoadColumn(5)]
            [ColumnName(@"col5")]
            public float Col5 { get; set; }

            [LoadColumn(6)]
            [ColumnName(@"col6")]
            public float Col6 { get; set; }

            [LoadColumn(7)]
            [ColumnName(@"col7")]
            public float Col7 { get; set; }

            [LoadColumn(8)]
            [ColumnName(@"col8")]
            public float Col8 { get; set; }

            [LoadColumn(9)]
            [ColumnName(@"col9")]
            public float Col9 { get; set; }

            [LoadColumn(10)]
            [ColumnName(@"col10")]
            public float Col10 { get; set; }

            [LoadColumn(11)]
            [ColumnName(@"col11")]
            public float Col11 { get; set; }

            [LoadColumn(12)]
            [ColumnName(@"col12")]
            public float Col12 { get; set; }

            [LoadColumn(13)]
            [ColumnName(@"col13")]
            public float Col13 { get; set; }

            [LoadColumn(14)]
            [ColumnName(@"col14")]
            public float Col14 { get; set; }

            [LoadColumn(15)]
            [ColumnName(@"col15")]
            public float Col15 { get; set; }

            [LoadColumn(16)]
            [ColumnName(@"col16")]
            public float Col16 { get; set; }

            [LoadColumn(17)]
            [ColumnName(@"col17")]
            public float Col17 { get; set; }

            [LoadColumn(18)]
            [ColumnName(@"col18")]
            public float Col18 { get; set; }

            [LoadColumn(19)]
            [ColumnName(@"col19")]
            public float Col19 { get; set; }

            [LoadColumn(20)]
            [ColumnName(@"col20")]
            public float Col20 { get; set; }

            [LoadColumn(21)]
            [ColumnName(@"col21")]
            public float Col21 { get; set; }

            [LoadColumn(22)]
            [ColumnName(@"col22")]
            public float Col22 { get; set; }

            [LoadColumn(23)]
            [ColumnName(@"col23")]
            public float Col23 { get; set; }

            [LoadColumn(24)]
            [ColumnName(@"col24")]
            public float Col24 { get; set; }

            [LoadColumn(25)]
            [ColumnName(@"col25")]
            public float Col25 { get; set; }

            [LoadColumn(26)]
            [ColumnName(@"col26")]
            public float Col26 { get; set; }

            [LoadColumn(27)]
            [ColumnName(@"col27")]
            public float Col27 { get; set; }

            [LoadColumn(28)]
            [ColumnName(@"col28")]
            public float Col28 { get; set; }

            [LoadColumn(29)]
            [ColumnName(@"col29")]
            public float Col29 { get; set; }

            [LoadColumn(30)]
            [ColumnName(@"col30")]
            public float Col30 { get; set; }

            [LoadColumn(31)]
            [ColumnName(@"col31")]
            public float Col31 { get; set; }

            [LoadColumn(32)]
            [ColumnName(@"col32")]
            public float Col32 { get; set; }

            [LoadColumn(33)]
            [ColumnName(@"col33")]
            public float Col33 { get; set; }

            [LoadColumn(34)]
            [ColumnName(@"col34")]
            public float Col34 { get; set; }

            [LoadColumn(35)]
            [ColumnName(@"col35")]
            public float Col35 { get; set; }

            [LoadColumn(36)]
            [ColumnName(@"col36")]
            public float Col36 { get; set; }

            [LoadColumn(37)]
            [ColumnName(@"col37")]
            public float Col37 { get; set; }

            [LoadColumn(38)]
            [ColumnName(@"col38")]
            public float Col38 { get; set; }

        }

        #endregion

        /// <summary>
        /// model output class for OverModel.
        /// </summary>
        #region model output class
        public class ModelOutput
        {
            [ColumnName(@"col0")]
            public uint Col0 { get; set; }

            [ColumnName(@"col1")]
            public float Col1 { get; set; }

            [ColumnName(@"col2")]
            public float Col2 { get; set; }

            [ColumnName(@"col3")]
            public float Col3 { get; set; }

            [ColumnName(@"col4")]
            public float Col4 { get; set; }

            [ColumnName(@"col5")]
            public float Col5 { get; set; }

            [ColumnName(@"col6")]
            public float Col6 { get; set; }

            [ColumnName(@"col7")]
            public float Col7 { get; set; }

            [ColumnName(@"col8")]
            public float Col8 { get; set; }

            [ColumnName(@"col9")]
            public float Col9 { get; set; }

            [ColumnName(@"col10")]
            public float Col10 { get; set; }

            [ColumnName(@"col11")]
            public float Col11 { get; set; }

            [ColumnName(@"col12")]
            public float Col12 { get; set; }

            [ColumnName(@"col13")]
            public float Col13 { get; set; }

            [ColumnName(@"col14")]
            public float Col14 { get; set; }

            [ColumnName(@"col15")]
            public float Col15 { get; set; }

            [ColumnName(@"col16")]
            public float Col16 { get; set; }

            [ColumnName(@"col17")]
            public float Col17 { get; set; }

            [ColumnName(@"col18")]
            public float Col18 { get; set; }

            [ColumnName(@"col19")]
            public float Col19 { get; set; }

            [ColumnName(@"col20")]
            public float Col20 { get; set; }

            [ColumnName(@"col21")]
            public float Col21 { get; set; }

            [ColumnName(@"col22")]
            public float Col22 { get; set; }

            [ColumnName(@"col23")]
            public float Col23 { get; set; }

            [ColumnName(@"col24")]
            public float Col24 { get; set; }

            [ColumnName(@"col25")]
            public float Col25 { get; set; }

            [ColumnName(@"col26")]
            public float Col26 { get; set; }

            [ColumnName(@"col27")]
            public float Col27 { get; set; }

            [ColumnName(@"col28")]
            public float Col28 { get; set; }

            [ColumnName(@"col29")]
            public float Col29 { get; set; }

            [ColumnName(@"col30")]
            public float Col30 { get; set; }

            [ColumnName(@"col31")]
            public float Col31 { get; set; }

            [ColumnName(@"col32")]
            public float Col32 { get; set; }

            [ColumnName(@"col33")]
            public float Col33 { get; set; }

            [ColumnName(@"col34")]
            public float Col34 { get; set; }

            [ColumnName(@"col35")]
            public float Col35 { get; set; }

            [ColumnName(@"col36")]
            public float Col36 { get; set; }

            [ColumnName(@"col37")]
            public float Col37 { get; set; }

            [ColumnName(@"col38")]
            public float Col38 { get; set; }

            [ColumnName(@"Features")]
            public float[] Features { get; set; }

            [ColumnName(@"PredictedLabel")]
            public float PredictedLabel { get; set; }

            [ColumnName(@"Score")]
            public float[] Score { get; set; }

        }

        #endregion

        private static string MLNetModelPath = Path.GetFullPath("OverModel.mlnet");

        public static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictEngine(), true);


        private static PredictionEngine<ModelInput, ModelOutput> CreatePredictEngine()
        {
            var mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);
            return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
        }

        /// <summary>
        /// Use this method to predict scores for all possible labels.
        /// </summary>
        /// <param name="input">model input.</param>
        /// <returns><seealso cref=" ModelOutput"/></returns>
        public static IOrderedEnumerable<KeyValuePair<string, float>> PredictAllLabels(ModelInput input)
        {
            var predEngine = PredictEngine.Value;
            var result = predEngine.Predict(input);
            return GetSortedScoresWithLabels(result);
        }

        /// <summary>
        /// Map the unlabeled result score array to the predicted label names.
        /// </summary>
        /// <param name="result">Prediction to get the labeled scores from.</param>
        /// <returns>Ordered list of label and score.</returns>
        /// <exception cref="Exception"></exception>
        public static IOrderedEnumerable<KeyValuePair<string, float>> GetSortedScoresWithLabels(ModelOutput result)
        {
            var unlabeledScores = result.Score;
            var labelNames = GetLabels(result);

            Dictionary<string, float> labledScores = new Dictionary<string, float>();
            for (int i = 0; i < labelNames.Count(); i++)
            {
                // Map the names to the predicted result score array
                var labelName = labelNames.ElementAt(i);
                labledScores.Add(labelName.ToString(), unlabeledScores[i]);
            }

            return labledScores.OrderByDescending(c => c.Value);
        }

        /// <summary>
        /// Get the ordered label names.
        /// </summary>
        /// <param name="result">Predicted result to get the labels from.</param>
        /// <returns>List of labels.</returns>
        /// <exception cref="Exception"></exception>
        private static IEnumerable<string> GetLabels(ModelOutput result)
        {
            var schema = PredictEngine.Value.OutputSchema;

            var labelColumn = schema.GetColumnOrNull("col0");
            if (labelColumn == null)
            {
                throw new Exception("col0 column not found. Make sure the name searched for matches the name in the schema.");
            }

            // Key values contains an ordered array of the possible labels. This allows us to map the results to the correct label value.
            var keyNames = new VBuffer<float>();
            labelColumn.Value.GetKeyValues(ref keyNames);
            return keyNames.DenseValues().Select(x => x.ToString());
        }

        /// <summary>
        /// Use this method to predict on <see cref="ModelInput"/>.
        /// </summary>
        /// <param name="input">model input.</param>
        /// <returns><seealso cref=" ModelOutput"/></returns>
        public static ModelOutput Predict(ModelInput input)
        {
            var predEngine = PredictEngine.Value;
            return predEngine.Predict(input);
        }
    }
}
