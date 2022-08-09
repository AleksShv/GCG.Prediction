using System;

using Microsoft.Extensions.Logging;

using GCG.Prediction.Core.Contracts;
using GCG.Prediction.Core.Helpers;

namespace GCG.Prediction.PredictionModule.ModelTrainers
{
    public class RegressionModelTrainer
    {
        private readonly IDataLoader _loader;
        private readonly ITrainer _trainer;
        private readonly IEvaluater _evaluater;
        private readonly ISaver _saver;
        private readonly ILogger _logger;

        public RegressionModelTrainer(
            IDataLoader loader, 
            ITrainer trainer, 
            IEvaluater evaluater, 
            ISaver saver,
            ILogger<RegressionModelTrainer> logger)
        {
            _loader = loader;
            _trainer = trainer;
            _evaluater = evaluater;
            _saver = saver;
            _logger = logger;
        }

        public void TrainAndSaveModel()
        {
            try
            {
                var inputData = _loader.LoadInputData();
                var testData = _loader.LoadTestData();

                var model = _trainer.TrainModel(inputData);

                var tests = _evaluater.TestModel(model, testData);
                var metrics = _evaluater.EvaluateModel(model, testData);

                _logger.LogInformation(OutputHelpers.GetTests(tests));
                _logger.LogInformation(OutputHelpers.GetMetrics(metrics));

                _logger.LogInformation("Press Enter for save model or Escape for abort");
                if (CheckPressedKey())
                {
                    _saver.SaveModel(model, inputData.Schema);
                    _logger.LogInformation("Model Saved");
                }
                else
                {
                    _logger.LogInformation("Model not saved");
                }
            }
            catch (Exception exception)
            {
                _logger.LogCritical($"{exception.Message} | {exception.StackTrace}");
            }
        }

        private static bool CheckPressedKey()
        {
            var keyInfo = Console.ReadKey();
            while (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Escape)
            {
                keyInfo = Console.ReadKey();
            }
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                return true;
            }
            return false;
        }
    }
}
