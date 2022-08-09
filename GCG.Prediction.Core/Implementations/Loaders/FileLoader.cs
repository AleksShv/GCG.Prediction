using Microsoft.Extensions.Options;
using Microsoft.ML;

using GCG.Prediction.Core.Settings;
using GCG.Prediction.Core.Contracts;
using GCG.Prediction.Data;
using GCG.Prediction.Core.Helpers;

namespace GCG.Prediction.Core.Implementations.Loader
{
    public class FileLoader : IDataLoader
    {
        private readonly MLContext _context;
        private readonly MLSettings _settings;

        public FileLoader(MLContext context, IOptions<MLSettings> options)
        {
            _context = context;
            _settings = options.Value;
        }

        public IDataView LoadInputData() =>
            LoadData(_settings.TrainingDataPath);

        public IDataView LoadTestData() =>
            LoadData(_settings.TestingDataPath);

        private IDataView LoadData(string path) =>
            _context.Data.LoadFromTextFile<InputData>(PathHelpers.GetAbsolutePath(path), ',', true);
    }
}
