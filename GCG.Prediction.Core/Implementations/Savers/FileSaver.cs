using System.IO;

using Microsoft.ML;
using Microsoft.Extensions.Options;

using GCG.Prediction.Core.Contracts;
using GCG.Prediction.Core.Helpers;
using GCG.Prediction.Core.Settings;

namespace GCG.Prediction.Core.Implementations.Savers
{
    public class FileSaver : ISaver
    {
        private readonly MLContext _context;
        private readonly MLSettings _settings;

        public FileSaver(MLContext context, IOptions<MLSettings> options)
        {
            _context = context;
            _settings = options.Value;
        }

        public void SaveModel(ITransformer model, DataViewSchema schema)
        {
            var modelPath = PathHelpers.GetAbsolutePath(_settings.ModelPath);

            if (File.Exists(modelPath))
            {
                File.Delete(modelPath);
            }

            _context.Model.Save(model, schema, modelPath);
        }
    }
}
