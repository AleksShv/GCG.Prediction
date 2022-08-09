using System.IO;
using System.Threading;

using Microsoft.Extensions.ML;
using Microsoft.Extensions.Primitives;
using Microsoft.ML;

namespace GCG.Prediction.PredictionModule.Loaders
{
    internal class FileLoader : ModelLoader
    {
        private readonly MLContext _context;
        private readonly string _absoluteModelPath;

        private readonly string _predictionModulePath = "GCG.Prediction.PredictionModule";
        private readonly string _dataPath = "Models";
        private readonly string _modelPath = "ml-model.zip";

        public FileLoader()
        {
            _context = new MLContext();
            
            var appDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            _absoluteModelPath = Path.Combine(appDirectory, _predictionModulePath, _dataPath, _modelPath);
        }

        public override ITransformer GetModel()
        {
            using var strem = File.OpenRead(_absoluteModelPath);
            return _context.Model.Load(strem, out var _);
        }

        public override IChangeToken GetReloadToken()
        {
            return new CancellationChangeToken(CancellationToken.None);
        }
    }
}
